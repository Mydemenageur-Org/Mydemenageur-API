﻿using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.GenericService;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.BLL.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services
{
    public class GenericServicesService : IGenericServicesService
    {
        private readonly IDPGenericService _dpGenericService;
        private readonly IDPMyDemenageurUser _dpUser;

        public GenericServicesService(IDPGenericService dPGenericService, IDPMyDemenageurUser dpUser)
        {
            _dpGenericService = dPGenericService;
            _dpUser = dpUser;
        }

        public async Task<GenericServicePopulated> GetGenericService(string id, IList<string> fields)
        {
            GenericService service = await _dpGenericService.GetGenericServiceById(id).FirstOrDefaultAsync();
            var user = await _dpUser.GetUserById(service.UserId).FirstOrDefaultAsync();
            if (fields.Count > 0) {
                foreach (var serviceField in service.Fields)
                {
                    if (!fields.Contains(serviceField.Name))
                    {
                        service.Fields.Remove(serviceField);
                    }
                }
            }

            GenericServicePopulated finalService = new GenericServicePopulated() {
                Id = service.Id,
                User = user,
                IsGenericForm = service.IsGenericForm,
                Name = service.Name,
                Date = service.Date,
                Fields = service.Fields
            };
 
            return finalService;
        }

        public async Task<GenericService> GetBaseGenericService(string name)
        {
            GenericService service = await _dpGenericService.GetGenericServicesByName(name).Where(service => service.IsGenericForm == true).FirstOrDefaultAsync();
            return service;
        }

        public async Task<List<GenericService>> GetGenericServices(IQueryCollection queryParams, int pageNumber = -1, int numberOfElementsPerPage = -1)
        {
            var sortDefinition = new SortDefinitionBuilder<GenericService>().Descending("Date");
            List<GenericService> services = await _dpGenericService.GetCollection().FilterByQueryParamsMongo(queryParams, pageNumber, numberOfElementsPerPage, sortDefinition);

            return services;
        }

        public async Task<GenericService> CreateGenericService(GenericService toCreate)
        { 
            await _dpGenericService.GetCollection().InsertOneAsync(toCreate);

            return toCreate;
        }

        public async Task UpdateGenericService(GenericService toUpdate)
        {
            // TODO: sercure modification from non admin users
            await _dpGenericService.GetCollection().ReplaceOneAsync(
                dbGenericService => dbGenericService.Id == toUpdate.Id,
                toUpdate
            );
        }

        public async Task DeleteGenericService(string id)
        {
            await _dpGenericService.GetCollection().DeleteOneAsync(dbGenericService => dbGenericService.Id == id);
        }
    }
}
