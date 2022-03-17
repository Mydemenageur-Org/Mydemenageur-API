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
        
        private readonly IDPGenericService _dpGenericService;

        private readonly IDPDemand _dpDemand;

        public UsersService(IDPMyDemenageurUser dPMyDemenageurUser, IFilesService filesService, IDPUser dpUser, IDPGrosBras dPGrosBras, IDPCity dPCity, IDPGenericService dpGenericService, IDPDemand dpDemand)
        {
            _dpMyDemenageurUser = dPMyDemenageurUser;
            _dpUser = dpUser;
            _filesService = filesService;
            _dPGrosBras = dPGrosBras;
            _dpCity = dPCity;
            _dpGenericService = dpGenericService;
            _dpDemand = dpDemand;
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
                VeryGoodGrade = profil.VeryGoodGrade.ToString(),
                GoodGrade = profil.GoodGrade.ToString(),
                MediumGrade = profil.MediumGrade.ToString(),
                BadGrade = profil.BadGrade.ToString(),
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

        public async Task<string> UpdateTokens(string id, MyDemenageurUserTokens tokens)
        {
            MyDemenageurUser myDemUser = await _dpMyDemenageurUser.GetUserById(id).FirstOrDefaultAsync();

            if (myDemUser == null) throw new Exception("MyDemenageurUser does not exist");

            tokens.Value = Math.Abs(tokens.Value); // Security shit
            
;           UpdateDefinition<MyDemenageurUser> update = null;
            switch (tokens.Operation)
            {
                case "take":
                    if (myDemUser.PaidTokens + myDemUser.FreeTokens < tokens.Value) 
                        throw new Exception("Not enough tokens");
            
                    // Retrieve on Paid tokens in priority
                    var remainingTokens = myDemUser.PaidTokens - tokens.Value;
                    myDemUser.PaidTokens -= tokens.Value - Math.Abs(remainingTokens);
                    if (remainingTokens < 0)
                        myDemUser.FreeTokens -= Math.Abs(remainingTokens);

                    update = Builders<MyDemenageurUser>.Update
                        .Set(user => user.FreeTokens, myDemUser.FreeTokens)
                        .Set(user => user.PaidTokens, myDemUser.PaidTokens);
                    break;
                case "add":
                    update = Builders<MyDemenageurUser>.Update
                        .Inc(user => user.PaidTokens, tokens.Value);
                    break;
                default:
                    throw new Exception("No valid operations set");
            }

            await _dpMyDemenageurUser.GetCollection().UpdateOneAsync(user => user.Id == id, update);
            
            return $"Successfully {tokens.Operation} {tokens.Value} tokens.";
        }
        
        public async Task DeleteUser(string id)
        {
            await _dpMyDemenageurUser.GetCollection().DeleteOneAsync(user => user.Id == id);
            await _dPGrosBras.GetCollection().DeleteOneAsync(grosBras => grosBras.MyDemenageurUserId == id);
            await _dpGenericService.GetCollection().DeleteOneAsync(genericService => genericService.UserId == id);
            await _dpDemand.GetCollection()
                .DeleteOneAsync(demand => demand.Sender.Id == id || demand.Recipient.Id == id);
        }
    }
}
