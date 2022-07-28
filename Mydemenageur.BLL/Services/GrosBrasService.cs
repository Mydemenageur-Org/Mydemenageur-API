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
using Mydemenageur.DAL.Models.Reviews;

namespace Mydemenageur.BLL.Services
{
    public class GrosBrasService: IGrosBrasService
    {
        private readonly IDPGrosBras _dpGrosBras;
        private readonly IDPMyDemenageurUser _dpMDUser;
        private readonly IDPCity _dpCity;
        private readonly ICitiesService _citiesService;
        private readonly IDPDemand _dpDemand;
        private readonly IReviewsService _reviewsService;

        private readonly IFilesService _filesService;

        public GrosBrasService(IDPGrosBras dPGrosBras, IDPMyDemenageurUser dpMDUser, IDPCity dpCity, ICitiesService citiesService, IDPDemand dpDemand,IReviewsService reviewsService, IFilesService filesService)
        {
            _dpGrosBras = dPGrosBras;
            _dpMDUser = dpMDUser;
            _dpCity = dpCity;
            _citiesService = citiesService;
            _dpDemand = dpDemand;
            _reviewsService = reviewsService;

            _filesService = filesService;
        }
        
        private Dictionary<string, StringValues> parseCity(Dictionary<string, StringValues> dictionary)
        {
            if (dictionary.TryGetValue("cityLabel", out StringValues values))
            {
                var department = values.FirstOrDefault().Split('-').LastOrDefault();
                if (Int32.TryParse(department, out _)) {
                    List<City> cities = _dpCity.GetCollection().FindAsync(c => c.Departement == department).Result.ToList();
                    dictionary["CityId"] = new StringValues();
                    foreach (var city in cities)
                        dictionary["CityId"] = StringValues.Concat(city.Id, dictionary["CityId"]);
                }
                else
                {
                    try
                    {
                        var city = _dpCity.GetCollection().FindAsync(c => c.Label.ToLower() == values[0].ToLower()).Result.FirstOrDefault();
                        dictionary.Add("CityId", city.Id);
                    }catch(Exception e)
                    {
                        Console.Write(e.Message);
                    }
                }
            }

            return dictionary;
        }

        public async Task<IList<GrosBrasPopulated>> GetGrosBras(QueryString queryString, int pageNumber = -1, int numberOfElementsPerPage = -1, string cityLabel = "")
        {
            List<GrosBrasPopulated> grosBrasFinal = new List<GrosBrasPopulated>();
            // Sort by reviews count
            var sortDefinition = new SortDefinitionBuilder<GrosBras>().Descending("Note");
            var dictionary = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString.Value);
            dictionary = parseCity(dictionary);
            List<GrosBras> grosBras = await _dpGrosBras.GetCollection().FilterByQueryParamsMongo(new QueryCollection(dictionary), pageNumber, numberOfElementsPerPage, sortDefinition);
            if (grosBras.Count > 0)
            {
                grosBras.ForEach((profil) =>
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
                        Siren = profil.Siren,
                        LicenceTransport = profil.LicenceTransport,
                        City = city,
                        Departement = profil.Departement,
                        CreatedAt = profil.CreatedAt,
                        UpdatedAt = profil.UpdatedAt,
                        VeryGoodGrade = profil.VeryGoodGrade.ToString(),
                        GoodGrade = profil.GoodGrade.ToString(),
                        MediumGrade = profil.MediumGrade.ToString(),
                        BadGrade = profil.BadGrade.ToString(),
                        Rayon = profil.Rayon,
                        Title = profil.Title,
                        Realisations = profil.Realisations,
                        IsPro = profil.IsPro,
                        IsVerified = profil.IsVerified,
                        DepartmentNotifications = profil.DepartmentNotifications,
                        MyDemCert = profil.MyDemCert,
                        MyJugCert = profil.MyJugCert,
                        Cesu = profil.Cesu,
                    };
                    grosBrasFinal.Add(grosBras);
                });
            }

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
                Siren = grosBrasProfil.Siren,
                LicenceTransport = grosBrasProfil.LicenceTransport,
                City = city,
                Departement = grosBrasProfil.Departement,
                CreatedAt = grosBrasProfil.CreatedAt,
                UpdatedAt = grosBrasProfil.UpdatedAt,
                VeryGoodGrade = grosBrasProfil.VeryGoodGrade.ToString(),
                GoodGrade = grosBrasProfil.GoodGrade.ToString(),
                MediumGrade = grosBrasProfil.MediumGrade.ToString(),
                BadGrade = grosBrasProfil.BadGrade.ToString(),
                Rayon = grosBrasProfil.Rayon,
                Title = grosBrasProfil.Title,
                Realisations = grosBrasProfil.Realisations,
                IsVerified = grosBrasProfil.IsVerified,
                DepartmentNotifications = grosBrasProfil.DepartmentNotifications,
                //Fix/Certifications-my-demenageur -- Maxime.M 28-04-2022
                MyDemCert = grosBrasProfil.MyDemCert,
                MyJugCert = grosBrasProfil.MyJugCert,
                Cesu = grosBrasProfil.Cesu,
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
                    Siren = profil.Siren,
                    LicenceTransport = profil.LicenceTransport,
                    City = city,
                    Departement = profil.Departement,
                    CreatedAt = profil.CreatedAt,
                    UpdatedAt = profil.UpdatedAt,
                    VeryGoodGrade = profil.VeryGoodGrade.ToString(),
                    GoodGrade = profil.GoodGrade.ToString(),
                    MediumGrade = profil.MediumGrade.ToString(),
                    BadGrade = profil.BadGrade.ToString(),
                    Rayon = profil.Rayon,
                    Title = profil.Title,
                    Realisations = profil.Realisations,
                    IsVerified = profil.IsVerified,
                    DepartmentNotifications = profil.DepartmentNotifications,
                    //Fix/Certifications-my-demenageur -- Maxime.M 28-04-2022
                    MyDemCert = profil.MyDemCert,
                    MyJugCert = profil.MyJugCert,
                    Cesu = profil.Cesu,
                    
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
        public async Task UploadRealisation(string id, FileModel file)
        {
            GrosBras grosBras = await (_dpGrosBras.GetGrosBrasById(id)).FirstOrDefaultAsync();

            if (grosBras == null)
            {
                throw new ArgumentNullException("Le gros bras n'existe pas");
            }

            MyDemenageurUser mdUser = await _dpMDUser.GetUserById(grosBras.MyDemenageurUserId).FirstOrDefaultAsync();

            if (mdUser == null)
            {
                throw new ArgumentNullException("Le gros bras n'est pas associé à un utilisateur");
            }

            if (mdUser.RoleType == "Basique")
            {
                throw new UnauthorizedAccessException("Vous n'avez pas le droit");
            }

            if (mdUser.RoleType == "Intermédiaire")
            {
                if (grosBras.Realisations.Count >= 3)
                {
                    throw new UnauthorizedAccessException("Vous ne pouvez pas rajouter de nouvelles réalisation");
                }
            }

            string fileId = await _filesService.UploadFile(file.Filename, file.Data);

            grosBras.Realisations.Add(fileId);

            _dpGrosBras.GetCollection().ReplaceOne(dbGrosBras => dbGrosBras.Id == grosBras.Id, grosBras);
        }
        
        public Task<FileModel> GetRealisation(string realisationId)
        {
            return _filesService.GetFile(realisationId);
        }
        
        public async Task CalculatRanking()
        { 
            Console.WriteLine("ENTER SERVICE RANKING GROS BRAS");
            
            //GET ALL GROSBRAS
            var grosBrasList = _dpGrosBras.GetCollection().Find(new BsonDocument()).ToList();
            
            if (grosBrasList.Count == 0)
            {
                throw new Exception("No GrosBras found");
            }
            foreach (var grosBras in grosBrasList)
            {
                var note = 0;
                
                //1-REVIEWS
                IList<ReviewPopulated> reviewsList = await _reviewsService.GetReviews(grosBras.MyDemenageurUserId);
                if (reviewsList.Count < 10)
                {
                    note += 5;
                }else if (reviewsList.Count > 10 && reviewsList.Count < 20)
                {
                    note += 10;
                }else if (reviewsList.Count > 20 && reviewsList.Count < 60)
                {
                    note += 20;
                }
                else
                {
                    note += 30;
                }
                
                //2-ROLETYPE
                try
                {
                    MyDemenageurUser myDemenageurUser = _dpMDUser.GetUserById(grosBras.MyDemenageurUserId).FirstOrDefault();
                    if (myDemenageurUser.RoleType == "Intermédiaire")
                    {
                        note += 10;
                    }else if (myDemenageurUser.RoleType == "Premium" || myDemenageurUser.RoleType == "Professionnel")
                    {
                        note += 20;
                    }
                
                    //3-CONNECTION TIMING
                    var between = Math.Floor((DateTime.Now - myDemenageurUser.LastConnection).TotalDays);
                    if (between < 11)
                    {
                        note += 10;
                    }

                    //4-PROFIL PICTURE
                    if (!(string.IsNullOrWhiteSpace(myDemenageurUser.ProfilePictureId)))
                    {
                        note += 5;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"No MyDemenageurUser found for this grosBras {grosBras.MyDemenageurUserId}");
                }

                //5-DESCRIPTION
                var numberOfChar = grosBras.Description.ToCharArray().Length;
                if (numberOfChar > 140)
                {
                    note += 10;
                }
                
                //6-SENIORITY
                var seniority  = Math.Floor((DateTime.Now - grosBras.CreatedAt).TotalDays);
                if (seniority >= 1095)
                {
                    note += 15;
                }else if (seniority >= 365)
                {
                    note += 10;
                }
                
                //7-RANDOM POINTS
                var random = new Random().Next(0, 11);
                note += random;
                
                //8-NEW ACCOUNT
                if (seniority < 7)
                {
                    note = new Random().Next(70, 86);
                }
                
                //9-INSERT NOTE TO DB
                grosBras.Note = note;
                try
                {
                    await _dpGrosBras.GetCollection().ReplaceOneAsync(g => g.Id == grosBras.Id, grosBras);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            Console.WriteLine("OUT SERVICE RANKING GROSBRAS");
        }
    }
}
