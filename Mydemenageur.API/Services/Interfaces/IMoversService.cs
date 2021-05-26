using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Movers;
using Mydemenageur.API.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IMoversService
    {
        Task<List<Mover>> GetMoversAsync();
        Task<Mover> GetMoverAsync(string id);
        Task<User> GetUserAsync(string id);
        Task<string> RegisterMoverAsync(MoverRegisterModel toRegister);
        Task UpdateMoverAsync(string currentUserId, string id, MoverUpdateModel toUpdate);
        Task DeleteMover(string id, string userId);
        

    }
}
