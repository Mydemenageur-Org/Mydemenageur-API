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

        private readonly IDPCity _dpCity;

        private readonly IDPGrosBras _dPGrosBras;

        private readonly IDPUser _dpUser;

        private readonly IFilesService _filesService;

        public UsersService(IDPMyDemenageurUser dPMyDemenageurUser, IFilesService filesService, IDPUser dpUser, IDPGrosBras dPGrosBras, IDPCity dPCity)
        {
            _dpMyDemenageurUser = dPMyDemenageurUser;
            _dpUser = dpUser;
            _filesService = filesService;
            _dPGrosBras = dPGrosBras;
            _dpCity = dPCity;
        }

        public async Task<MyDemenageurUser> GetUser(string id)
        {
            return await _dpMyDemenageurUser.GetUserById(id).FirstOrDefaultAsync();
        }

        public async Task<GrosBrasPopulated> GetGrosBrasFromUserId(string id)
        {
            var myDemUser = await _dpMyDemenageurUser.Obtain().Where(w => w.UserId == id).FirstOrDefaultAsync();
            if(myDemUser == null)
            {
                throw new Exception("MyDemenageurUser does not exist");
            }

            var profil = await _dPGrosBras.Obtain().Where(w => w.MyDemenageurUserId == myDemUser.Id).FirstOrDefaultAsync();

            if(profil == null)
            {
                throw new Exception("Gros Bras does not exist");
            }

            var city = _dpCity.GetCityById(profil.CityId).FirstOrDefault();
            GrosBrasPopulated grosBras = new GrosBrasPopulated
            {
                Id = profil.Id,
                MyDemenageurUserId = myDemUser,
                ServicesProposed = profil.ServicesProposed,
                DiplomaOrExperiences = profil.DiplomaOrExperiences,
                Description = profil.Description,
                Commitment = profil.Commitment,
                ProStatus = profil.ProStatus,
                CityId = city,
                Departement = profil.Departement,
                CreatedAt = profil.CreatedAt,
                UpdatedAt = profil.UpdatedAt,
                VeryGoodGrade = profil.VeryGoodGrade,
                GoodGrade = profil.GoodGrade,
                MediumGrade = profil.MediumGrade,
                BadGrade = profil.BadGrade,
                Title = profil.Title
            };

            return grosBras;
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
            // This int check either the email toUpdate if it has changed does not exist in the database to avoid replicated email
            int myDemUsers = await _dpMyDemenageurUser.GetCollection().AsQueryable().Where(user => user.Email == toUpdate.Email).CountAsync();
            if (oldUser == null)
            {
                throw new Exception("The user doesn't exist");
            }
            if (toUpdate.Email != oldUser.Email && myDemUsers < 2)
            {
                User userAuth = await _dpUser.GetUserById(oldUser.UserId).FirstOrDefaultAsync();
                var update = Builders<User>.Update
                    .Set(user => user.Email, toUpdate.Email);
                await _dpUser.Obtain().UpdateOneAsync(user => user.Id == userAuth.Id, update);
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
