using MongoDB.Driver;
using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Society;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services
{
    public class SocietiesService : ISocietiesService
    {
        private readonly IMongoCollection<Society> _societies;
        private readonly IMongoCollection<User> _users;

        public SocietiesService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _societies = database.GetCollection<Society>(mongoSettings.SocietiesCollectionName);
            _users = database.GetCollection<User>(mongoSettings.UsersCollectionName);
        }

        public async Task<Society> GetSocietyAsync(string id)
        {
            var society = await _societies.FindAsync<Society>(society => society.Id == id);
            return await society.FirstOrDefaultAsync();
        }
        public async Task<List<Society>> GetSocietiesAsync()
        {
            var societies = await _societies.FindAsync(societies => true);
            return await societies.ToListAsync();
        }
        public async Task<string> RegisterSocietyAsync(SocietyRegisterModel toRegister)
        {
            if (!UserExist(toRegister.ManagerId)) throw new Exception("The user doesn't exist");

            string id = await RegisterToDatabase(toRegister);
            return id;
        }

        public async Task UpdateSocietyAsync(string currentUserId, string id, SocietyUpdateModel societyUpdateModel)
        {
            var society = await GetSocietyAsync(id);

            if (society == null) throw new Exception("The society doesn't exist");
            if (society.ManagerId == currentUserId) throw new UnauthorizedAccessException("You are not authorize to update this society");

            var update = Builders<Society>.Update
                .Set(dbSociety => dbSociety.SocietyName, societyUpdateModel.SocietyName)
                .Set(dbSociety => dbSociety.ManagerId, societyUpdateModel.ManagerId)
                .Set(dbSociety => dbSociety.EmployeeNumber, societyUpdateModel.EmployeeNumber)
                .Set(dbSociety => dbSociety.Address, societyUpdateModel.Address)
                .Set(dbSociety => dbSociety.Town, societyUpdateModel.Town)
                .Set(dbSociety => dbSociety.Zipcode, societyUpdateModel.Zipcode)
                .Set(dbSociety => dbSociety.Country, societyUpdateModel.Country)
                .Set(dbSociety => dbSociety.Region, societyUpdateModel.Region);

            await _societies.UpdateOneAsync(dbSociety =>
                dbSociety.Id == id,
                update
            );
        }

        public async Task DeleteSocietyAsync(string id)
        {
            if (id != null)
            {
                var society = await GetSocietyAsync(id);

                if (society == null) throw new Exception("The user doesn't exist");

                await _societies.DeleteOneAsync<Society>(society => society.Id == id);

            }
        }

        private async Task<string> RegisterToDatabase(SocietyRegisterModel toRegister)
        {
            Society dbSociety = new()
            {
                SocietyName = toRegister.SocietyName,
                ManagerId = toRegister.ManagerId,
                EmployeeNumber = toRegister.EmployeeNumber,
                Address = toRegister.Address,
                Town = toRegister.Town,
                Zipcode = toRegister.Zipcode,
                Country = toRegister.Country,
                Region = toRegister.Region
            };

            await _societies.InsertOneAsync(dbSociety);

            return dbSociety.Id;
        }

        private bool UserExist(string userId)
        {
            return _users.AsQueryable<User>().Any(dbUser =>
                dbUser.Id == userId
            );
        }
    }
}
