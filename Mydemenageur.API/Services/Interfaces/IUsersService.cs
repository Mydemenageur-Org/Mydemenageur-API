using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(string id);
        Task UpdateUserAsync(string id, UserUpdateModel toUpdate);
        Task<List<PastAction>> GetPastActionListFromUserAsync(string id);

    }
}
