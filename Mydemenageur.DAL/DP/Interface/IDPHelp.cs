using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;


namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPHelp
    {
        public IMongoQueryable<Help> Obtain();
        public IMongoQueryable<Help> GetHelpById(string id);
        public IMongoCollection<Help> GetCollection();
    }
}
