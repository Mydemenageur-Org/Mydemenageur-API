using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.DP.Interface;

namespace Mydemenageur.DAL.DP.DataProvider
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
