using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Models.Demands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPDemand
    {
        public IMongoQueryable<Demand> Obtain();
        public IMongoCollection<Demand> GetCollection();
        public IMongoQueryable<Demand> GetDemandById(string id);
    }
}
