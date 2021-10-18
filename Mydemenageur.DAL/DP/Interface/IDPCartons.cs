using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPCartons
    {
        public IMongoQueryable<Carton> Obtain();
        public IMongoCollection<Carton> GetCollection();
        public IMongoQueryable<Carton> GetCartonById(string id);
    }
}
