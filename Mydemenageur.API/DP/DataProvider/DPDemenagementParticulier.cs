using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;
using Mydemenageur.API.DP.Interface;

namespace Mydemenageur.API.DP.DataProvider
{
    public class DPDemenagementParticulier: IDPDemenagementParticulier
    {
        private readonly MyDemenageurContext _db;

        public DPDemenagementParticulier(IOptions<MongoSettings> settings)
        {
            _db = new MyDemenageurContext(settings);
        }

        public IMongoQueryable<DemenagementIndividuel> Obtain()
        {
            return _db.DemenagementIndiv.AsQueryable();
        }

        public IMongoCollection<DemenagementIndividuel> GetCollection()
        {
            return _db.DemenagementIndiv;
        }

        public IMongoQueryable<DemenagementIndividuel> GetDemenagementIndivById(string id)
        {
            return _db.DemenagementIndiv.AsQueryable().Where(w => w.Id == id);
        }
    }
}
