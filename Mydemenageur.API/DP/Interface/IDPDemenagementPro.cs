using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPDemenagementPro
    {
        public IMongoQueryable<DemenagementPro> Obtain();
        public IMongoCollection<DemenagementPro> GetCollection();
        public IMongoQueryable<DemenagementPro> GetDemenagementById(string id);
    }
}
