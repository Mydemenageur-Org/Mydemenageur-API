using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPMenageRepassage
    {
        public IMongoQueryable<MenageRepassage> Obtain();
        public IMongoCollection<MenageRepassage> GetCollection();
        public IMongoQueryable<MenageRepassage> GetCartonById(string id);
    }
}
