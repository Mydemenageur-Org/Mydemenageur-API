using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPMenage
    {
        public IMongoQueryable<Menage> Obtain();
        public IMongoCollection<Menage> GetCollection();
        public IMongoQueryable<Menage> GetMenageById(string id);
    }
}
