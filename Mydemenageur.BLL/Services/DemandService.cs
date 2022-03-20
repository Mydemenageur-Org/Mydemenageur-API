using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.DAL.Models.Demands;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mydemenageur.BLL.Helpers;

namespace Mydemenageur.BLL.Services
{
    public class DemandService : IDemandService
    {
        private readonly IUsersService _usersService;
        private readonly IDPDemand _dpDemand;
        private readonly IDPMyDemenageurUser _dpUser;
        private readonly IMapper _mapper;
        private readonly IFilesService _filesService;

        public DemandService(IUsersService usersService, IDPDemand dPDemand, IDPMyDemenageurUser dpUser, IMapper mapper, IFilesService filesService)
        {
            _usersService = usersService;
            _dpDemand = dPDemand;
            _dpUser = dpUser;
            _mapper = mapper;
            _filesService = filesService;
        }

        public async Task<Demand> GetDemand(string id)
        {
            Demand demand = await _dpDemand.GetDemandById(id).FirstOrDefaultAsync();
            if (demand == null)
                return null;

            return demand;
        }

        public async Task<IList<Demand>> GetDemands()
        {
            IList<Demand> demands = await _dpDemand.Obtain().ToListAsync();
            
            return demands;
        }

        public async Task<IList<DemandMessage>> GetSenderDemands(string senderId)
        {
            List<DemandMessage> demandMessageList = new List<DemandMessage>();
            IList<Demand> demands = await _dpDemand.Obtain().Where(W => W.Sender.Id == senderId).ToListAsync();

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
                    DescriptionDemand = Censure.All(demand.DescriptionDemand),
                    Recipient = mdUserRecipient,
                    Sender = mdUserSender,
                    HasBeenAccepted = demand.HasBeenAccepted,
                    HasBeenDeclined = demand.HasBeenDeclined,
                    Revealed = demand.Revealed
                };

                demandMessageList.Add(demandMessage);
            }

            return demandMessageList;
        }

        public async Task<IList<DemandMessage>> GetRecipientDemands(string recipientId)
        {
            List<DemandMessage> demandMessageList = new List<DemandMessage>();
            IList<Demand> demands = await _dpDemand.Obtain().Where(W => W.Recipient.Id == recipientId).ToListAsync();

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
                    DescriptionDemand = Censure.All(demand.DescriptionDemand),
                    Recipient = mdUserRecipient,
                    Sender = mdUserSender,
                    HasBeenAccepted = demand.HasBeenAccepted,
                    HasBeenDeclined = demand.HasBeenDeclined,
                    Revealed = demand.Revealed
                };

                demandMessageList.Add(demandMessage);
            }

            return demandMessageList;
        }

        public async Task<Demand> CreateDemand(DemandCreation demand)
        {
            MyDemenageurUser recipient = await _dpUser.GetUserById(demand.RecipientId).FirstOrDefaultAsync();
            MyDemenageurUser sender = await _dpUser.GetUserById(demand.SenderId).FirstOrDefaultAsync();

            if (demand.AnnounceId != null)
            {
                if(sender.Role != "ServiceProvider")
                    throw new System.Exception("Incorrect role");
            
                if((sender.RoleType == "Basique" || sender.RoleType == "Intermédiaire") && await _usersService.GetTotalTokens(sender.Id) < 1) 
                    throw new System.Exception("Not enough tokens");
                
                if (sender.RoleType == "Basique" || sender.RoleType == "Intermédiaire")
                {
                    await _usersService.UpdateTokens(sender.Id, new MyDemenageurUserTokens
                    {
                        Value = 1,
                        Operation = "take"
                    });   
                }
            }

            Demand newDemand = new Demand
            {
                PriceProposed = demand.PriceProposed,
                DescriptionDemand = Censure.All(demand.DescriptionDemand),
                Recipient = recipient,
                Sender = sender,
                AnnounceId = demand.AnnounceId,
                ServiceSlug = demand.ServiceSlug,
                HasBeenAccepted = false,
                HasBeenDeclined = false,
                Revealed = false
            };

            await _dpDemand.GetCollection().InsertOneAsync(newDemand);


            return newDemand;
        }

        public async Task<List<Demand>> GetDemandsFromAnnounceId(string id)
        {
            List<Demand> demands = await _dpDemand.GetCollection().Find(x => x.AnnounceId == id).ToListAsync();
            if(demands.Count == 0)
            {
                throw new Exception("No demand found");
            }

            return demands;
        }


        public async Task<IList<DemandMessage>> GetDemandsFromAny(string userId)
        {
            List<DemandMessage> demandMessageList = new List<DemandMessage>();
            IList<Demand> demands = await _dpDemand.Obtain().Where(W => W.Recipient.Id == userId || W.Sender.Id == userId).ToListAsync();

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
                    DescriptionDemand = Censure.All(demand.DescriptionDemand),
                    Recipient = mdUserRecipient,
                    Sender = mdUserSender,
                    HasBeenAccepted = demand.HasBeenAccepted,
                    HasBeenDeclined = demand.HasBeenDeclined,
                    Revealed = demand.Revealed
                };

                demandMessageList.Add(demandMessage);
            }

            return demandMessageList;
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

            if (demandToBeUpdated.Revealed != demand.Revealed)
                if (await _usersService.GetTotalTokens(recipient.Id) < 1)
                    throw new System.Exception("Not enough token to reveal");
                else
                    await _usersService.UpdateTokens(recipient.Id, new MyDemenageurUserTokens {
                        Value = 1,
                        Operation = "take"
                    });

            demandToBeUpdated.PriceProposed = demand.PriceProposed;
            demandToBeUpdated.DescriptionDemand = Censure.All(demand.DescriptionDemand);
            demandToBeUpdated.Recipient = recipient;
            demandToBeUpdated.Sender = sender;
            demandToBeUpdated.HasBeenAccepted = demand.HasBeenAccepted;
            demandToBeUpdated.HasBeenDeclined = demand.HasBeenDeclined;
            demandToBeUpdated.Revealed = demand.Revealed;

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
