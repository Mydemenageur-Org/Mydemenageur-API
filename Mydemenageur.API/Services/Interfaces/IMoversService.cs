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

        Task<Mover> GetMoverAsync(string id);
        Task<User> GetUserAsync(string id);
        Task<string> RegisterMoverAsync(MoverRegisterModel toRegister);
        Task UpdateMoverAsync(string id, MoverUpdateModel toUpdate);
        Task DeleteMover(string moverId, string userId);
        

    }
}
