using Mydemenageur.API.Entities;
using MongoDB.Driver;

namespace Mydemenageur.API.Contexts
{
    public interface IMyDemenageurContext
    {
        public IMongoCollection<User> User { get; }
        public IMongoCollection<Review> Review { get; }
        public IMongoCollection<Help> Help { get; }
        public IMongoCollection<Service> Service { get; }
    }
}
