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
                Category = service.Category,
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

        private Dictionary<string, StringValues> parseDepartment(Dictionary<string, StringValues> dictionary)
        {
            var filter = "";
            if (dictionary.ContainsKey("category") && dictionary["category"].Equals("demenagement"))
            {
                filter = "Fields.metadata1.startCity";
            } else
            {
                filter = "Fields.metadata1.city";
            }
            if (dictionary.TryGetValue(filter, out StringValues values))
            {
                var department = values.FirstOrDefault().Split('-').LastOrDefault();
                if (Int32.TryParse(department, out _)) {
                    List<City> cities = _dpCity.GetCollection().FindAsync(c => c.Departement == department).Result.ToList();
                    dictionary[filter] = new StringValues();
                    foreach (var city in cities)
                        dictionary[filter] = StringValues.Concat(city.Label, dictionary[filter]);
                }
            }

            return dictionary;
        }

        public async Task<List<GenericService>> GetGenericServices(QueryString queryString, int pageNumber = -1, int numberOfElementsPerPage = -1)
        {
            var sortDefinition = new SortDefinitionBuilder<GenericService>().Descending("Date");
            var dictionary = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString.Value);
            dictionary = parseDepartment(dictionary);
            
            return await _dpGenericService.GetCollection().FilterByQueryParamsMongo(new QueryCollection(dictionary), pageNumber, numberOfElementsPerPage, sortDefinition);
        }
        //Fix/annonce-get-service-category-500 -- Maxime.M 01/05/22
        public async Task<List<GenericService>> GetUserGenericServices(QueryString queryString, int pageNumber = -1, int numberOfElementsPerPage = -1)
        {
            var sortDefinition = new SortDefinitionBuilder<GenericService>().Descending("Date");
            var dictionary = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString.Value);
            
            return await _dpGenericService.GetCollection().FilterByQueryParamsMongo(new QueryCollection(dictionary), pageNumber, numberOfElementsPerPage, sortDefinition);
        }
        public async Task<long> GetGenericServicesCount(QueryString queryString, int pageNumber = -1, int numberOfElementsPerPage = -1)
        {
            var dictionary = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString.Value);
            dictionary = parseDepartment(dictionary);

            return await _dpGenericService.GetCollection().CountByQueryParamsMongo(new QueryCollection(dictionary), pageNumber, numberOfElementsPerPage);
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
