using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPDemenagement
    {
        public IMongoQueryable<Demenagement> Obtain();
        public IMongoCollection<Demenagement> GetCollection();
        public IMongoQueryable<Demenagement> GetDemenagementById(string id);
    }
}
