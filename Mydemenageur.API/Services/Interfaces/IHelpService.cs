using System;
using System.Collections.Generic;
using Mydemenageur.API.Models.Demands;
using Mydemenageur.API.Entities;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IHelpService
    {
        public Task<IList<Help>> GetAllHelpAnnounces(string type, string title, Nullable<DateTime> createdAt, string personNumber, Nullable<DateTime> timeNeeded, string start, string destination, string budget, List<Service> services, int size);
        public Task<Help> GetHelpAnnounceById(string id);
        public Task<string> CreateHelpAnnounce(string type, string title, string personNumber, DateTime timeNeeded, string start, string destination, string budget, List<Service> services);
        public Task UpdateHelpAnnounce(string id, string type, string title, string personNumber, DateTime timeNeeded, string start, string destination, string budget, List<Service> services);
        public Task DeleteHelpAnnounce(string id);
    }
}
