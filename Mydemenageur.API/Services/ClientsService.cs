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

        public ClientsService(IMongoSettings mongoSettings, IMydemenageurSettings mydemenageurSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _clients = database.GetCollection<Client>(mongoSettings.ClientsCollectionName);
            _users = database.GetCollection<User>(mongoSettings.UsersCollectionName);
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

        public async Task UpdateClientAsync(string id, ClientUpdateModel toUpdate)
        {
            var client = await GetClientAsync(id);

            if (client == null) throw new ArgumentException("The client doesn't exist", "clientId");

            var update = Builders<Client>.Update
                .Set(dbClient => dbClient.Adress, toUpdate.Adress)
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
            var client = await _clients.FindAsync(databaseClient =>
                databaseClient.Id == id
            );

            return await client.FirstOrDefaultAsync();
        }
    }
}
