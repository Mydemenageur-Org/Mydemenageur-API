using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Users;
using Mydemenageur.API.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mydemenageur.API.DP.Interface;

namespace Mydemenageur.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDPUser _dpUser;
        private readonly IMongoCollection<User> _users;
        private readonly IMydemenageurSettings _mydemenageurSettings;

        public AuthenticationService(IDPUser dpUser, IMydemenageurSettings mydemenageurSettings)
        {
            _dpUser = dpUser;
            _users = _dpUser.Obtain();

            _mydemenageurSettings = mydemenageurSettings;
        }

        public async Task<User> LoginAsync(string username, string password)
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

            var update = Builders<User>.Update
                .Set(dbUser => dbUser.LastConnection, DateTime.Now);

            await _users.UpdateOneAsync(dbUser =>
                dbUser.Id == user.Id,
                update
            );

            return user;
        }

        public async Task<User> RegisterAsync(RegisterModel registerModel)
        {
            // We need some basic checks
            if (string.IsNullOrWhiteSpace(registerModel.Email)) { throw new Exception("An email is required for the registration");  }
            if (string.IsNullOrWhiteSpace(registerModel.Username)) { throw new Exception("A username is required for the registration"); }
            if (UserExist(registerModel.Email, registerModel.Username)) { throw new Exception("The user already exist"); }
            if(registerModel.Role.Equals("Admin")) { throw new UnauthorizedAccessException("You are not authorized to register with this role"); }

            // Password stuff, to ensure we never have clear password stored
            CreatePasswordHash(registerModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // Now we register the user in the database
            User dbUser = new()
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,

                Email = registerModel.Email.ToLower(),
                Phone = registerModel.Phone,
                Username = registerModel.Username.ToLower(),
                Gender = registerModel.Gender,
                Role = registerModel.Role,
                About = registerModel.About,
                Birthday = registerModel.Birthday,
                SignupDate = DateTime.Now,

                PasswordHash = Convert.ToBase64String(passwordHash),
                PasswordSalt = passwordSalt
            };

            await _users.InsertOneAsync(dbUser);

            return dbUser;
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
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }


}
