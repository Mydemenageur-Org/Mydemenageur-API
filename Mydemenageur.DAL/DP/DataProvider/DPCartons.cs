using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPCartons: IDPCartons
    {
        public readonly MyDemenageurContext _db;
        public DPCartons(IOptions<MongoSettings> settings)
        {
            _db = new MyDemenageurContext(settings);
        }

        public IMongoQueryable<Carton> Obtain()
        {
            return _db.Carton.AsQueryable();
        }

        public IMongoCollection<Carton> GetCollection()
        {
            return _db.Carton;
        }

        public IMongoQueryable<Carton> GetCartonById(string id)
        {
            return _db.Carton.AsQueryable().Where(w => w.Id == id);
        }
    }
}
