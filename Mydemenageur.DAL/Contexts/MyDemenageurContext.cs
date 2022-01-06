using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Mydemenageur.DAL.Models.GenericService;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.DAL.Models.Demands;
using Mydemenageur.DAL.Settings;
using Mydemenageur.DAL.Models.Reviews;

namespace Mydemenageur.DAL.Contexts
{
    public class MyDemenageurContext: IMyDemenageurContext
    {
        private readonly IMongoDatabase _database = null;
        public MyDemenageurContext(IOptions<MongoSettings> setting)
        {
            var client = new MongoClient(setting.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(setting.Value.DatabaseName);
        }

        public IMongoCollection<User> User { get { return _database.GetCollection<User>("users"); } }
        public IMongoCollection<MyDemenageurUser> MyDemenageurUser { get { return _database.GetCollection<MyDemenageurUser>("myDemenageurUser"); } }
        public IMongoCollection<GenericService> GenericService { get { return _database.GetCollection<GenericService>("genericServices"); } }
        public IMongoCollection<Review> Review { get { return _database.GetCollection<Review>("reviews"); } }
        public IMongoCollection<Demand> Demand { get { return _database.GetCollection<Demand>("demand"); } }
        public IMongoCollection<GrosBras> GrosBras { get { return _database.GetCollection<GrosBras>("grosBras"); } }
    }
}
