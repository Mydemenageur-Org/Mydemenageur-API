using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mydemenageur.DAL.Entities
{
    public class Depannage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Category { get; set; }
        public string ZipCode { get; set; }
        public bool IsEmergency { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public bool isPro { get; set; }
    }
}
