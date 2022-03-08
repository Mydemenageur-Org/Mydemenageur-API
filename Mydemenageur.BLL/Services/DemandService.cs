﻿using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.DAL.Models.Demands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Mydemenageur.BLL.Services
{
    public class DemandService : IDemandService
    {
        private readonly IDPDemand _dpDemand;
        private readonly IDPMyDemenageurUser _dpUser;
        private readonly IMapper _mapper;
        private readonly IFilesService _filesService;

        public DemandService(IDPDemand dPDemand, IDPMyDemenageurUser dpUser, IMapper mapper, IFilesService filesService)
        {
            _dpDemand = dPDemand;
            _dpUser = dpUser;
            _mapper = mapper;
            _filesService = filesService;
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

        public async Task<IList<DemandMessage>> GetRecipientDemands(string recipientId)
        {
            List<DemandMessage> demandMessageList = new List<DemandMessage>();
            IList<Demand> demands = await _dpDemand.Obtain().Where(W => W.Recipient.Id == recipientId || W.Sender.Id == recipientId).ToListAsync();

            foreach (Demand demand in demands)
            {
                MyDemenageurUserPopulated mdUserSender = _mapper.Map<MyDemenageurUserPopulated>(demand.Sender);
                MyDemenageurUserPopulated mdUserRecipient = _mapper.Map<MyDemenageurUserPopulated>(demand.Recipient);
                if (demand.Sender.ProfilePictureId != null)
                {
                    byte[] senderProfilPicture = (await _filesService.GetFile(demand.Sender.ProfilePictureId)).Data;
                    mdUserSender.ProfilPictureId = senderProfilPicture;
                }

                if (demand.Recipient.ProfilePictureId != null)
                {
                    byte[] recipientProfilPicture = (await _filesService.GetFile(demand.Recipient.ProfilePictureId)).Data;
                    mdUserRecipient.ProfilPictureId = recipientProfilPicture;
                }

                DemandMessage demandMessage = new DemandMessage
                {
                    Id = demand.Id,
                    PriceProposed = demand.PriceProposed,
                    AnnounceId = demand.AnnounceId,
                    DescriptionDemand = demand.DescriptionDemand,
                    Recipient = mdUserRecipient,
                    Sender = mdUserSender,
                    HasBeenAccepted = demand.HasBeenAccepted,
                    HasBeenDeclined = demand.HasBeenDeclined
                };

                demandMessageList.Add(demandMessage);
            }

            return demandMessageList;
        }

        public async Task<Demand> CreateDemand(DemandCreation demand)
        {
            MyDemenageurUser recipient = await _dpUser.GetUserById(demand.RecipientId).FirstOrDefaultAsync();
            MyDemenageurUser sender = await _dpUser.GetUserById(demand.SenderId).FirstOrDefaultAsync();

            string tokenAmount = sender.MDToken;
            if(int.Parse(tokenAmount) < 1 && sender.Role == "ServiceProvider")
            {
                return null;
            }

            Demand newDemand = new Demand()
            {
                PriceProposed = demand.PriceProposed,
                DescriptionDemand = demand.DescriptionDemand,
                Recipient = recipient,
                Sender = sender,
                AnnounceId = demand.AnnounceId,
                ServiceSlug = demand.ServiceSlug,
                HasBeenAccepted = false,
                HasBeenDeclined = false,
            };

            if (sender.Role == "ServiceProvider")
            {
                sender.MDToken = (int.Parse(sender.MDToken) - 1).ToString();

                await _dpUser.GetCollection().ReplaceOneAsync(d => d.Id == sender.Id, sender);

            }

            await _dpDemand.GetCollection().InsertOneAsync(newDemand);


            return newDemand;
        }

        public async Task<DemandMessage> UpdateDemand(DemandCreation demand)
        {
            Demand demandToBeUpdated = await _dpDemand.GetDemandById(demand.Id).FirstOrDefaultAsync();
            if(demandToBeUpdated == null) { throw new System.Exception("Demand not found"); }
            MyDemenageurUser recipient = await _dpUser.GetUserById(demand.RecipientId).FirstOrDefaultAsync();
            if(recipient == null) {
                throw new System.Exception("Recipient not found");
            }
            MyDemenageurUser sender = await _dpUser.GetUserById(demand.SenderId).FirstOrDefaultAsync();
            if(sender == null)
            {
                throw new System.Exception("Sender not found");
            }


            demandToBeUpdated.PriceProposed = demand.PriceProposed;
            demandToBeUpdated.DescriptionDemand = demand.DescriptionDemand;
            demandToBeUpdated.Recipient = recipient;
            demandToBeUpdated.Sender = sender;
            demandToBeUpdated.HasBeenAccepted = demand.HasBeenAccepted;
            demandToBeUpdated.HasBeenDeclined = demand.HasBeenDeclined;

            await _dpDemand.GetCollection().ReplaceOneAsync(
                dpDemand => dpDemand.Id == demand.Id,
                demandToBeUpdated
            );

            DemandMessage demandFromMessage = _mapper.Map<DemandMessage>(demandToBeUpdated);

            return demandFromMessage;
        }

        public async Task<string> DeleteDemand(string id)
        {
            return null;
        }


    }
}
