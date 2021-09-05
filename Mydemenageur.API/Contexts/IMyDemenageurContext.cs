using Mydemenageur.API.Entities;
using MongoDB.Driver;

namespace Mydemenageur.API.Contexts
{
    public interface IMyDemenageurContext
    {
        public IMongoCollection<User> User { get; }
    }
}
