using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;
namespace Mydemenageur.API.DP.DataProvider
{
    public class DPMenage
    {
        public readonly MyDemenageurContext _db;
        public DPMenage(IOptions<MongoSettings> settings)
        {
            _db = new MyDemenageurContext(settings);
        }

        public IMongoQueryable<Menage> Obtain()
        {
            return _db.Menage.AsQueryable();
        }

        public IMongoCollection<Menage> GetCollection()
        {
            return _db.Menage;
        }

        public IMongoQueryable<Menage> GetMenageById(string id)
        {
            return _db.Menage.AsQueryable().Where(db => db.Id == id);
        }
    }
}
