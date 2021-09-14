using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPDemenagement
    {
        public IMongoQueryable<Demenagement> Obtain();
        public IMongoCollection<Demenagement> GetCollection();
        public IMongoQueryable<Demenagement> GetDemenagementById(string id);
    }
}
