using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPMenageLavageAuto
    {
        public IMongoQueryable<MenageLavageAuto> Obtain();
        public IMongoCollection<MenageLavageAuto> GetCollection();
        public IMongoQueryable<MenageLavageAuto> GetMenageById(string id);
    }
}
