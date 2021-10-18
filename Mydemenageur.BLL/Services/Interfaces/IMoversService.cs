using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Movers;
using Mydemenageur.DAL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IMoversService
    {
        Task<List<Mover>> GetMoversAsync();
        Task<Mover> GetMoverAsync(string id);
        Task<User> GetUserAsync(string id);
        Task<string> RegisterMoverAsync(MoverRegisterModel toRegister);
        Task UpdateMoverAsync(string currentUserId, string id, MoverUpdateModel toUpdate);
        Task DeleteMoverFromAdminAsync(string id);


    }
}
