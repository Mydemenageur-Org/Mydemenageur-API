using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;
using MongoDB.Driver;

namespace Mydemenageur.API.DP.DataProvider
{
    public class DPHelp: IDPHelp
    {
        private readonly MyDemenageurContext _db;

        public DPHelp(IOptions<MongoSettings> setting)
        {
            _db = new MyDemenageurContext(setting);
        }

        public IMongoQueryable<Help> Obtain()
        {
            return _db.Help.AsQueryable();
        }

        public IMongoQueryable<Help> GetHelpById(string id)
        {
            return _db.Help.AsQueryable().Where(db => db.Id == id);
        }

        public IMongoCollection<Help> GetCollection()
        {
            return _db.Help;
        }
    }
}
