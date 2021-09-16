using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mydemenageur.API.Models.Services
{
    public class MenageDomicileModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string MenageId { get; set; }
        public string Surface { get; set; }
        public string AdditionnalNeeds { get; set; }
        public string Frequency { get; set; }
    }
}
