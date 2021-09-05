using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPUser
    {
        public IMongoCollection<User> Obtain();
    }
}
