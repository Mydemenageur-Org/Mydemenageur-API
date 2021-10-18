using MongoDB.Driver;
using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.MoveRequest;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services
{
    public class MoveRequestsService : IMoveRequestsService
    {
        private readonly IMongoCollection<MoveRequest> _moveRequests;
        private readonly IMongoCollection<Housing> _housings;
        private readonly IMongoCollection<User> _users;

        public MoveRequestsService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _moveRequests = database.GetCollection<MoveRequest>(mongoSettings.MoveRequestsCollectionName);
            _housings = database.GetCollection<Housing>(mongoSettings.HousingsCollectionName);
            _users = database.GetCollection<User>(mongoSettings.UsersCollectionName);
        }
        public async Task<List<MoveRequest>> GetMoveRequestAsync()
        {
            var moveRequest = await _moveRequests.FindAsync<MoveRequest>(moveRequest => true);
            return await moveRequest.ToListAsync();
        }

        public async Task<MoveRequest> GetMoveRequestAsync(string id)
        {
            var moveRequest = await _moveRequests.FindAsync<MoveRequest>(moveRequest => moveRequest.Id == id);
            return await moveRequest.FirstOrDefaultAsync();
        }

        public async Task<List<Housing>> GetAllHousingAssociate(string id)
        {
            var housings = await _housings.FindAsync<Housing>(housings => housings.MoveRequestId == id);
            return await housings.ToListAsync();
        }

        public async Task<string> RegisterMoveRequestAsync(MoveRequestRegisterModel toRegister)
        {
            if (!UserExist(toRegister.UserId)) throw new Exception("The user doesn't exist");

            string id = await RegisterToDatabase(toRegister);
            return id;
        }

        public async Task UpdateMoveRequestAsync(string currentUserId, string id, MoveRequestUpdateModel moveRequestUpdateModel)
        {
            var moveRequest = await GetMoveRequestAsync(id);

            if (moveRequest == null) throw new ArgumentException("The move request doesn't exist", nameof(id));

            if (!UserExist(moveRequestUpdateModel.UserId)) throw new Exception("The user doesn't exist");

            if (moveRequest.UserId != currentUserId) throw new UnauthorizedAccessException("Your are not the user of this move request");

            var update = Builders<MoveRequest>.Update
                .Set(dbMoveRequest => dbMoveRequest.UserId, moveRequestUpdateModel.UserId)
                .Set(dbMoveRequest => dbMoveRequest.Title, moveRequestUpdateModel.Title)
                .Set(dbMoveRequest => dbMoveRequest.MoveRequestVolume, moveRequestUpdateModel.MoveRequestVolume)
                .Set(dbMoveRequest => dbMoveRequest.NeedFurnitures, moveRequestUpdateModel.NeedFurnitures)
                .Set(dbMoveRequest => dbMoveRequest.NeedAssembly, moveRequestUpdateModel.NeedAssembly)
                .Set(dbMoveRequest => dbMoveRequest.NeedDiassembly, moveRequestUpdateModel.NeedDiassembly)
                .Set(dbMoveRequest => dbMoveRequest.MinimumRequestDate, moveRequestUpdateModel.MinimumRequestDate)
                .Set(dbMoveRequest => dbMoveRequest.MaximumRequestDate, moveRequestUpdateModel.MaximumRequestDate)
                .Set(dbMoveRequest => dbMoveRequest.HeavyFurnitures, moveRequestUpdateModel.HeavyFurnitures)
                .Set(dbMoveRequest => dbMoveRequest.AdditionalInformation, moveRequestUpdateModel.AdditionalInformation);

            await _moveRequests.UpdateOneAsync(dbMoveRequest =>
                dbMoveRequest.Id == id,
                update
            );
        }

        public async Task DeleteMoveRequestModel(string id, string userId)
        {
            var moveRequest = await GetMoveRequestAsync(id);

            if (moveRequest == null) throw new Exception("The move request doesn't exist");

            if (moveRequest.UserId == userId) throw new UnauthorizedAccessException("Your are not the user of this move request");

            await _moveRequests.DeleteOneAsync<MoveRequest>(moveRequest => moveRequest.Id == id);

            var AllHousingsAssociate = GetAllHousingAssociate(id);

            foreach (Housing housingOfList in await AllHousingsAssociate)
            {
                await _housings.DeleteOneAsync<Housing>(housing => housing.Id == housingOfList.Id);
            }
        }

        private async Task<string> RegisterToDatabase(MoveRequestRegisterModel toRegister)
        {
            MoveRequest dbMoveRequest = new()
            {
                UserId = toRegister.UserId,
                Title = toRegister.Title,
                MoveRequestVolume = toRegister.MoveRequestVolume,
                NeedFurnitures = toRegister.NeedFurnitures,
                NeedAssembly = toRegister.NeedAssembly,
                NeedDiassembly = toRegister.NeedDiassembly,
                MinimumRequestDate = toRegister.MinimumRequestDate,
                HeavyFurnitures = toRegister.HeavyFurnitures,
                AdditionalInformation = toRegister.AdditionalInformation
            };

            await _moveRequests.InsertOneAsync(dbMoveRequest);

            return dbMoveRequest.Id;
        }

        private bool UserExist(string userId)
        {
            return _users.AsQueryable<User>().Any(dbUser =>
                dbUser.Id == userId
            );
        }

    }
}
