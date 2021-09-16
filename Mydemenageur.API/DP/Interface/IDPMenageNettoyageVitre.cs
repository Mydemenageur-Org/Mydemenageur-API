using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPMenageNettoyageVitre
    {
        public IMongoQueryable<MenageNettoyageVitre> Obtain();
        public IMongoCollection<MenageNettoyageVitre> GetCollection();
        public IMongoQueryable<MenageNettoyageVitre> GetCartonById(string id);
    }
}
