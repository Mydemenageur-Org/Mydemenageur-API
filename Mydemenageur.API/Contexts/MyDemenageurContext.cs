using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Mydemenageur.API.Settings;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.Contexts
{
    public class MyDemenageurContext
    {
        private readonly IMongoDatabase _database = null;
        public MyDemenageurContext(IOptions<MongoSettings> setting)
        {
            var client = new MongoClient(setting.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(setting.Value.DatabaseName);
        }

        public IMongoCollection<User> User { get { return _database.GetCollection<User>("users"); } }
    }
}
