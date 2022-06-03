using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Mydemenageur.DAL.Models.Reviews
{
    public class ReviewUpdater
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Deposer { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Receiver { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Commentaire { get; set; }
        public string LastNote { get; set; }
    }
}