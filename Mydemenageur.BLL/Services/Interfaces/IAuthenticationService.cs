﻿
using Mydemenageur.DAL.Models.Users;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<MyDemenageurUser> LoginAsync(string username, string password);
        Task<MyDemenageurUser> RegisterAsync(RegisterModel registerModel);
    }
}