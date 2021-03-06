using MongoDB.Driver;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Movers;
using Mydemenageur.API.Models.Users;
using Mydemenageur.API.Services.Interfaces;
using Mydemenageur.API.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services
{
    public class MoversService : IMoversService
    {
        private readonly IMongoCollection<Mover> _movers;
        private readonly IMongoCollection<User> _users;

        public MoversService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _movers = database.GetCollection<Mover>(mongoSettings.MoversCollectionName);
            _users = database.GetCollection<User>(mongoSettings.UsersCollectionName);
        }
        public async Task<List<Mover>> GetMoversAsync()
        {
            var movers = await _movers.FindAsync(Vehicles => true);
            return await movers.ToListAsync();
        }
        public Task<Mover> GetMoverAsync(string id)
        {
            return GetMoverFromIdAsync(id);
        }

        public async Task<User> GetUserAsync(string id)
        {
            var mover = await GetMoverFromIdAsync(id);

            if (mover == null) { return null; }

            var user = await(await _users.FindAsync(dbUser =>
                dbUser.Id == mover.UserId
            )).FirstOrDefaultAsync();

            return user;
        }

        public async Task UpdateMoverAsync(string currentUserId, string id, MoverUpdateModel toUpdate)
        {
            var mover = await GetMoverAsync(id);

            if (mover == null) throw new Exception("The mover doesn't exist");
            if (mover.UserId != currentUserId) throw new UnauthorizedAccessException("You are not authorize to update this mover");

            var update = Builders<Mover>.Update
                .Set(dbMover => dbMover.IsVIP, toUpdate.IsVIP)
                .Set(dbMover => dbMover.AverageCustomerRating, toUpdate.AverageCustomer);

            await _movers.UpdateOneAsync(dbMover =>
                dbMover.Id == id,
                update
            );
        }

        public async Task<string> RegisterMoverAsync(MoverRegisterModel toRegister)
        {
            if (!UserExist(toRegister.UserId)) throw new Exception("The user doesn't exist");
            if (MoverExist(toRegister.UserId)) throw new Exception("The mover already exist");

            string id = await RegisterToDatabase(toRegister);

            return id;

        }

        public async Task DeleteMoverFromAdminAsync(string id)
        {
            if (id != null)
            {
                if (!MoverExist(id)) throw new Exception("The mover doesn't exist");

                User user = await GetUserAsync(id);

                if (!UserExist(user.Id)) throw new Exception("The user doesn't exist");


                await _movers.DeleteOneAsync<Mover>(mover => mover.Id == id);
                await _users.DeleteOneAsync<User>(user => user.Id == user.Id);

            }
        }

        private async Task<string> RegisterToDatabase(MoverRegisterModel toRegister)
        {
            Mover dbMover = new()
            {
                UserId = toRegister.UserId,
                IsVIP = false,
                AverageCustomerRating = -1,
                FileIds = new()

            };

            await _movers.InsertOneAsync(dbMover);

            return dbMover.Id;
        }

        private bool MoverExist(string userId)
        {
            return _movers.AsQueryable<Mover>().Any(dbMover =>
                dbMover.UserId == userId
            );
        }

        private bool UserExist(string userId)
        {
            return _users.AsQueryable<User>().Any(dbUser =>
                dbUser.Id == userId
            );
        }
        private async Task<Mover> GetMoverFromIdAsync(string id)
        {
            var mover = await _movers.FindAsync(databaseClient =>
                databaseClient.Id == id
            );

            return await mover.FirstOrDefaultAsync();
        }


    }
}
