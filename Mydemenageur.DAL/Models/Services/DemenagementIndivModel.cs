using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.Models.Services
{
    public class DemenagementIndivModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Demenagement Demenagement { get; set; }
        public bool AskHelpStart { get; set; }
        public bool AskHelpDest { get; set; }
        public bool IsFlexibleDate { get; set; }
        public string PersonnNeeded { get; set; }
        public string Volume { get; set; }
    }
}
