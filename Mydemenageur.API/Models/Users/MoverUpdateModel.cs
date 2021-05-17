using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Models.Users
{
    public class MoverUpdateModel
    {
        [Required]
        /// <summary>
        /// The user's adress
        /// </summary>
        /// <example>36 Rue des Coquelicots</example>
        public string Adress { get; set; }
        [Required]
        /// <summary>
        /// The user's town
        /// </summary>
        /// <example>Rennes</example>
        public string Town { get; set; }
        [Required]
        /// <summary>
        /// The user's zipcode
        /// </summary>
        /// <example>85250</example>
        public string Zipcode { get; set; }
        [Required]
        /// <summary>
        /// The user's country
        /// </summary>
        /// <example>France</example>
        public string Country { get; set; }
        [Required]
        /// <summary>
        /// The movers's region 
        /// </summary>
        /// <example>Provence-Alpes-Côte d'Azur</example>
        public string Region { get; set; }
        [Required]
        /// <summary>
        /// The mover's vip status
        /// </summary>
        public Boolean VIP { get; set; }
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
