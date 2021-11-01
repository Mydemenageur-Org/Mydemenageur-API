using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Models.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPGenericService
    {
        public IMongoQueryable<GenericService> Obtain();

        public IMongoCollection<GenericService> GetCollection();

        public IMongoQueryable<GenericService> GetGenericServiceById(string id);
        public IMongoQueryable<GenericService> GetGenericServicesByName(string name);
        public IMongoQueryable<GenericService> GetFiltered(Expression<Func<GenericService, bool>> predicate);
    }
}
