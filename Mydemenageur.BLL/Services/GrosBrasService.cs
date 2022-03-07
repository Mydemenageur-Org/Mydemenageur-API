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
        private readonly ICitiesService _citiesService;

        public GrosBrasService(IDPGrosBras dPGrosBras, IDPMyDemenageurUser dpMDUser, IDPCity dpCity, ICitiesService citiesService)
        {
            _dpGrosBras = dPGrosBras;
            _dpMDUser = dpMDUser;
            _dpCity = dpCity;
            _citiesService = citiesService;
        }

        public async Task<IList<GrosBrasPopulated>> GetGrosBras(IQueryCollection queryParams, int pageNumber = -1, int numberOfElementsPerPage = -1, string cityLabel = "")
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
                    MyDemenageurUser = myDem,
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
                    Rayon = profil.Rayon,
                    Title = profil.Title,
                    Formula = profil.Formula
                };
                grosBrasFinal.Add(grosBras);
            });

            if(cityLabel.Length > 1)
            {
                List<GrosBrasPopulated> grosBrasFinalFiltered = new List<GrosBrasPopulated>();
                grosBrasFinalFiltered = grosBrasFinal.FindAll(w => w.City.Label == cityLabel).ToList();

                return grosBrasFinalFiltered;
            }

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
                MyDemenageurUser = myDem,
                ServicesProposed = grosBrasProfil.ServicesProposed,
                DiplomaOrExperiences = grosBrasProfil.DiplomaOrExperiences,
                Description = grosBrasProfil.Description,
                Commitment = grosBrasProfil.Commitment,
                ProStatus = grosBrasProfil.ProStatus,
                City = city,
                Departement = grosBrasProfil.Departement,
                CreatedAt = grosBrasProfil.CreatedAt,
                UpdatedAt = grosBrasProfil.UpdatedAt,
                VeryGoodGrade = grosBrasProfil.VeryGoodGrade,
                GoodGrade = grosBrasProfil.GoodGrade,
                MediumGrade = grosBrasProfil.MediumGrade,
                BadGrade = grosBrasProfil.BadGrade,
                Rayon = grosBrasProfil.Rayon,
                Title = grosBrasProfil.Title,
                Formula = grosBrasProfil.Formula
            };

            return grosBras;
        }

        public async Task<IList<GrosBrasPopulated>> GetRanking(int numberOfGrosBras)
        {
            List<GrosBrasPopulated> grosBrasFinal = new List<GrosBrasPopulated>();
            var cursor = _dpGrosBras.GetCollection().Find(new BsonDocument());

            cursor.Limit(numberOfGrosBras);

            cursor.ToListAsync().Result.ForEach((profil) =>
            {
                var city = _dpCity.GetCityById(profil.CityId).FirstOrDefault();
                var myDem = _dpMDUser.GetUserById(profil.MyDemenageurUserId).FirstOrDefault();
                GrosBrasPopulated grosBras = new GrosBrasPopulated
                {
                    Id = profil.Id,
                    MyDemenageurUser = myDem,
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
                    Rayon = profil.Rayon,
                    Title = profil.Title,
                    Formula = profil.Formula
                };
                grosBrasFinal.Add(grosBras);
            });

            return grosBrasFinal;
        }

        public async Task<string> CreateGrosBras(GrosBras grosBras, string cityName = null)
        {
            if (cityName != null)
            {
                var matchedCities = await _dpCity.GetCollection().FindAsync(c => c.Label.ToLower() == cityName.ToLower());
                if (matchedCities == null)
                {
                    City newCity = await _citiesService.CreateNewCity(cityName);
                    grosBras.CityId = newCity.Id;
                }
                else
                {
                    grosBras.CityId = matchedCities.First().Id;
                }
            }
            
            await _dpGrosBras.GetCollection().InsertOneAsync(grosBras);
            
            return grosBras.Id;
        }

        public async Task<string> UpdateGrosBras(GrosBras grosBras, string cityName = null)
        {
            if (grosBras.CityId == null && cityName != null)
            {
                var matchedCities = await _dpCity.GetCollection().FindAsync(c => c.Label.ToLower() == cityName.ToLower());
                if (matchedCities == null)
                {
                    City newCity = await _citiesService.CreateNewCity(cityName);
                    grosBras.CityId = newCity.Id;
                }
                else
                {
                    grosBras.CityId = matchedCities.First().Id;
                }
            }

            await _dpGrosBras.GetCollection().ReplaceOneAsync(g => g.Id == grosBras.Id, grosBras);

            return grosBras.Id;
        }
    }
}
