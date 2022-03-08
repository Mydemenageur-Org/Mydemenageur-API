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
            var myDemUser = await _dpMyDemenageurUser.Obtain().Where(w => w.Id == id).FirstOrDefaultAsync();
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
                MyDemenageurUser = myDemUser,
                ServicesProposed = profil.ServicesProposed,
                DiplomaOrExperiences = profil.DiplomaOrExperiences,
                Description = profil.Description,
                Commitment = profil.Commitment,
                ProStatus = profil.ProStatus,
                City = city,
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

        public async Task<string> UpdateUserRole(string id, MyDemenageurUserRole data)
        {
            MyDemenageurUser myDemUser = await _dpMyDemenageurUser.GetUserById(id).FirstOrDefaultAsync();

            if (myDemUser == null) throw new Exception("MyDemenageurUser does not exist");

            var update = Builders<MyDemenageurUser>.Update
                .Set(user => user.Role, data.Role)
                .Set(user => user.RoleType, data.RoleType);

            await _dpMyDemenageurUser.GetCollection().UpdateOneAsync(user => user.Id == id, update);
            
            return "Role update done";
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

        public async Task<int> GetTotalTokens(string id)
        {
            var user = await _dpMyDemenageurUser.GetUserById(id).FirstOrDefaultAsync();

            return user.FreeTokens + user.PaidTokens;
        }

        public async Task<string> RetrieveTokens(string id, MyDemenageurUserTokens tokens)
        {
            MyDemenageurUser myDemUser = await _dpMyDemenageurUser.GetUserById(id).FirstOrDefaultAsync();

            if (myDemUser == null) throw new Exception("MyDemenageurUser does not exist");

            if (myDemUser.PaidTokens + myDemUser.FreeTokens < tokens.Cost) 
                throw new Exception("Not enough tokens");
            
            // Retrieve on Paid tokens in priority
            var remainingTokens = myDemUser.PaidTokens - tokens.Cost;
            myDemUser.PaidTokens -= tokens.Cost - Math.Abs(remainingTokens);
            if (remainingTokens < 0)
                myDemUser.FreeTokens -= Math.Abs(remainingTokens);

            var update = Builders<MyDemenageurUser>.Update
                .Set(user => user.FreeTokens, myDemUser.FreeTokens)
                .Set(user => user.PaidTokens, myDemUser.PaidTokens);

            await _dpMyDemenageurUser.GetCollection().UpdateOneAsync(user => user.Id == id, update);
            
            return $"Successfully retrieved {tokens.Cost} tokens.";
        }
    }
}
