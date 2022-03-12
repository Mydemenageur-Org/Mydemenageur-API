using Mydemenageur.DAL.Models.Demands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IDemandService
    {
        public Task<Demand> GetDemand(string id);
        public Task<IList<Demand>> GetDemands();
        public Task<IList<DemandMessage>> GetSenderDemands(string senderId);
        public Task<IList<DemandMessage>> GetRecipientDemands(string recipientId);
        public Task<IList<DemandMessage>> GetDemandsFromAny(string userId);

        public Task<Demand> CreateDemand(DemandCreation demand);
        public Task<DemandMessage> UpdateDemand(DemandCreation demand);
        public Task<string> DeleteDemand(string id);
    }
}
