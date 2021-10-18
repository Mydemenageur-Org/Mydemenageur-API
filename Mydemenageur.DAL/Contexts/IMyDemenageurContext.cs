using Mydemenageur.DAL.Entities;
using MongoDB.Driver;

namespace Mydemenageur.DAL.Contexts
{
    public interface IMyDemenageurContext
    {
        public IMongoCollection<User> User { get; }
        public IMongoCollection<Review> Review { get; }
        public IMongoCollection<Help> Help { get; }
        public IMongoCollection<Carton> Carton { get; }
        public IMongoCollection<DemenagementIndividuel> DemenagementIndiv { get; }
        public IMongoCollection<DemenagementPro> DemenagementPro { get; }
        public IMongoCollection<Demarche> Demarche { get; }
    }
}
