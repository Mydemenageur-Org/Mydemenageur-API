using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Users;
using Mydemenageur.API.Services.Interfaces;
using Mydemenageur.API.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.UserRelated
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        private readonly IMydemenageurSettings _mydemenageurSettings;

        public UserService(IMongoSettings mongoSettings, IMydemenageurSettings mydemenageurSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _users = database.GetCollection<User>("users");

            _mydemenageurSettings = mydemenageurSettings;
        }

        public User GetUser(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public void UpdateUser(string currentUserId, string userId, User toUpdate)
        {
            _users.ReplaceOne(user => user.Id == userId, toUpdate);
        }
    }
}
