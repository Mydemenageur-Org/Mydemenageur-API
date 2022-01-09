
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Models;


namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPCity
    {
        public IMongoQueryable<City> Obtain();
        public IMongoCollection<City> GetCollection();
        public IMongoQueryable<City> GetCityById(string id);
    }
}
