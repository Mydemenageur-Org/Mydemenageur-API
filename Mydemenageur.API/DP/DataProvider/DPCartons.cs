using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.DataProvider
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
