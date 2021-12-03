using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Models.Demands;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPDemand
    {
        public IMongoQueryable<Demand> Obtain();
        public IMongoCollection<Demand> GetCollection();
        public IMongoQueryable<Demand> GetDemandById(string id);
    }
}
