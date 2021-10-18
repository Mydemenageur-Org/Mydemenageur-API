using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPMenageRepassage
    {
        public IMongoQueryable<MenageRepassage> Obtain();
        public IMongoCollection<MenageRepassage> GetCollection();
        public IMongoQueryable<MenageRepassage> GetMenageById(string id);

    }
}
