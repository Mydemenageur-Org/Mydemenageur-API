using MongoDB.Driver;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Clients;
using Mydemenageur.API.Models.Users;
using Mydemenageur.API.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public class ClientsService : IClientsService
    {
        private readonly IMongoCollection<Client> _clients;
        private readonly IMongoCollection<User> _users;

        public ClientsService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _clients = database.GetCollection<Client>(mongoSettings.ClientsCollectionName);
            _users = database.GetCollection<User>(mongoSettings.UsersCollectionName);
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            var client = await _clients.FindAsync(client => true);
            return await client.ToListAsync();
        }

        public Task<Client> GetClientAsync(string id)
        {
            return GetClientFromIdAsync(id);
        }

        public async Task<User> GetUserAsync(string id)
        {
            var client = await GetClientFromIdAsync(id);

            if (client == null) { return null; }

            var user = await (await _users.FindAsync(dbUser =>
                dbUser.Id == client.UserId
            )).FirstOrDefaultAsync();

            return user;
        }

        public async Task<string> RegisterClientAsync(ClientRegisterModel toRegister)
        {
            if (!UserExist(toRegister.UserId)) throw new Exception("The user doesn't exist");
            if (ClientExist(toRegister.UserId)) throw new Exception("The client already exist");

            string id = await RegisterToDatabase(toRegister);

            return id;

        }

        public async Task UpdateClientAsync(string currentUserId, string id, ClientUpdateModel toUpdate)
        {
            var client = await GetClientFromIdAsync(id);

            if (client == null) throw new Exception("The client doesn't exist");
            if (client.UserId != currentUserId) throw new UnauthorizedAccessException("You are not allowed to update this client");

            await UpdateClientFromAdminAsync(id, toUpdate);
        }

        public async Task UpdateClientFromAdminAsync(string id, ClientUpdateModel toUpdate)
        {
            var update = Builders<Client>.Update
                .Set(dbClient => dbClient.Address, toUpdate.Address)
                .Set(dbClient => dbClient.Town, toUpdate.Town)
                .Set(dbClient => dbClient.Zipcode, toUpdate.Zipcode)
                .Set(dbClient => dbClient.Country, toUpdate.Country);

            await _clients.UpdateOneAsync(dbClient =>
                dbClient.Id == id,
                update
            );
        }

        private async Task<Client> GetClientFromIdAsync(string id)
        {
            var client = await _clients.FindAsync(dbClient =>
                dbClient.Id == id
            );

            return await client.FirstOrDefaultAsync();
        }

        private async Task<Client> GetClientFromUserIdAsyn(string userId)
        {
            var client = await _clients.FindAsync(dbClient =>
                dbClient.UserId == userId
            );

            return await client.FirstOrDefaultAsync();
        }

        private async Task<string> RegisterToDatabase(ClientRegisterModel toRegister)
        {
            Client dbClient = new()
            {
                UserId = toRegister.UserId
            };

            await _clients.InsertOneAsync(dbClient);

            return dbClient.Id;
        }

        private bool ClientExist(string userId)
        {
            return _clients.AsQueryable<Client>().Any(dbClient =>
                dbClient.UserId == userId
            );
        }

        private bool UserExist(string userId)
        {
            return _users.AsQueryable<User>().Any(dbUser =>
                dbUser.Id == userId
            );
        }
    }
}
