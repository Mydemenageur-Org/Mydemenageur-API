using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;


namespace Mydemenageur.API.DP.Interface
{
    public interface IDPHelp
    {
        public IMongoQueryable<Help> Obtain();
        public IMongoQueryable<Help> GetHelpById(string id);
        public IMongoCollection<Help> GetCollection();
    }
}
