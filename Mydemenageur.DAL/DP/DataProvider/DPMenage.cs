using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPMenage: IDPMenage
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
