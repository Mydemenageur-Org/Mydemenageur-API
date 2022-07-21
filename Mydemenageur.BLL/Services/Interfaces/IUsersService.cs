using Mydemenageur.DAL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IUsersService
    {
        Task<MyDemenageurUser> GetUser(string id);
        Task<MyDemenageurUser> GetUserByEmail(string email);
        Task<IList<MyDemenageurUser>> GetUsers();
        Task<IList<MyDemenageurUser>> GetUsersFiltered(QueryString queryString, int pageNumber = -1, int numberOfElementsPerPage = -1);
        Task<byte[]> GetProfilePicture(string id);

        Task UpdateUser(byte[] profilePicture, string newPassword, MyDemenageurUser toUpdate);
        Task<GrosBrasPopulated> GetGrosBrasFromUserId(string id);
        Task<string> UpdateUserRole(string id, MyDemenageurUserRole data);
        Task<MyDemenageurUser> GetByStripeId(string id);
        Task<string> UpdateStripeId(string id, string stripeId);
        Task<int> GetTotalTokens(string id);
        Task<string> UpdateTokens(string id, MyDemenageurUserTokens tokens);
        Task UpdateNotif(string id, MyDemenageurUserNotif notif);
        Task DeleteUser(string id);
    }
}
