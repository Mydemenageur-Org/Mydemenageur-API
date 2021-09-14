﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Entities
{
    public class Demarche
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime? DemenagementDate { get; set; }
        public bool HasAlreadyMovedIn { get; set; }
        public bool EnergyNotification { get; set; }
        public bool BoxAndMobileNotification { get; set; }
        public bool Assurance { get; set; }
        public bool AlarmAndMonitor { get; set; }
        public bool BankAccount { get; set; }
        public bool GrayCard { get; set; }
        public bool DevisDemenagement { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}