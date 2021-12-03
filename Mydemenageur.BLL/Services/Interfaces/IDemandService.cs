using Mydemenageur.DAL.Models.Demands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IDemandService
    {
        public Task<Demand> GetDemand(string id);
        public Task<IList<Demand>> GetDemands();
        public Task<IList<Demand>> GetSenderDemands(string senderId);
        public Task<IList<Demand>> GetRecipientDemands(string recipientId);
        public Task<Demand> CreateDemand(DemandCreation demand);
        public Task<string> UpdateDemand(DemandCreation demand);
        public Task<string> DeleteDemand(string id);
    }
}
