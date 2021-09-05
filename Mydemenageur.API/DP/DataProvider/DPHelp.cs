using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.DataProvider
{
    public class DPHelp: IDPHelp
    {
        private readonly MyDemenageurContext _db;

        public DPHelp(IOptions<MongoSettings> setting)
        {
            _db = new MyDemenageurContext(setting);
        }

        public IMongoCollection<Help> Obtain()
        {
            return _db.Help;
        }
    }
}
