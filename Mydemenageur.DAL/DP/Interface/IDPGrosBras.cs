using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPGrosBras
    {
        public IMongoCollection<GrosBras> GetCollection();
        public IMongoQueryable<GrosBras> Obtain();
        public IMongoQueryable<GrosBras> GetGrosBrasById(string idGrosBras);
        public IMongoQueryable<GrosBras> GetFiltered(Expression<Func<GrosBras, bool>> predicate);
    }
}
