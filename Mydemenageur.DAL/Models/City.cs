using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Mydemenageur.DAL.Models
{
    public class City
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Label { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int SearchCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
