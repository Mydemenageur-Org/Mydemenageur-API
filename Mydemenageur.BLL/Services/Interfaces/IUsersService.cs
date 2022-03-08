using Mydemenageur.DAL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IUsersService
    {
        Task<MyDemenageurUser> GetUser(string id);
        Task<IList<MyDemenageurUser>> GetUsers();

        Task<byte[]> GetProfilePicture(string id);

        Task UpdateUser(byte[] profilePicture, string newPassword, MyDemenageurUser toUpdate);
        Task<GrosBrasPopulated> GetGrosBrasFromUserId(string id);
        Task<string> UpdateUserRole(string id, MyDemenageurUserRole data);
        Task<int> GetTotalTokens(string id);
        Task<string> RetrieveTokens(string id, MyDemenageurUserTokens tokens);
    }
}
