using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Mydemenageur.DAL.Entities
{
    public class Demenagement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string DemenagementId { get; set; }
        public DateTime? MoveDate { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string TitleAnnounce { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ServiceType { get; set; }
        public string MoveType{ get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
    }
}
