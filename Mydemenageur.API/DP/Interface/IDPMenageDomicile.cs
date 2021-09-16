using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPMenageDomicile
    {
        public IMongoQueryable<MenageDomicile> Obtain();
        public IMongoCollection<MenageDomicile> GetCollection();
        public IMongoQueryable<MenageDomicile> GetCartonById(string id);
    }
}
