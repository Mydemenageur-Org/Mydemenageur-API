using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Mydemenageur.DAL.Models.Experiences
{
    public class Experience
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Years { get; set; }
        public Certificate Diplome { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
