using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Mydemenageur.DAL.Entities
{
    public class Picture
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string Filename { get; set; }
        [Required]
        public byte[] Data { get; set; }
        public string Description { get; set; }
    }
}
