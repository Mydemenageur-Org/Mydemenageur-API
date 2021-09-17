using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.DataProvider
{
    public class DPMenageLavageAuto: IDPMenageLavageAuto
    {
        public readonly MyDemenageurContext _db;
        public DPMenageLavageAuto(IOptions<MongoSettings> settings)
        {
            _db = new MyDemenageurContext(settings);
        }

        public IMongoQueryable<MenageLavageAuto> Obtain()
        {
            return _db.MenageLavageAuto.AsQueryable();
        }

        public IMongoCollection<MenageLavageAuto> GetCollection()
        {
            return _db.MenageLavageAuto;
        }

        public IMongoQueryable<MenageLavageAuto> GetMenageById(string id)
        {
            return _db.MenageLavageAuto.AsQueryable().Where(db => db.Id == id);
        }
    }
}
