using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Mydemenageur.API.Models.Services
{
    public class CartonModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ResearchType { get; set; }
        public string BoxNumber { get; set; }
        public string BoxSize { get; set; }
        public DateTime? DateDisponibility { get; set; }
        public bool isFlexible { get; set; }
        public DateTime? StartDisponibility { get; set; }
        public DateTime? EndDisponibility { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string AnnounceTitle { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
    }
}
