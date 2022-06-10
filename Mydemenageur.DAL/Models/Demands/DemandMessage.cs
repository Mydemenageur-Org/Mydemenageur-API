﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mydemenageur.DAL.Models.Users;

namespace Mydemenageur.DAL.Models.Demands
{
    public class DemandMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public MyDemenageurUserPopulated Recipient { get; set; }
        public MyDemenageurUserPopulated Sender { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string AnnounceId { get; set; }
        public string PriceProposed { get; set; }
        public string DescriptionDemand { get; set; }
        public bool HasBeenAccepted { get; set; }
        public bool HasBeenDeclined { get; set; }
        public bool Revealed { get; set; }
        public bool Archived { get; set; }
        //Feat/Messagerie -- Maxime.M 13/05/2022
        public bool ConversationClosed { get; set; } = false;
        //Feat/add-notif-for-demand -- Maxime.M 09/06/22
        public bool Unread { get; set; } 
    }
}
