using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.DataProvider
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
