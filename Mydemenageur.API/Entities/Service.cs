using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mydemenageur.API.Entities
{
    public class Service
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// The label of the specified service
        /// </summary>
        /// <example>Bricolage à domicile</example>
        public string Label { get; set; }
        /// <summary>
        /// The picture of the service
        /// </summary>
        public byte Picture { get; set; }
        
    }
}
