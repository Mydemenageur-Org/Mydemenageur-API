using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.DataProvider
{
    public class DPMenageDomicile
    {
        public readonly MyDemenageurContext _db;
        public DPMenageDomicile(IOptions<MongoSettings> settings)
        {
            _db = new MyDemenageurContext(settings);
        }


        public IMongoQueryable<MenageDomicile> Obtain()
        {
            return _db.MenageDomicile.AsQueryable();
        }

        public IMongoCollection<MenageDomicile> GetCollection()
        {
            return _db.MenageDomicile;
        }

        public IMongoQueryable<MenageDomicile> GetMenageById(string id)
        {
            return _db.MenageDomicile.AsQueryable().Where(db => db.Id == id);
        }
    }
}
