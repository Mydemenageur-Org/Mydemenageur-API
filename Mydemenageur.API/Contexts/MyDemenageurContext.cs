using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Mydemenageur.API.Settings;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.Contexts
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
        public IMongoCollection<Review> Review { get { return _database.GetCollection<Review>("reviews"); } }
        public IMongoCollection<Help> Help { get { return _database.GetCollection<Help>("helps"); } }
        public IMongoCollection<Carton> Carton { get { return _database.GetCollection<Carton>("cartons"); } }
        public IMongoCollection<Demenagement> Demenagement { get { return _database.GetCollection<Demenagement>("demenagement"); } }
        public IMongoCollection<DemenagementIndividuel> DemenagementIndiv { get { return _database.GetCollection<DemenagementIndividuel>("demenagementIndiv"); } }
        public IMongoCollection<DemenagementPro> DemenagementPro { get { return _database.GetCollection<DemenagementPro>("demenagementPro"); } }
        public IMongoCollection<Menage> Menage { get { return _database.GetCollection<Menage>("Menage"); } }
        public IMongoCollection<MenageLavageAuto> MenageLavageAuto { get { return _database.GetCollection<MenageLavageAuto>("MenageLavageAuto"); } }
        public IMongoCollection<MenageDomicile> MenageDomicile { get { return _database.GetCollection<MenageDomicile>("MenageDomicile"); } }
        public IMongoCollection<MenageNettoyageVitre> MenageNettoyageVitre { get { return _database.GetCollection<MenageNettoyageVitre>("MenageNettoyageVitre"); } }
        public IMongoCollection<MenageRepassage> MenageRepassage { get { return _database.GetCollection<MenageRepassage>("MenageRepassage"); } }
        public IMongoCollection<Demarche> Demarche { get { return _database.GetCollection<Demarche>("Demarche"); } }
    }
}
