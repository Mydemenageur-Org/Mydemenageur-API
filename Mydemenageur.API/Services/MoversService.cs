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
    public class MoversService : IMoversService
    {
        private readonly IMongoCollection<Mover> _movers;

        private readonly IMydemenageurSettings _mydemenageurSettings;

        public MoversService(IMongoSettings mongoSettings, IMydemenageurSettings mydemenageurSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _movers = database.GetCollection<Mover>("movers");

            _mydemenageurSettings = mydemenageurSettings;
        }
        public async Task<Mover> GetMoverAsync(string id)
        {
            var mover = await _movers.FindAsync(databaseClient =>
                databaseClient.Id == id
            );

            return await mover.FirstOrDefaultAsync();
        }

        public async Task UpdateMoverAsync(string moverId, MoverUpdateModel toUpdate)
        {
            var mover = await GetMoverAsync(moverId);

            if (mover == null) throw new ArgumentException("The mover doesn't exist", "moverId");

            var update = Builders<Mover>.Update
                .Set(dbMover => dbMover.Adress, toUpdate.Adress)
                .Set(dbMover => dbMover.Town, toUpdate.Town)
                .Set(dbMover => dbMover.Zipcode, toUpdate.Zipcode)
                .Set(dbMover => dbMover.Country, toUpdate.Country)
                .Set(dbMover => dbMover.Region, toUpdate.Region)
                .Set(dbMover => dbMover.VIP, toUpdate.VIP)
                .Set(dbMover => dbMover.SocietyId, toUpdate.SocietyId)
                .Set(dbMover => dbMover.AverageCustomer, toUpdate.AverageCustomer);

            await _movers.UpdateOneAsync(dbMover =>
                dbMover.Id == moverId,
                update
            );
        }
    }
}
