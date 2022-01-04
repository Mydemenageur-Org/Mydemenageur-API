﻿using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.BLL.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Settings.Interfaces;
using AutoMapper;

namespace Mydemenageur.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDPUser _dpUser;
        private readonly IMongoCollection<User> _users;
        private readonly IMydemenageurSettings _mydemenageurSettings;
        private readonly IMapper _mapper;
        private readonly IDPMyDemenageurUser _dpMyDemUser;

        public AuthenticationService(IDPUser dpUser, IMydemenageurSettings mydemenageurSettings, IMapper mapper, IDPMyDemenageurUser dpMyDemUser)
        {
            _dpUser = dpUser;
            _users = _dpUser.Obtain();
            _mapper = mapper;
            _dpMyDemUser = dpMyDemUser;

            _mydemenageurSettings = mydemenageurSettings;
        }

        public async Task<MyDemenageurUser> LoginAsync(string username, string password)
        {
            // We don't send exception here, the request is only
            // valid for non null values
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            // It's important to note here that the username can
            // be both the actual username or the email
            var user = await (await _users.FindAsync(dbUser =>
                dbUser.Email == username.ToLower() ||
                dbUser.Username == username.ToLower()
            )).FirstOrDefaultAsync();

            if (user == null) { return null; }
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) { return null; }

            // Since the authentication is successful, now we can
            // generate the token
            user.Token = TokenForUser(user);

            var update = Builders<MyDemenageurUser>.Update
                .Set(dbMyDemUser => dbMyDemUser.LastConnection, DateTime.Now)
                .Set(dbMyDemUser => dbMyDemUser.Token, user.Token);

            var myDemUser = await (await _dpMyDemUser.GetCollection().FindAsync(dbMyDemUser => dbMyDemUser.UserId == user.Id)).FirstOrDefaultAsync();

            await _dpMyDemUser.GetCollection().UpdateOneAsync(dbMyDemUser => dbMyDemUser.UserId == user.Id, update);

            myDemUser = await (await _dpMyDemUser.GetCollection().FindAsync(dbMyDemUser => dbMyDemUser.UserId == user.Id)).FirstOrDefaultAsync();

            return myDemUser;
        }

        public async Task<MyDemenageurUser> RegisterAsync(RegisterModel registerModel)
        {
            // We need some basic checks
            if (string.IsNullOrWhiteSpace(registerModel.Email)) { throw new Exception("An email is required for the registration");  }
            if (string.IsNullOrWhiteSpace(registerModel.Username)) { throw new Exception("A username is required for the registration"); }
            if (UserExist(registerModel.Email, registerModel.Username)) { throw new Exception("The user already exist"); }
            //if(registerModel.Role.Equals("Admin")) { throw new UnauthorizedAccessException("You are not authorized to register with this role"); }

            registerModel.Role = "User"; // TODO: temp workaround

            // Password stuff, to ensure we never have clear password stored
            CreatePasswordHash(registerModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // Now we register the user in the database
            User dbUser = new()
            {
                Email = registerModel.Email,
                PasswordHash = Convert.ToBase64String(passwordHash),
                PasswordSalt = passwordSalt
            };

            await _users.InsertOneAsync(dbUser);

            MyDemenageurUser mdUser = _mapper.Map<MyDemenageurUser>(registerModel);
            mdUser.UserId = dbUser.Id;
                                
            await _dpMyDemUser.GetCollection().InsertOneAsync(mdUser);

            return mdUser;
        }

        public async Task<string> UpdatePassword(string id, string password)
        {
            MyDemenageurUser mdUser = await _dpMyDemUser.GetUserById(id).FirstOrDefaultAsync();
            User userAuth = await _dpUser.GetUserById(mdUser.UserId).FirstOrDefaultAsync();

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            if (Convert.ToBase64String(passwordHash) == userAuth.PasswordHash)
            {
                return "The password must be different from the original";
            }

            var update = Builders<User>.Update
                .Set(usr => usr.PasswordHash, Convert.ToBase64String(passwordHash))
                .Set(usr => usr.PasswordSalt, passwordSalt);

            await _dpUser.Obtain().UpdateOneAsync(user => user.Id == userAuth.Id, update);
            return mdUser.Id;
        }

        public async Task<MyDemenageurUser> TokenizeFirebaseUser(FirebaseUserModel user)
        {
            MyDemenageurUser mdUser = new MyDemenageurUser();
            //Step one, check if the MyDemUser exist. If not we create one
            mdUser = await (await _dpMyDemUser.GetCollection().FindAsync(dbMyDemUser => dbMyDemUser.Email == user.Email)).FirstOrDefaultAsync();

            if(mdUser == null)
            {
                mdUser = _mapper.Map<MyDemenageurUser>(user);
                await _dpMyDemUser.GetCollection().InsertOneAsync(mdUser);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_mydemenageurSettings.ApiSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", mdUser.Id.ToString()),
                    new Claim(ClaimTypes.Role, mdUser.Role),
                    new Claim("username", mdUser.Username),
                    new Claim("firstName", mdUser.FirstName != null ? mdUser.FirstName : ""),
                    new Claim("lastName", mdUser.LastName != null ? mdUser.LastName : ""),
                    new Claim("tokens", mdUser.MDToken != null ? mdUser.MDToken : "0"),
                    new Claim("about", mdUser.About != null ? mdUser.MDToken : ""),
                    new Claim("lastConnection", mdUser.LastConnection.ToString()),
                    new Claim(ClaimTypes.DateOfBirth, mdUser.Birthday.ToString() != null ? mdUser.Birthday.ToString() : ""),
                    new Claim(ClaimTypes.Gender, mdUser.Gender != null ? mdUser.Gender : ""),
                    new Claim("city", mdUser.City != null ? mdUser.City : ""),
                    new Claim(ClaimTypes.MobilePhone, mdUser.Phone != null ? mdUser.Phone : ""),
                    new Claim(ClaimTypes.Email, user.Email != null ? user.Email : ""),
                    new Claim(ClaimTypes.StreetAddress, mdUser.Address != null ? mdUser.Address : ""),
                    new Claim("complementaryAddress", mdUser.ComplementaryAddress != null ? mdUser.ComplementaryAddress : ""),
                    new Claim(ClaimTypes.PostalCode, mdUser.ZipCode != null ? mdUser.ZipCode : ""),
                    new Claim("isFirebaseUser", mdUser.IsFirebaseAccount.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            mdUser.Token = tokenHandler.WriteToken(token);

            return mdUser;
        }

        public async Task<string> LogoutAsync(string nameId)
        {
            if(nameId.Length == 0)
            {
                return null;
            }

            // It's important to note here that the username can
            // be both the actual username or the email
            var user = await (await _users.FindAsync(dbUser =>
                    dbUser.Id == nameId
            )).FirstOrDefaultAsync();

            if (user == null) { return null; }

            var update = Builders<MyDemenageurUser>.Update
                .Set(dbMyDemUser => dbMyDemUser.Token, "");

            var myDemUser = await (await _dpMyDemUser.GetCollection().FindAsync(dbMyDemUser => dbMyDemUser.UserId == user.Id)).FirstOrDefaultAsync();

            await _dpMyDemUser.GetCollection().UpdateOneAsync(dbMyDemUser => dbMyDemUser.UserId == user.Id, update);

            return nameId;
        }

        private bool UserExist(string email, string username)
        {
            return _users.AsQueryable().Any(dbUser =>
                dbUser.Email == email.ToLower() ||
                dbUser.Username == username.ToLower()
            );
        }

        // Password related functions
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password)) { throw new Exception("The password must not be null or empty"); }

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, string storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password)) { throw new Exception("The password must not be null or empy"); }
            if (storedSalt.Length != 128) { throw new Exception("Invalid length of password salt (128 bytes expected)"); }

            byte[] storedHashBytes = Convert.FromBase64String(storedHash);

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; ++i)
                {
                    if (computedHash[i] != storedHashBytes[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // Generate the token for the user
        private string TokenForUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_mydemenageurSettings.ApiSecret);
            var myDemClient = _dpMyDemUser.Obtain().Where(mdUser => mdUser.UserId == user.Id).FirstOrDefault();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", myDemClient.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, myDemClient.Role),
                    new Claim("username", myDemClient.Username),
                    new Claim("firstName", myDemClient.FirstName != null ? myDemClient.FirstName : ""),
                    new Claim("lastName", myDemClient.LastName != null ? myDemClient.LastName : ""),
                    new Claim("tokens", myDemClient.MDToken != null ? myDemClient.MDToken : "0"),
                    new Claim("about", myDemClient.About != null ? myDemClient.MDToken : ""),
                    new Claim("lastConnection", myDemClient.LastConnection.ToString()),
                    new Claim(ClaimTypes.DateOfBirth, myDemClient.Birthday.ToString() != null ? myDemClient.Birthday.ToString() : ""),
                    new Claim(ClaimTypes.Gender, myDemClient.Gender != null ? myDemClient.Gender : ""),
                    new Claim("city", myDemClient.City != null ? myDemClient.City : ""),
                    new Claim(ClaimTypes.MobilePhone, myDemClient.Phone != null ? myDemClient.Phone : ""),
                    new Claim(ClaimTypes.Email, user.Email != null ? user.Email : ""),
                    new Claim(ClaimTypes.StreetAddress, myDemClient.Address != null ? myDemClient.Address : ""),
                    new Claim("complementaryAddress", myDemClient.ComplementaryAddress != null ? myDemClient.ComplementaryAddress : ""),
                    new Claim(ClaimTypes.PostalCode, myDemClient.ZipCode != null ? myDemClient.ZipCode : ""),
                    new Claim("isFirebaseUser", myDemClient.IsFirebaseAccount.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }


}
