using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.Models.Services
{
    public class DemenagementProModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Demenagement Demenagement { get; set; }
        public bool IsHouse { get; set; }
        public bool HasMultipleFloors { get; set; }
        public string StartZipCode { get; set; }
        public string StartAddress { get; set; }
        public string DestZipCode { get; set; }
        public string DestAddress { get; set; }
        public bool IsStartingDateKnown { get; set; }
        public string? Volume { get; set; }
        public string? Surface { get; set; }
    }
}
