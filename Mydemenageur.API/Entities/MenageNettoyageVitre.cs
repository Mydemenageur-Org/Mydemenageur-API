using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.Entities
{
    public class MenageNettoyageVitre
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Menage Menage { get; set; }
        public string WindowNumber { get; set; }
    }
}
