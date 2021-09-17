using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPDemarche
    {
        public IMongoQueryable<Demarche> Obtain();
        public IMongoCollection<Demarche> GetCollection();
        public IMongoQueryable<Demarche> GetDemarcheById(string id);
    }
}
