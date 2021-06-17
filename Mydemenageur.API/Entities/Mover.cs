using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Entities
{
    public class Mover
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// The id of the user associated with the mover
        /// </summary>
        /// <example>6030deb57116e097987bcae5</example>
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        /// <summary>
        /// The mover's vip status
        /// </summary>
        public bool IsVIP { get; set; }
        /// <summary>
        /// Average customer of the mover's
        /// </summary>
        /// <example>8.5</example>
        public float AverageCustomerRating { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// The file is list
        /// </summary>
        public List<string> FileIds { get; set; }
    }
}
