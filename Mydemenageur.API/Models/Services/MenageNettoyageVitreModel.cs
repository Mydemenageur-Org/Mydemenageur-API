using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mydemenageur.API.Models.Services
{
    public class MenageNettoyageVitreModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string MenageId { get; set; }
        public string WindowNumber { get; set; }
    }
}
