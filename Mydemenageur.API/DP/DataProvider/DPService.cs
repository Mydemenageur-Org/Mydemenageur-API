using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.DataProvider
{
    public class DPService: IDPService
    {
        private readonly MyDemenageurContext _db;
        public DPService(IOptions<MongoSettings> settings)
        {
            _db = new MyDemenageurContext(settings);
        }

        public IMongoQueryable<Service> Obtain()
        {
            return _db.Service.AsQueryable();
        }

        public IMongoCollection<Service> GetCollection()
        {
            return _db.Service;
        }

        public IMongoQueryable<Service> GetServiceById(string id)
        {
            return _db.Service.AsQueryable().Where(w => w.Id == id);
        }
    }
}
