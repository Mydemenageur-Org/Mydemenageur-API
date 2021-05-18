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

        public Task RegisterSocietyAsync(SocietyRegisterModel societyRegisterModel)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSocietyAsync(string id, SocietyUpdateModel societyUpdateModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSocietyAsync(string id)
        {
            throw new NotImplementedException();
        }

        private Task<string> RegisterToDatabase(SocietyRegisterModel toRegister)
        {
            return null;
        }

        private bool SocietyExist(string societyId)
        {
            return _societiesService.AsQueryable<Society>().Any(dbSociety =>
                dbSociety.Id == societyId
            );
        }
    }
}
