using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;


namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPDemenagementPro: IDPDemenagementPro
    {
        private readonly MyDemenageurContext _db;

        public DPDemenagementPro(IOptions<MongoSettings> settings)
        {
            _db = new MyDemenageurContext(settings);
        }

        public IMongoQueryable<DemenagementPro> Obtain()
        {
            return _db.DemenagementPro.AsQueryable();
        }

        public IMongoCollection<DemenagementPro> GetCollection()
        {
            return _db.DemenagementPro;
        }

        public IMongoQueryable<DemenagementPro> GetDemenagementById(string id)
        {
            return _db.DemenagementPro.AsQueryable().Where(w => w.Id == id);
        }
    }
}
