using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.DAL.Settings;

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
        public IMongoCollection<MyDemenageurUser> MyDemenageurUser { get { return _database.GetCollection<MyDemenageurUser>("myDemenageurUser");  } }
    }
}
