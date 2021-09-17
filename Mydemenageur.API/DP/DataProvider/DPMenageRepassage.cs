using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.DataProvider
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
