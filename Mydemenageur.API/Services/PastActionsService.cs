using MongoDB.Driver;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.PastAction;
using Mydemenageur.API.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public class PastActionsService : IPastActionsService
    {

        private readonly IMongoCollection<PastAction> _pastActions;
        private readonly IMongoCollection<User> _users;

        public PastActionsService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _pastActions = database.GetCollection<PastAction>(mongoSettings.PastActionsCollectionName);
            _users = database.GetCollection<User>(mongoSettings.UsersCollectionName);
        }

        public async Task<PastAction> GetPastActionAsync(string id)
        {
            var pastAction = await _pastActions.FindAsync(dbPastAction => dbPastAction.Id == id);

            return await pastAction.FirstOrDefaultAsync();
        }
        public async Task<List<PastAction>> GetPastActionListFromUserAsync(string userId)
        {
            var pastAction = await _pastActions.FindAsync(dbPastAction => dbPastAction.UserId == userId);

            return await pastAction.ToListAsync();
        }

        public async Task<User> GetUserAsync(string id)
        {
            var pastAction = await (await _pastActions.FindAsync<PastAction>(dbPastAction => dbPastAction.Id == id)).FirstOrDefaultAsync();

            if (pastAction == null) { return null; }

            var user = await(await _users.FindAsync(dbUser =>
               dbUser.Id == pastAction.UserId
            )).FirstOrDefaultAsync();

            return user;
        }

        public async Task<string> RegisterPastActionAsync(PastActionRegisterModel toRegister)
        {
            if (!UserExist(toRegister.UserId)) throw new Exception("The user doesn't exist");

            string id = await RegisterToDatabase(toRegister);

            return id;
        }

        private async Task<string> RegisterToDatabase(PastActionRegisterModel toRegister)
        {
            PastAction dbPastAction = new()
            {
                ActionIcon = toRegister.ActionIcon,
                Title = toRegister.Title,
                Description = toRegister.Description,
                UserId = toRegister.UserId
            };

            await _pastActions.InsertOneAsync(dbPastAction);

            return dbPastAction.Id;
        }

        private bool UserExist(string userId)
        {
            return _users.AsQueryable<User>().Any(dbUser =>
                dbUser.Id == userId
            );
        }
    }
}
