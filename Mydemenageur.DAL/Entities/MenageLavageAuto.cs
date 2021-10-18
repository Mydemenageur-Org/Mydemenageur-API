using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.Entities
{
    public class MenageLavageAuto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Menage Menage { get; set; }
        public string VehiculeType { get; set; }
    }
}
