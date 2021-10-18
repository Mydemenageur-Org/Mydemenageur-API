using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPMenageRepassage: IDPMenageRepassage
    {
        public readonly MyDemenageurContext _db;
        public DPMenageRepassage(IOptions<MongoSettings> settings)
        {
            _db = new MyDemenageurContext(settings);
        }
        public IMongoQueryable<MenageRepassage> Obtain()
        {
            return _db.MenageRepassage.AsQueryable();
        }

        public IMongoCollection<MenageRepassage> GetCollection()
        {
            return _db.MenageRepassage;
        }

        public IMongoQueryable<MenageRepassage> GetMenageById(string id)
        {
            return _db.MenageRepassage.AsQueryable().Where(db => db.Id == id);
        }
    }
}
