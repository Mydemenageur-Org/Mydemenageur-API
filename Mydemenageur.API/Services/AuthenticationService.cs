using MongoDB.Driver;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Users;
using Mydemenageur.API.Services.Interfaces;
using Mydemenageur.API.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMongoCollection<User> _users;

        public AuthenticationService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _users = database.GetCollection<User>("users");
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

            // TODO: generate the JWT token

            return user;
        }

        public async Task<string> RegisterAsync(RegisterModel registerModel)
        {
            // We need some basic checks
            if (string.IsNullOrWhiteSpace(registerModel.Email)) { throw new Exception("An email is required for the registration");  }
            if (string.IsNullOrWhiteSpace(registerModel.Username)) { throw new Exception("A username is required for the registration"); }
            if (UserExist(registerModel.Email, registerModel.Username)) { throw new Exception("The user already exist"); }

            // Password stuff, to ensure we never have clear password stored
            CreatePasswordHash(registerModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // Now we register the user in the database
            User dbUser = new User()
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,

                Email = registerModel.Email.ToLower(),
                Phone = registerModel.Phone,
                Username = registerModel.Username.ToLower(),

                PasswordHash = Convert.ToBase64String(passwordHash),
                PasswordSalt = passwordSalt
            };

            await _users.InsertOneAsync(dbUser);

            return dbUser.Id;
        }

        private bool UserExist(string email, string username)
        {
            return _users.AsQueryable().Any(dbUser =>
                dbUser.Email == email.ToLower() ||
                dbUser.Username == username.ToLower()
            );
        }

        // Password related functions
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password)) { throw new Exception("The password must not be null or empty"); }

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, string storedHash, byte[] storedSalt)
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
    }
}
