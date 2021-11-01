using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;
using System;
using Mydemenageur.DAL.Models.MyDemenageurUser;

namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPMyDemenageurUser : IDPMyDemenageurUser
    {
        private readonly MyDemenageurContext _db;

        public DPUser(IOptions<MongoSettings> setting)
        {
            this._db = new MyDemenageurContext(setting);
        }

        public IMongoCollection<User> Obtain()
        {
            return this._db.MyDemenageurUser;
        }

        public IMongoQueryable<User> GetUserById(string idUser)
        {
            return _db.MyDemenageurUser.AsQueryable().Where(w => w.Id == idUser);
        }

        public IMongoQueryable<User> GetFiltered(Expression<Func<User, bool>> predicate)
        {
            return _db.MyDemenageurUser.AsQueryable()
                .Where(predicate);
        }
    }
}
