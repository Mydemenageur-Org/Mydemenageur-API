using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;
namespace Mydemenageur.API.DP.DataProvider
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
