using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> LoginAsync(string username, string password);
        Task<string> RegisterAsync(RegisterModel registerModel);
    }
}
