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

namespace Mydemenageur.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<PastAction> _pastActions;

        public UsersService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _users = database.GetCollection<User>(mongoSettings.UsersCollectionName);
            _pastActions = database.GetCollection<PastAction>(mongoSettings.PastActionsCollectionName);
        }
        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _users.FindAsync(users => true);
            return await users.ToListAsync();
        }
        public async Task<User> GetUserAsync(string id)
        {
            var user = await _users.FindAsync(databaseUser =>
                databaseUser.Id == id
            );

            return await user.FirstOrDefaultAsync();
        }

        public async Task<List<PastAction>> GetPastActionListFromUserAsync(string id)
        {
            var pastAction = await _pastActions.FindAsync(dbPastAction => dbPastAction.UserId == id);

            return await pastAction.ToListAsync();
        }

        public async Task UpdateUserAsync(string id, UserUpdateModel toUpdate)
        {
            var user = await GetUserAsync(id);

            if (user == null) throw new Exception("The user doesn't exist");

            var update = Builders<User>.Update
                .Set(dbUser => dbUser.FirstName, toUpdate.FirstName)
                .Set(dbUser => dbUser.LastName, toUpdate.LastName)
                .Set(dbUser => dbUser.Username, toUpdate.Username)
                .Set(dbUser => dbUser.Email, toUpdate.Email)
                .Set(dbUser => dbUser.Phone, toUpdate.Phone)
                .Set(dbUser => dbUser.About, toUpdate.About);

            await _users.UpdateOneAsync(dbUser =>
                dbUser.Id == id,
                update
            );
        }
    }
}
