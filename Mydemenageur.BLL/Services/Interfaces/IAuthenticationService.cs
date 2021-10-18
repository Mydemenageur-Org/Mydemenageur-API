using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> LoginAsync(string username, string password);
        Task<User> RegisterAsync(RegisterModel registerModel);
    }
}
