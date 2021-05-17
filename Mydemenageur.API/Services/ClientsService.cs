using MongoDB.Driver;
using Mydemenageur.API.Entities;
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

        private readonly IMydemenageurSettings _mydemenageurSettings;

        public ClientsService(IMongoSettings mongoSettings, IMydemenageurSettings mydemenageurSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _clients = database.GetCollection<Client>("clients");

            _mydemenageurSettings = mydemenageurSettings;
        }

        public async Task<Client> GetClientAsync(string id)
        {
            var client = await _clients.FindAsync(databaseClient =>
                databaseClient.Id == id
            );

            return await client.FirstOrDefaultAsync();
        }

        public async Task UpdateClientAsync(string clientId, ClientUpdateModel toUpdate)
        {
            var client = await GetClientAsync(clientId);

            if (client == null) throw new ArgumentException("The client doesn't exist", "clientId");

            var update = Builders<Client>.Update
                .Set(dbClient => dbClient.Adress, toUpdate.Adress)
                .Set(dbClient => dbClient.Town, toUpdate.Town)
                .Set(dbClient => dbClient.Zipcode, toUpdate.Zipcode)
                .Set(dbClient => dbClient.Country, toUpdate.Country);

            await _clients.UpdateOneAsync(dbClient =>
                dbClient.Id == clientId,
                update
            );
        }
    }
}
