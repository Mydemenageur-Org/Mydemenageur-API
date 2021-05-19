using MongoDB.Driver;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Society;
using Mydemenageur.API.Services.Interfaces;
using Mydemenageur.API.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services
{
    public class SocietiesService : ISocietiesService
    {
        private readonly IMongoCollection<Society> _societiesService;
        private readonly IMongoCollection<Mover> _moversService;

        public SocietiesService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _societiesService = database.GetCollection<Society>(mongoSettings.SocietiesCollectionName);
        }

        public async Task<List<Mover>> GetAllMoverAsync(string id)
        {
            var moversInSociety = await _moversService.FindAsync<Mover>(mover => mover.SocietyId == id);
            return await moversInSociety.ToListAsync<Mover>();
        }

        public async Task<Society> GetSocietyAsync(string id)
        {
            var society = await _societiesService.FindAsync<Society>(society => society.Id == id);
            return await society.FirstOrDefaultAsync();
        }

        public async Task<string> RegisterSocietyAsync(SocietyRegisterModel societyRegisterModel)
        {
            string id = await RegisterToDatabase(societyRegisterModel);
            return id;
        }

        public async Task UpdateSocietyAsync(string id, SocietyUpdateModel societyUpdateModel)
        {
            var society = await GetSocietyAsync(id);

            if (society == null) throw new ArgumentException("The society doesn't exist", nameof(id));

            var update = Builders<Society>.Update
                .Set(dbUser => dbUser.SocietyName, societyUpdateModel.SocietyName)
                .Set(dbUser => dbUser.ManagerId, societyUpdateModel.ManagerId)
                .Set(dbUser => dbUser.Adress, societyUpdateModel.Adress)
                .Set(dbUser => dbUser.Town, societyUpdateModel.Town)
                .Set(dbUser => dbUser.Zipcode, societyUpdateModel.Zipcode)
                .Set(dbUser => dbUser.Country, societyUpdateModel.Country)
                .Set(dbUser => dbUser.Region, societyUpdateModel.Region);

            await _societiesService.UpdateOneAsync(dbSociety =>
                dbSociety.Id == id,
                update
            );
        }

        public async Task DeleteSocietyAsync(string id)
        {
            if(id != null)
            {
                await _societiesService.DeleteOneAsync<Society>(society => society.Id == id);
            }
        }

        private async Task<string> RegisterToDatabase(SocietyRegisterModel toRegister)
        {
            Society dbSociety = new()
            {
                SocietyName = toRegister.SocietyName,
                ManagerId = toRegister.ManagerId,
                Adress = toRegister.Adress,
                Town = toRegister.Town,
                Zipcode = toRegister.Zipcode,
                Country = toRegister.Country,
                Region = toRegister.Region

            };

            await _societiesService.InsertOneAsync(dbSociety);

            return dbSociety.Id;
        }
    }
}
