using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPDemarche
    {
        public IMongoQueryable<Demarche> Obtain();
        public IMongoCollection<Demarche> GetCollection();
        public IMongoQueryable<Demarche> GetDemarcheById(string id);
    }
}
