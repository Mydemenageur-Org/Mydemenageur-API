using MongoDB.Driver;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Housing;
using Mydemenageur.API.Services.Interfaces;
using Mydemenageur.API.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services
{
    public class HousingsService : IHousingsService
    {
        private readonly IMongoCollection<Housing> _housings;
        private readonly IMongoCollection<MoveRequest> _moveRequests;

        public HousingsService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _housings = database.GetCollection<Housing>(mongoSettings.HousingsCollectionName);
            _moveRequests = database.GetCollection<MoveRequest>(mongoSettings.MoveRequestsCollectionName);
        }

        public async Task<List<Housing>> GetHousingAsync()
        {
            var housings = await _housings.FindAsync(housing => true);
            return await housings.ToListAsync();
        }

        public async Task<Housing> GetHousingAsync(string id)
        {
            var housings = await _housings.FindAsync<Housing>(housing => housing.Id == id);
            return await housings.FirstOrDefaultAsync();
        }

        public async Task<string> RegisterHousingAsync(HousingRegisterModel housingRegisterModel)
        {
            string id = await RegisterToDatabase(housingRegisterModel);
            return id;
        }

        public async Task UpdateHousingAsync(string currentUserId, string id, HousingUpdateModel housingUpdateModel)
        {
            var housing = await GetHousingAsync(id);

            if (housing == null) throw new Exception("The housing doesn't exist");
            if (housing.UserId != currentUserId) throw new UnauthorizedAccessException("Your are not allowed to update this housing");

            var update = Builders<Housing>.Update
                .Set(dbHousing => dbHousing.HousingType, housingUpdateModel.HousingType)
                .Set(dbHousing => dbHousing.HousingFloor, housingUpdateModel.HousingFloor)
                .Set(dbHousing => dbHousing.IsElevator, housingUpdateModel.IsElevator)
                .Set(dbHousing => dbHousing.Surface, housingUpdateModel.Surface)
                .Set(dbHousing => dbHousing.Adress, housingUpdateModel.Adress)
                .Set(dbHousing => dbHousing.Town, housingUpdateModel.Town)
                .Set(dbHousing => dbHousing.Zipcode, housingUpdateModel.Zipcode)
                .Set(dbHousing => dbHousing.Region, housingUpdateModel.Region)
                .Set(dbHousing => dbHousing.Country, housingUpdateModel.Country)
                .Set(dbHousing => dbHousing.State, housingUpdateModel.State)
                .Set(dbHousing => dbHousing.MoveRequestId, housingUpdateModel.MoveRequestId);

            await _housings.UpdateOneAsync(dbHousing =>
                dbHousing.Id == id,
                update
            );
        }

        public async Task DeleteHousingAsync(string id, string userId)
        {
            var housing = await GetHousingAsync(id);
            var moveRequest = await (await _moveRequests.FindAsync<MoveRequest>(moveRequest => moveRequest.Id == housing.MoveRequestId)).FirstOrDefaultAsync();

            if (housing == null) throw new Exception("The housing doesn't exist");

            if (moveRequest.UserId == userId) throw new UnauthorizedAccessException("Your are not the user of this housing");

            await _housings.DeleteOneAsync<Housing>(housing => housing.Id == id);
        }

        private async Task<string> RegisterToDatabase(HousingRegisterModel toRegister)
        {
            Housing dbHousing = new()
            {
                HousingType = toRegister.HousingType,
                HousingFloor = toRegister.HousingFloor,
                IsElevator = toRegister.IsElevator,
                Surface = toRegister.Surface,
                Adress = toRegister.Adress,
                Town = toRegister.Town,
                Zipcode = toRegister.Zipcode,
                Region = toRegister.Region,
                Country = toRegister.Country,
                State = toRegister.State,
                MoveRequestId = toRegister.MoveRequestId,
            };

            await _housings.InsertOneAsync(dbHousing);

            return dbHousing.Id;
        }

        private async Task<MoveRequest> GetMoveRequestFromIdAsync(string moveRequestId)
        {
            var moveRequest = await _moveRequests.FindAsync(dbMoveRequest =>
                dbMoveRequest.Id == moveRequestId
            );

            return await moveRequest.FirstOrDefaultAsync();
        }
    }
}
