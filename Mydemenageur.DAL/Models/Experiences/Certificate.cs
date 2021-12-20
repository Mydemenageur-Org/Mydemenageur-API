

using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Mydemenageur.DAL.Models.Experiences
{
    public class Certificate
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Label { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
