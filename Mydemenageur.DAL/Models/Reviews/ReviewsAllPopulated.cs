using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mydemenageur.DAL.Models.Users;
using System;

namespace Mydemenageur.DAL.Models.Reviews
{
    public class ReviewAllopulated
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public MyDemenageurUser Deposer { get; set; }
        public MyDemenageurUser Receiver { get; set; }
        public GrosBrasPopulated ReceiverProfil { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string[] Commentaires { get; set; }
    }
}