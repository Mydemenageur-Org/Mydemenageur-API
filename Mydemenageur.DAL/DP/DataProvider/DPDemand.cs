using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.Demands;
using Mydemenageur.DAL.Settings;

namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPDemand : IDPDemand
    {
        private readonly MyDemenageurContext _context;

        public DPDemand(IOptions<MongoSettings> settings)
        {
            _context = new MyDemenageurContext(settings);
        }

        public IMongoQueryable<Demand> Obtain()
        {
            return _context.Demand.AsQueryable();
        }

        public IMongoCollection<Demand> GetCollection()
        {
            return _context.Demand;
        }

        public IMongoQueryable<Demand> GetDemandById(string id)
        {
            return _context.Demand.AsQueryable().Where(demand => demand.Id == id);
        }
    }
}
