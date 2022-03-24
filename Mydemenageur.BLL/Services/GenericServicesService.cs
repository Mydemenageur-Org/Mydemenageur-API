using Microsoft.AspNetCore.Http;
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
using System;
using Microsoft.Extensions.Primitives;
using Mydemenageur.DAL.Models;

namespace Mydemenageur.BLL.Services
{
    public class GenericServicesService : IGenericServicesService
    {
        private readonly IDPCity _dpCity;
        private readonly IDPGenericService _dpGenericService;
        private readonly IDPMyDemenageurUser _dpUser;

        public GenericServicesService(IDPCity dpCity, IDPGenericService dPGenericService, IDPMyDemenageurUser dpUser)
        {
            _dpCity = dpCity;
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
                    if (serviceField.Name == "needPrecisions" || serviceField.Name == "need")
                    {
                        serviceField.Value = Censure.All(serviceField.Value);
                    }
                    
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
                Fields = service.Fields,
                IsDone = service.IsDone
            };
 
            return finalService;
        }

        public async Task<GenericService> GetBaseGenericService(string name)
        {
            GenericService service = await _dpGenericService.GetGenericServicesByName(name).Where(service => service.IsGenericForm == true).FirstOrDefaultAsync();
            return service;
        }

        public async Task<List<GenericService>> GetGenericServices(QueryString queryString, int pageNumber = -1, int numberOfElementsPerPage = -1)
        {
            var sortDefinition = new SortDefinitionBuilder<GenericService>().Descending("Date");
            var dictionary = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString.Value);
            dictionary.TryGetValue("Fields.metadata1.startCity", out StringValues values);
            var department = values.FirstOrDefault().Split('-').LastOrDefault();
            List<GenericService> services = new List<GenericService>();
            // Temporary system to find by department
            if (Int32.TryParse(department, out _)) {
                List<City> cities = _dpCity.GetCollection().FindAsync(c => c.Departement == department).Result.ToList();

                foreach (var city in cities)
                {
                    if (numberOfElementsPerPage > 0 && services.Count >= numberOfElementsPerPage) break;
                    dictionary["Fields.metadata1.startCity"] = city.Label;

                    IQueryCollection queryParams = new QueryCollection(dictionary);
                    services.AddRange(await _dpGenericService.GetCollection().FilterByQueryParamsMongo(queryParams, pageNumber, numberOfElementsPerPage-services.Count, sortDefinition));
                }
            }
            else
            {
                IQueryCollection queryParams = new QueryCollection(dictionary);
                services = await _dpGenericService.GetCollection().FilterByQueryParamsMongo(queryParams, pageNumber, numberOfElementsPerPage, sortDefinition);
            }
            
            return services;
        }
        public async Task<long> GetGenericServicesCount(QueryString queryString, int pageNumber = -1, int numberOfElementsPerPage = -1)
        {
            var dictionary = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString.Value);
            if (dictionary.ContainsKey("cityLabel"))
            {
                string stringCity = dictionary["cityLabel"];
            var city = (await _dpCity.GetCollection().FindAsync(c => c.Label.ToLower() == stringCity.ToLower())).FirstOrDefault();
                dictionary.Add("CityId", city.Id);
            }
            dictionary.TryGetValue("Fields.metadata1.startCity", out StringValues values);
            var department = values.FirstOrDefault().Split('-').LastOrDefault();
            // Temporary system to find by department
            if (Int32.TryParse(department, out _)) {
                List<City> cities = _dpCity.GetCollection().FindAsync(c => c.Departement == department).Result.ToList();
                long count = 0;
                
                foreach (var city in cities)
                {
                    if (numberOfElementsPerPage > 0 && count >= numberOfElementsPerPage) break;
                    dictionary["Fields.metadata1.startCity"] = city.Label;

                    IQueryCollection queryParams = new QueryCollection(dictionary);
                    count += await _dpGenericService.GetCollection()
                        .CountByQueryParamsMongo(queryParams, pageNumber, numberOfElementsPerPage);
                }

                return count;
            }
            else
            {
                IQueryCollection queryParams = new QueryCollection(dictionary);
                return await _dpGenericService.GetCollection().CountByQueryParamsMongo(queryParams, pageNumber, numberOfElementsPerPage);
            }
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

        public async Task<bool> SetGenericServiceDone(string id)
        {
           GenericService genericServiceToBeUpdated = await _dpGenericService.GetGenericServiceById(id).FirstOrDefaultAsync();
           if(genericServiceToBeUpdated == null)
            {
                throw new Exception("Generic service not found");
            }

            var update = Builders<GenericService>.Update.Set(service => service.IsDone, true);

            await _dpGenericService.GetCollection().UpdateOneAsync(service => service.Id == id, update);

            return true;

        }
    }
}
