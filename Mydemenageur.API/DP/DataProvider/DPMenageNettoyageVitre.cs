using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.DataProvider
{
    public class DPMenageNettoyageVitre
    {
        public readonly MyDemenageurContext _db;
        public DPMenageNettoyageVitre(IOptions<MongoSettings> settings)
        {
            _db = new MyDemenageurContext(settings);
        }
        public IMongoQueryable<MenageNettoyageVitre> Obtain()
        {
            return _db.MenageNettoyageVitre.AsQueryable();
        }

        public IMongoCollection<MenageNettoyageVitre> GetCollection()
        {
            return _db.MenageNettoyageVitre;
        }

        public IMongoQueryable<MenageNettoyageVitre> GetMenageById(string id)
        {
            return _db.MenageNettoyageVitre.AsQueryable().Where(db => db.Id == id);
        }
    }
}
