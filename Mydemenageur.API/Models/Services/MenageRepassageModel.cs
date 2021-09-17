using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.Models.Services
{
    public class MenageRepassageModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Menage Menage { get; set; }
        public string ClotheNumber { get; set; }
        public string Frequency { get; set; }
    }
}
