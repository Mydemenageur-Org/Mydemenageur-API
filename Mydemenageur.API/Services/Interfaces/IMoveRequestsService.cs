using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.MoveRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IMoveRequestsService
    {

        Task<List<MoveRequest>> GetMoveRequestAsync();

        Task<MoveRequest> GetMoveRequestAsync(string id);

        Task<List<Housing>> GetAllHousingAssociate(string id);

        Task<string> RegisterMoveRequestAsync(MoveRequestRegisterModel moveRequestRegisterModel);

        Task UpdateMoveRequestAsync(string currentUserId, string id, MoveRequestUpdateModel moveRequestUpdateModel);

        Task DeleteMoveRequestModel(string id, string userId);

    }
}
