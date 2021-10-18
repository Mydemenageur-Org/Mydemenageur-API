using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPMenageDomicile: IDPMenageDomicile
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
