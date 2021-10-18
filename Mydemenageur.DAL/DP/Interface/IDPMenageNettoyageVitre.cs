using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPMenageNettoyageVitre
    {
        public IMongoQueryable<MenageNettoyageVitre> Obtain();
        public IMongoCollection<MenageNettoyageVitre> GetCollection();
        public IMongoQueryable<MenageNettoyageVitre> GetMenageById(string id);
    }
}
