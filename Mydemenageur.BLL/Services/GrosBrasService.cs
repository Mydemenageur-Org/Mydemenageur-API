using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models;
using Mydemenageur.DAL.Models.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Mydemenageur.BLL.Helpers;

namespace Mydemenageur.BLL.Services
{
    public class GrosBrasService: IGrosBrasService
    {
        private readonly IDPGrosBras _dpGrosBras;
        private readonly IDPMyDemenageurUser _dpMDUser;
        private readonly IDPCity _dpCity;

        public GrosBrasService(IDPGrosBras dPGrosBras, IDPMyDemenageurUser dpMDUser, IDPCity dpCity)
        {
            _dpGrosBras = dPGrosBras;
            _dpMDUser = dpMDUser;
            _dpCity = dpCity;
        }

        public async Task<IList<GrosBrasPopulated>> GetGrosBras(IQueryCollection queryParams, int pageNumber = -1, int numberOfElementsPerPage = -1)
        {
            List<GrosBrasPopulated> grosBrasFinal = new List<GrosBrasPopulated>();

            var grosBras = await _dpGrosBras.GetCollection().FilterByQueryParamsMongo(queryParams, pageNumber, numberOfElementsPerPage);

            grosBras.ForEach((profil) =>
            {
                var city = _dpCity.GetCityById(profil.CityId).FirstOrDefault();
                var myDem= _dpMDUser.GetUserById(profil.MyDemenageurUserId).FirstOrDefault();
                GrosBrasPopulated grosBras = new GrosBrasPopulated
                {
                    Id = profil.Id,
                    MyDemenageurUserId = myDem,
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
                    BadGrade = profil.BadGrade
                };
                grosBrasFinal.Add(grosBras);
            });

            return grosBrasFinal;
        }

        public long CountGrosBras()
        {
            long grosBrasNumber = _dpGrosBras.Obtain().ToList().Count();
            return  grosBrasNumber;
        }

        public async Task<GrosBrasPopulated> GetGrosBrasById(string id)
        {
            GrosBras grosBrasProfil = await _dpGrosBras.GetGrosBrasById(id).FirstOrDefaultAsync();
            if(grosBrasProfil == null)
            {
                return null;
            }

            var city = _dpCity.GetCityById(grosBrasProfil.CityId).FirstOrDefault();
            var myDem = _dpMDUser.GetUserById(grosBrasProfil.MyDemenageurUserId).FirstOrDefault();
            GrosBrasPopulated grosBras = new GrosBrasPopulated
            {
                Id = grosBrasProfil.Id,
                MyDemenageurUserId = myDem,
                ServicesProposed = grosBrasProfil.ServicesProposed,
                DiplomaOrExperiences = grosBrasProfil.DiplomaOrExperiences,
                Description = grosBrasProfil.Description,
                Commitment = grosBrasProfil.Commitment,
                ProStatus = grosBrasProfil.ProStatus,
                CityId = city,
                Departement = grosBrasProfil.Departement,
                CreatedAt = grosBrasProfil.CreatedAt,
                UpdatedAt = grosBrasProfil.UpdatedAt,
                VeryGoodGrade = grosBrasProfil.VeryGoodGrade,
                GoodGrade = grosBrasProfil.GoodGrade,
                MediumGrade = grosBrasProfil.MediumGrade,
                BadGrade = grosBrasProfil.BadGrade
            };

            return grosBras;
        }

        public string CreateGrosBras(string myDemUserId)
        {
            return myDemUserId;
        }

    }
}
