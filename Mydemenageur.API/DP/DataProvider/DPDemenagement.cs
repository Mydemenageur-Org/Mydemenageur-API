using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.DataProvider
{
    public class DPDemenagement: IDPDemenagement
    {
        private readonly MyDemenageurContext _db;
        public DPDemenagement(IOptions<MongoSettings> settings)
        {
            _db = new MyDemenageurContext(settings);
        }

        public IMongoQueryable<Demenagement> Obtain()
        {
            return _db.Demenagement.AsQueryable();
        }

        public IMongoCollection<Demenagement> GetCollection()
        {
            return _db.Demenagement;
        }

        public IMongoQueryable<Demenagement> GetDemenagementById(string id)
        {
            return _db.Demenagement.AsQueryable().Where(w => w.Id == id);
        }
    }
}
