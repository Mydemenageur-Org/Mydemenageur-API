using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPDemenagementPro
    {
        public IMongoQueryable<DemenagementPro> Obtain();
        public IMongoCollection<DemenagementPro> GetCollection();
        public IMongoQueryable<DemenagementPro> GetDemenagementById(string id);
    }
}
