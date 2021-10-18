using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPDemenagementParticulier
    {
        public IMongoQueryable<DemenagementIndividuel> Obtain();
        public IMongoCollection<DemenagementIndividuel> GetCollection();
        public IMongoQueryable<DemenagementIndividuel> GetDemenagementIndivById(string id);

    }
}
