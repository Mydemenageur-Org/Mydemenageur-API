using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Mydemenageur.DAL.Entities
{
    public class Menage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DateEvent { get; set; }
        public DateTime EstimatedTime { get; set; }
        public string Budget { get; set; }
        public string MenageType { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public bool IsPro {get; set;}
    }
}
