using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPMenageNettoyageVitre: IDPMenageNettoyageVitre
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
