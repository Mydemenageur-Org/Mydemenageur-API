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

        public async Task<GenericService> GetGenericService(string id, IList<string> fields)
        {
            GenericService service = await _dpGenericService.GetGenericServiceById(id).FirstOrDefaultAsync();


            if (fields.Count > 0) {
                foreach (var serviceField in service.Fields)
                {
                    if (!fields.Contains(serviceField.FieldId))
                    {
                        service.Fields.Remove(serviceField);
                    }
                }
            }

            return service;
        }

        public async Task<GenericService> GetBaseGenericService(string name)
        {
            GenericService service = await _dpGenericService.GetGenericServicesByName(name).Where(service => service.IsGenericForm == true).FirstOrDefaultAsync();
            return service;
        }

        public async Task<IList<GenericService>> GetGenericServices(string name, IList<string> fields)
        {

            IMongoQueryable<GenericService> genericServices;

            if (string.IsNullOrWhiteSpace(name)) {
                genericServices = _dpGenericService.Obtain();
            }
            else {
                genericServices = _dpGenericService.GetFiltered(
                    dbGenericService => dbGenericService.Name == name    
                );  
            }

            IList<GenericService> services = await genericServices.ToListAsync();

            if (fields.Count > 0)
            {
                foreach (var service in services)
                {
                    foreach (var serviceField in service.Fields)
                    {
                        if (!fields.Contains(serviceField.FieldId))
                        {
                            service.Fields.Remove(serviceField);
                        }
                    }
                }
            }

            return services;
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
