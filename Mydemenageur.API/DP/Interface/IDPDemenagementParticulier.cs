﻿using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPDemenagementParticulier
    {
        public IMongoQueryable<DemenagementIndividuel> Obtain();
        public IMongoCollection<DemenagementIndividuel> GetCollection();
        public IMongoQueryable<DemenagementIndividuel> GetDemenagementIndivById(string id);

    }
}
