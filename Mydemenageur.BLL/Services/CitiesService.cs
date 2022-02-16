using MongoDB.Driver;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.DP.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mydemenageur.DAL.Models;
using System;
using System.Linq;

namespace Mydemenageur.BLL.Services
{
    public class CitiesService : ICitiesService
    {
        private readonly IDPCity _dpCity;

        public CitiesService(IDPCity dpCity)
        {
            _dpCity = dpCity;
        }

        public async Task<IList<City>> GetAllCities()
        {
            IList<City> cities = _dpCity.Obtain().OrderBy(f => f.Label).ToList();

            if(cities.Count == 0)
            {
                throw new Exception("No cities found");
            }

            return cities;
        }

        public async Task<City> CreateNewCity(string label)
        {
            City newCity = new City
            {
                Label = label,
                CreatedAt = DateTime.Now,
            };

            await _dpCity.GetCollection().InsertOneAsync(newCity);

            return newCity;
        }

        public async Task<City> SearchCity(string label)
        {
            var update = Builders<City>.Update
                .Inc(city => city.SearchCount, 1);

            await _dpCity.GetCollection().UpdateOneAsync(city => city.Label == label, update);

            return _dpCity.GetCollection().FindAsync(city => city.Label == label).Result.First();
        }
    }
}
