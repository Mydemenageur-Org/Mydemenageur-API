using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPCartons
    {
        public IMongoQueryable<Carton> Obtain();
        public IMongoCollection<Carton> GetCollection();
        public IMongoQueryable<Carton> GetCartonById(string id);
    }
}
