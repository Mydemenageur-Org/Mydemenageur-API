using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPMenageLavageAuto
    {
        public IMongoQueryable<MenageLavageAuto> Obtain();
        public IMongoCollection<MenageLavageAuto> GetCollection();
        public IMongoQueryable<MenageLavageAuto> GetMenageById(string id);
    }
}
