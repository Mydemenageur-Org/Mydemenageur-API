using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mydemenageur.DAL.Models.Users;

namespace Mydemenageur.DAL.Models.Demands
{
    public class Demand
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public MyDemenageurUser Recipient { get; set; }
        public MyDemenageurUser Sender { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string AnnounceId { get; set; }
        public string PriceProposed { get; set; }
        public string DescriptionDemand { get; set; }
        public bool HasBeenAccepted { get; set; } = false;
        public bool HasBeenDeclined { get; set; } = false;
    }
}