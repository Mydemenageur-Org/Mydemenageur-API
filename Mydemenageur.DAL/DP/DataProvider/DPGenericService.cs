using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.GenericService;
using Mydemenageur.DAL.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPGenericService : IDPGenericService
    {
        private readonly MyDemenageurContext _context;

        public DPGenericService(IOptions<MongoSettings> settings)
        {
            _context = new MyDemenageurContext(settings);
        }

        public IMongoQueryable<GenericService> Obtain()
        {
            return _context.GenericService.AsQueryable();
        }

        public IMongoCollection<GenericService> GetCollection()
        {
            return _context.GenericService;
        }

        public IMongoQueryable<GenericService> GetGenericServiceById(string id)
        {
            return _context.GenericService.AsQueryable().Where(dbGenericService => dbGenericService.Id == id);
        }

        public IMongoQueryable<GenericService> GetGenericServicesByName(string name)
        {
            return _context.GenericService.AsQueryable().Where(dbGenericService => dbGenericService.Name == name);
        }

        public IMongoQueryable<GenericService> GetFiltered(Expression<Func<GenericService, bool>> predicate)
        {
            return _context.GenericService.AsQueryable().Where(predicate);
        }
    }
}
