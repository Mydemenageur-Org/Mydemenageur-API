using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services
{
    public class GenericServicesService : IGenericServicesService
    {
        private readonly IDPGenericService _dpGenericService;

        public GenericServicesService(IDPGenericService dPGenericService)
        {
            _dpGenericService = dPGenericService;
        }

        public async Task<GenericService> GetGenericService(string id)
        {
            return await _dpGenericService.GetGenericServiceById(id).FirstOrDefaultAsync();
        }

        public async Task<IList<GenericService>> GetGenericServices(string name)
        {
            IMongoQueryable<GenericService> genericServices = _dpGenericService.GetFiltered(
                dbGenericService => string.IsNullOrWhiteSpace(name) ? true : dbGenericService.Name == name
            );

            return await genericServices.ToListAsync();
        }

        public async Task<string> CreateGenericService(GenericService toCreate)
        {
            await _dpGenericService.GetCollection().InsertOneAsync(toCreate);

            return toCreate.Id;
        }

        public async Task UpdateGenericService(GenericService toUpdate)
        {
            // TODO: sercure modification from non admin users
            await _dpGenericService.GetCollection().ReplaceOneAsync(
                dbGenericService => dbGenericService.Id == toUpdate.Id,
                toUpdate
            );
        }
    }
}
