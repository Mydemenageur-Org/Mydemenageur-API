using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;
using System.Linq.Expressions;
using System;

namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPUser: IDPUser
    {
        private readonly MyDemenageurContext _db;

        public DPUser(IOptions<MongoSettings> setting)
        {
            this._db = new MyDemenageurContext(setting);
        }

        public IMongoCollection<User> Obtain()
        {
            return this._db.User;
        }

        public IMongoQueryable<User> GetUserById(string idUser)
        {
            return _db.User.AsQueryable().Where(w => w.Id == idUser);
        }

        public IMongoQueryable<User> GetFiltered(Expression<Func<User, bool>> predicate)
        {
            return _db.User.AsQueryable()
                .Where(predicate);
        }
    }
}
