using MongoDB.Driver;
using Mydemenageur.DAL.Models.Users;

namespace Mydemenageur.DAL.Contexts
{
    public interface IMyDemenageurContext
    {
        public IMongoCollection<User> User { get; }
    }
}
