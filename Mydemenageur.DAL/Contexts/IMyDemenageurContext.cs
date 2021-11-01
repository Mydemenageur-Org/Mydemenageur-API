using MongoDB.Driver;
using Mydemenageur.DAL.Models.GenericService;
using Mydemenageur.DAL.Models.Users;

namespace Mydemenageur.DAL.Contexts
{
    public interface IMyDemenageurContext
    {
        public IMongoCollection<User> User { get; }
        public IMongoCollection<MyDemenageurUser> MyDemenageurUser { get; }
        public IMongoCollection<GenericService> GenericService { get; }
    }
}
