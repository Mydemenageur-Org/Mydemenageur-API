using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPService
    {
        public IMongoQueryable<Service> Obtain();
        public IMongoCollection<Service> GetCollection();
        public IMongoQueryable<Service> GetServiceById(string id);
    }
}
