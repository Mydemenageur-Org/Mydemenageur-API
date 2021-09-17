﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.Models.Services
{
    public class MenageDomicileModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Menage Menage { get; set; }
        public string Surface { get; set; }
        public string AdditionnalNeeds { get; set; }
        public string Frequency { get; set; }
    }
}
