using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Models.Movers
{
    public class MoverUpdateModel
    {
        [Required]
        /// <summary>
        /// The mover's vip status
        /// </summary>
        public bool IsVIP { get; set; }
        [Required]
        /// <summary>
        /// The mover's society Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SocietyId { get; set; }
        [Required]
        /// <summary>
        /// Average customer of the mover's
        /// </summary>
        /// <example>8.5</example>
        public float AverageCustomer { get; set; }
    }
}
