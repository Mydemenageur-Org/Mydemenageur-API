using MongoDB.Driver;
using Mydemenageur.API.Entities;


namespace Mydemenageur.API.DP.Interface
{
    public interface IDPHelp
    {
        public IMongoCollection<Help> Obtain();
    }
}
