using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPMenageDomicile
    {
        public IMongoQueryable<MenageDomicile> Obtain();
        public IMongoCollection<MenageDomicile> GetCollection();
        public IMongoQueryable<MenageDomicile> GetMenageById(string id);
    }
}
