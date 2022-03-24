using System;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.DAL.Models.Demands;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;
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
        private readonly IDPDemand _dpDemand;

        public GrosBrasService(IDPGrosBras dPGrosBras, IDPMyDemenageurUser dpMDUser, IDPCity dpCity, ICitiesService citiesService, IDPDemand dpDemand)
        {
            _dpGrosBras = dPGrosBras;
            _dpMDUser = dpMDUser;
            _dpCity = dpCity;
            _citiesService = citiesService;
            _dpDemand = dpDemand;
        }

        public async Task<IList<GrosBrasPopulated>> GetGrosBras(QueryString queryString, int pageNumber = -1, int numberOfElementsPerPage = -1, string cityLabel = "")
        {
            List<GrosBrasPopulated> grosBrasFinal = new List<GrosBrasPopulated>();
            // Sort by reviews count
            var sortDefinition = new SortDefinitionBuilder<GrosBras>().Descending("VeryGoodGrade");
            var dictionary = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString.Value); 
            List<GrosBras> grosBras = new List<GrosBras>();
            if (dictionary.TryGetValue("cityLabel", out StringValues values))
            {
                var department = values.FirstOrDefault().Split('-').LastOrDefault();
                // Temporary system to find by department
                if (Int32.TryParse(department, out _)) {
                    List<City> cities = _dpCity.GetCollection().FindAsync(c => c.Departement == department).Result.ToList();

                    foreach (var city in cities)
                    {
                        if (numberOfElementsPerPage > 0 && grosBras.Count >= numberOfElementsPerPage) break;
                        dictionary["CityId"] = city.Id;
    
                        IQueryCollection queryParams = new QueryCollection(dictionary);
                        grosBras.AddRange(await _dpGrosBras.GetCollection().FilterByQueryParamsMongo(queryParams, pageNumber, numberOfElementsPerPage-grosBras.Count, sortDefinition));
                    }
                }
                else
                {
                    var city = (await _dpCity.GetCollection().FindAsync(c => c.Label.ToLower() == values[0].ToLower())).FirstOrDefault();
                    dictionary.Add("CityId", city.Id);
                    
                    grosBras = await _dpGrosBras.GetCollection().FilterByQueryParamsMongo(new QueryCollection(dictionary), pageNumber, numberOfElementsPerPage, sortDefinition);
                }
            }
            else
            {
                grosBras = await _dpGrosBras.GetCollection().FilterByQueryParamsMongo(new QueryCollection(dictionary), pageNumber, numberOfElementsPerPage, sortDefinition);
            }

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
                    Description = Censure.All(profil.Description),
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
                    Rayon = profil.Rayon,
                    Title = profil.Title
                };
                grosBrasFinal.Add(grosBras);
            });

            return grosBrasFinal;
        }

        public async Task<long> CountGrosBras(QueryString queryString)
        {
            List<GrosBrasPopulated> grosBrasFinal = new List<GrosBrasPopulated>();
            var dictionary = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString.Value);
            if (dictionary.TryGetValue("cityLabel", out StringValues values))
            {
                var department = values.FirstOrDefault().Split('-').LastOrDefault();
                // Temporary system to find by department
                if (Int32.TryParse(department, out _)) {
                    List<City> cities = _dpCity.GetCollection().FindAsync(c => c.Departement == department).Result.ToList();
                    long count = 0;
                    
                    foreach (var city in cities)
                    {
                        dictionary["CityId"] = city.Id;
    
                        count += await _dpGrosBras.GetCollection().CountByQueryParamsMongo(new QueryCollection(dictionary));
                    }

                    return count;
                }
                else
                {
                    string cityString = dictionary["cityLabel"];
                    var city = (await _dpCity.GetCollection().FindAsync(c => c.Label.ToLower() == cityString.ToLower())).FirstOrDefault();
                    dictionary.Add("CityId", city.Id);
                }
            }
        
            return await _dpGrosBras.GetCollection().CountByQueryParamsMongo(new QueryCollection(dictionary));
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
                Description = Censure.All(grosBrasProfil.Description),
                Commitment = grosBrasProfil.Commitment,
                ProStatus = grosBrasProfil.ProStatus,
                City = city,
                Departement = grosBrasProfil.Departement,
                CreatedAt = grosBrasProfil.CreatedAt,
                UpdatedAt = grosBrasProfil.UpdatedAt,
                VeryGoodGrade = grosBrasProfil.VeryGoodGrade.ToString(),
                GoodGrade = grosBrasProfil.GoodGrade.ToString(),
                MediumGrade = grosBrasProfil.MediumGrade.ToString(),
                BadGrade = grosBrasProfil.BadGrade.ToString(),
                Rayon = grosBrasProfil.Rayon,
                Title = grosBrasProfil.Title
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
                    Description = Censure.All(profil.Description),
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
                    Rayon = profil.Rayon,
                    Title = profil.Title
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
