using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;
using System;
using System.Linq.Expressions;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPUser
    {
        public IMongoCollection<User> Obtain();
        public IMongoQueryable<User> GetUserById(string idUser);
        public IMongoQueryable<User> GetFiltered(Expression<Func<User, bool>> predicate);
    }
}
