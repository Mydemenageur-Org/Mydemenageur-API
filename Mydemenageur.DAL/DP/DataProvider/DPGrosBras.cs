using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;
using System;
using Mydemenageur.DAL.Models.Users;


namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPGrosBras: IDPGrosBras
    {
        private readonly MyDemenageurContext _db;

        public DPGrosBras(IOptions<MongoSettings> setting)
        {
            this._db = new MyDemenageurContext(setting);
        }

        public IMongoQueryable<GrosBras> Obtain()
        {
            return this._db.GrosBras.AsQueryable();
        }

        public IMongoCollection<GrosBras> GetCollection()
        {
            return this._db.GrosBras;
        }

        public IMongoQueryable<GrosBras> GetGrosBrasById(string idGrosBras)
        {
            return _db.GrosBras.AsQueryable().Where(w => w.Id == idGrosBras);
        }

        public IMongoQueryable<GrosBras> GetFiltered(Expression<Func<GrosBras, bool>> predicate)
        {
            return _db.GrosBras.AsQueryable().Where(predicate);
        }
    }
}
