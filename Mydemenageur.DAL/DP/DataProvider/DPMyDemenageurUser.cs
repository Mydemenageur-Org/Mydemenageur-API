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
    public class DPMyDemenageurUser : IDPMyDemenageurUser
    {
        private readonly MyDemenageurContext _db;

        public DPMyDemenageurUser(IOptions<MongoSettings> setting)
        {
            this._db = new MyDemenageurContext(setting);
        }

        public IMongoQueryable<MyDemenageurUser> Obtain()
        {
            return this._db.MyDemenageurUser.AsQueryable();
        }

        public IMongoCollection<MyDemenageurUser> GetCollection()
        {
            return this._db.MyDemenageurUser;
        }

        public IMongoQueryable<MyDemenageurUser> GetUserById(string idUser)
        {
            return _db.MyDemenageurUser.AsQueryable().Where(w => w.Id == idUser);
        }

        public IMongoQueryable<MyDemenageurUser> GetFiltered(Expression<Func<MyDemenageurUser, bool>> predicate)
        {
            return _db.MyDemenageurUser.AsQueryable()
                .Where(predicate);
        }
    }
}
