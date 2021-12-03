using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.DAL.Models.Demands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services
{
    public class DemandService : IDemandService
    {
        private readonly IDPDemand _dpDemand;
        private readonly IDPMyDemenageurUser _dpUser;

        public DemandService(IDPDemand dPDemand, IDPMyDemenageurUser dpUser)
        {
            _dpDemand = dPDemand;
            _dpUser = dpUser;
        }

        public async Task<Demand> GetDemand(string id)
        {
            Demand demand = await _dpDemand.Obtain().Where(x => x.AnnounceId == id).FirstOrDefaultAsync();

            return demand;
        }

        public async Task<IList<Demand>> GetDemands()
        {
            IList<Demand> demands = await _dpDemand.Obtain().ToListAsync();
            return demands;
        }

        public async Task<IList<Demand>> GetSenderDemands(string senderId)
        {
            IList<Demand> demand = await _dpDemand.Obtain().Where(W => W.Sender.Id == senderId).ToListAsync();

            return demand;
        }

        public async Task<IList<Demand>> GetRecipientDemands(string recipientId)
        {
            IList<Demand> demand = await _dpDemand.Obtain().Where(W => W.Recipient.Id == recipientId).ToListAsync();

            return demand;
        }

        public async Task<Demand> CreateDemand(DemandCreation demand)
        {
            MyDemenageurUser recipient = await _dpUser.GetUserById(demand.RecipientId).FirstOrDefaultAsync();
            MyDemenageurUser sender = await _dpUser.GetUserById(demand.SenderId).FirstOrDefaultAsync();

            string tokenAmount = sender.MDToken;
            if(int.Parse(tokenAmount) < 1)
            {
                return null;
            }

            Demand newDemand = new Demand()
            {
                PriceProposed = demand.PriceProposed,
                DescriptionDemand = demand.DescriptionDemand,
                Recipient = recipient,
                Sender = sender,
                AnnounceId = demand.AnnounceId
            };

            sender.MDToken = (int.Parse(sender.MDToken) - 1).ToString();

            await _dpUser.GetCollection().ReplaceOneAsync(d => d.Id == sender.Id, sender);

            await _dpDemand.GetCollection().InsertOneAsync(newDemand);


            return newDemand;
        }

        public async Task<string> UpdateDemand(DemandCreation demand)
        {
            Demand demandToBeUpdated = await _dpDemand.GetDemandById(demand.Id).FirstOrDefaultAsync();
            MyDemenageurUser recipient = await _dpUser.GetUserById(demand.RecipientId).FirstOrDefaultAsync();
            MyDemenageurUser sender = await _dpUser.GetUserById(demand.SenderId).FirstOrDefaultAsync();

            demandToBeUpdated.PriceProposed = demand.PriceProposed;
            demandToBeUpdated.DescriptionDemand = demand.DescriptionDemand;
            demandToBeUpdated.Recipient = recipient;
            demandToBeUpdated.Sender = sender;

            await _dpDemand.GetCollection().ReplaceOneAsync(
                dpDemand => dpDemand.Id == demand.Id,
                demandToBeUpdated
            );

            return demandToBeUpdated.Id;
        }

        public async Task<string> DeleteDemand(string id)
        {
            return null;
        }


    }
}
