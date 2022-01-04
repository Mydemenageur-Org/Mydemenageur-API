using MongoDB.Driver;
using Mydemenageur.DAL.Models.GenericService;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.DAL.Models.Demands;
using Mydemenageur.DAL.Models.Reviews;

namespace Mydemenageur.DAL.Contexts
{
    public interface IMyDemenageurContext
    {
        public IMongoCollection<User> User { get; }
        public IMongoCollection<MyDemenageurUser> MyDemenageurUser { get; }
        public IMongoCollection<GenericService> GenericService { get; }
        public IMongoCollection<Review> Review { get; }
        public IMongoCollection<Demand> Demand { get; }
    }
}
