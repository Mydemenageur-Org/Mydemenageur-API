using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Mydemenageur.DAL.Models.Reviews
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public bool EtatRef { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserIdRefDepot { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserIdRefRecu { get; set; }
        public string Avis { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public string IpRedaction { get; set; }
        public string UserAgent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ModerationDate { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string[] Commentaires { get; set; }
    }
}
