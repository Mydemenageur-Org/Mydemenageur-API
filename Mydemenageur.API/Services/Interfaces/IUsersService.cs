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

        Task<User> GetUserAsync(string id);
        Task UpdateUserAsync(string userId, UserUpdateModel toUpdate);

    }
}
