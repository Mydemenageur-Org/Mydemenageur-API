using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mydemenageur.DAL.Models.Demands
{
    public class DemandCreation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string SenderId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string RecipientId { get; set; }
        public string PriceProposed { get; set; }
        public string DescriptionDemand { get; set; }
    }
}
