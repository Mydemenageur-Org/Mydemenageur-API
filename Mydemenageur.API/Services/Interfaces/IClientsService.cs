using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Clients;
using Mydemenageur.API.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IClientsService
    {
        Task<List<Client>> GetClientsAsync();
        Task<Client> GetClientAsync(string id);
        Task<User> GetUserAsync(string id);

        Task<string> RegisterClientAsync(ClientRegisterModel toRegister);

        Task UpdateClientAsync(string currentUserId, string id, ClientUpdateModel toUpdate);
        Task UpdateClientFromAdminAsync(string id, ClientUpdateModel toUpdate);
        Task DeleteClientFromAdminAsync(string id);
    }
}
