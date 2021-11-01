using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mydemenageur.DAL.DP.Interface;
using MongoDB.Driver.Linq;

namespace Mydemenageur.BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IDPMyDemenageurUser _dpMyDemenageurUser;

        private readonly IFilesService _filesService;

        public UsersService(IDPMyDemenageurUser dPMyDemenageurUser, IFilesService filesService)
        {
            _dpMyDemenageurUser = dPMyDemenageurUser;

            _filesService = filesService;
        }

        public async Task<MyDemenageurUser> GetUser(string id)
        {
            return await _dpMyDemenageurUser.GetUserById(id).FirstOrDefaultAsync();
        }

        public async Task<IList<MyDemenageurUser>> GetUsers()
        {
            IMongoQueryable<MyDemenageurUser> users = _dpMyDemenageurUser.Obtain();

            return await users.ToListAsync();
        }

        public async Task<byte[]> GetProfilePicture(string id)
        {
            MyDemenageurUser user = await _dpMyDemenageurUser.GetUserById(id).FirstOrDefaultAsync();

            if (user == null || user.ProfilePictureId == null) return null;

            return (await _filesService.GetFile(user.ProfilePictureId)).Data;
        }

        public async Task UpdateUser(byte[] profilePicture, string newPassword, MyDemenageurUser toUpdate)
        {
            MyDemenageurUser oldUser = await _dpMyDemenageurUser.GetUserById(toUpdate.Id).FirstOrDefaultAsync();

            if (oldUser == null)
            {
                throw new Exception("The user doesn't exist");
            }

            if (profilePicture != null)
            {
                string imageId = await _filesService.UploadFile(toUpdate.FirstName.ToLower().Trim(), profilePicture);

                toUpdate.ProfilePictureId = imageId;
            }
            else
            {
                toUpdate.ProfilePictureId = oldUser.ProfilePictureId;
            }

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                // TODO: update user's password
            }

            await _dpMyDemenageurUser.GetCollection().ReplaceOneAsync(
                dbUser => dbUser.Id == toUpdate.Id,
                toUpdate
            );
        }
    }
}
