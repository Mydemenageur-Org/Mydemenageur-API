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

        [BsonRepresentation(BsonType.ObjectId)]
        public string AnnounceId { get; set; } = null;
        public string ServiceSlug { get; set; }
        public string PriceProposed { get; set; }
        public string DescriptionDemand { get; set; }
        public bool HasBeenAccepted { get; set; }
        public bool HasBeenDeclined { get; set; }
        public bool Revealed { get; set; }
        public bool Archived { get; set; }
    }
}
