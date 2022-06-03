using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mydemenageur.DAL.Models.Users;
using System;

namespace Mydemenageur.DAL.Models.Reviews
{
    public class ReviewPopulated
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public MyDemenageurUser Deposer { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Receiver { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Commentaire { get; set; }
    }
}
