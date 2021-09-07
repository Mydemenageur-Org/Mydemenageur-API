using System;
using System.Collections.Generic;
using Mydemenageur.API.Models.Demands;
using Mydemenageur.API.Entities;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IHelpService
    {
        public Task<IList<HelpModel>> GetAllHelpAnnounces(string type, string title, DateTime createdAt, string personNumber, DateTime timeNeeded, string start, string destination, string budget, List<Service> services, int size);
        public Task<HelpModel> GetHelpAnnounceById(string id);
        public Task<string> CreateHelpAnnounce(string type, string title, DateTime createdAt, string personNumber, DateTime timeNeeded, string start, string destination, string budget, List<Service> services);
        public Task UpdateHelpAnnounce(string id);
        public Task DeleteHelpAnnounce(string id);
    }
}
