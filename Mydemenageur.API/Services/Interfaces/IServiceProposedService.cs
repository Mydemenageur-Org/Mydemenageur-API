using System;
using System.Collections.Generic;
using Mydemenageur.API.Entities;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IServiceProposedService
    {
        public Task<IList<Service>> GetAllServices();
        public Task<Service> GetServiceById(string id);
        public Task UpdateService(string id, string label, byte file);
        public Task DeleteService(string id);
        public Task<string> CreateService(string label, byte file);
    }
}
