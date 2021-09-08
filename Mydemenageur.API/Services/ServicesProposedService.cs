using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Mydemenageur.API.DP.Interface;
using MongoDB.Driver;
using Mydemenageur.API.Services.Interfaces;

namespace Mydemenageur.API.Services
{
    public class ServicesProposedService: IServiceProposedService
    {

        private readonly IDPService _dpService;

        public ServicesProposedService(IDPService dpService)
        {
            _dpService = dpService;
        }

        public async Task<IList<Service>> GetAllServices()
        {
            List<Service> services = await _dpService.Obtain().ToListAsync();
            return services;
        }

        public async Task<Service> GetServiceById(string id)
        {
            Service service = await _dpService.GetServiceById(id).FirstOrDefaultAsync();
            if (service == null) throw new Exception("The service does not exist");
            return service;
        }

        public async Task UpdateService(string id, string label, byte file)
        {
            Service service = await _dpService.GetServiceById(id).FirstOrDefaultAsync();
            if (service == null) throw new Exception("The service does not exist");

            var update = Builders<Service>.Update
                .Set(db => db.Label, label)
                .Set(db => db.Picture, file);

            await _dpService.GetCollection().UpdateOneAsync(db =>
                db.Id == id,
                update
            );
        }

        public async Task DeleteService(string id)
        {
            if(id != null)
            {
                Service serviceToBeDeleted = await _dpService.GetServiceById(id).FirstOrDefaultAsync();
                if (serviceToBeDeleted == null) throw new Exception("The service does not exist");

                await _dpService.GetCollection().DeleteOneAsync(db => db.Id == id);
            }
        }

        public async Task<string> CreateService(string label, byte file)
        {
            string idService = "";

            return idService;
        }
    }
}
