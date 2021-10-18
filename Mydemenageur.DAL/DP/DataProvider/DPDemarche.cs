using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;
namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPDemarche: IDPDemarche
    {
        public readonly MyDemenageurContext _db;
        public DPDemarche(IOptions<MongoSettings> settings)
        {
            _db = new MyDemenageurContext(settings);
        }

        public IMongoQueryable<Demarche> Obtain()
        {
            return _db.Demarche.AsQueryable();
        }

        public IMongoCollection<Demarche> GetCollection()
        {
            return _db.Demarche;
        }

        public IMongoQueryable<Demarche> GetDemarcheById(string id)
        {
            return _db.Demarche.AsQueryable().Where(db => db.Id == id);
        }
    }
}
