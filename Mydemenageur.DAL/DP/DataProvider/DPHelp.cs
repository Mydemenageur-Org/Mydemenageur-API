using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;
using MongoDB.Driver;

namespace Mydemenageur.DAL.DP.DataProvider
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
