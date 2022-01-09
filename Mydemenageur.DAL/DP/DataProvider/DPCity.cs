using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models;
using Mydemenageur.DAL.Settings;


namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPCity: IDPCity
    {
        private readonly MyDemenageurContext _context;

        public DPCity(IOptions<MongoSettings> settings)
        {
            _context = new MyDemenageurContext(settings);
        }

        public IMongoQueryable<City> Obtain()
        {
            return _context.City.AsQueryable();
        }

        public IMongoCollection<City> GetCollection()
        {
            return _context.City;
        }

        public IMongoQueryable<City> GetCityById(string id)
        {
            return _context.City.AsQueryable().Where(city => city.Id == id);
        }
    }
}
