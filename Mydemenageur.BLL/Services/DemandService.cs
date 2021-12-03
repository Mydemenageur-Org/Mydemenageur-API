using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.Demands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services
{
    public class DemandService: IDemandService
    {
        private readonly IDPDemand _dpDemand;

        public DemandService(IDPDemand dPDemand)
        {
            _dpDemand = dPDemand;
        }

        public async Task<Demand> GetDemand(string id)
        {
            return null;
        }

        public async Task<List<Demand>> GetDemands()
        {
            return null;
        }

        public async Task<List<Demand>> GetSenderDemands(string senderId)
        {
            return null;
        }

        public async Task<List<Demand>> GetRecipientDemands(string recipientId)
        {
            return null;
        }

        public async Task<Demand> CreateDemand(DemandCreation demand)
        {
            return null;
        }

        public async Task<string> UpdateDemand(DemandCreation demand)
        {
            return null;
        }


    }
}
