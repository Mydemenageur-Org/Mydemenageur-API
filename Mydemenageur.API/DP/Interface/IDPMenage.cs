using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPMenage
    {
        public IMongoQueryable<Menage> Obtain();
        public IMongoCollection<Menage> GetCollection();
        public IMongoQueryable<Menage> GetMenageById(string id);
    }
}
