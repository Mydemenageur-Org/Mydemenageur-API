using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;
using System;
using System.Linq.Expressions;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPUser
    {
        public IMongoCollection<User> Obtain();
        public IMongoQueryable<User> GetUserById(string idUser);
        public IMongoQueryable<User> GetFiltered(Expression<Func<User, bool>> predicate);
    }
}
