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
        /// The user's adress
        /// </summary>
        /// <example>36 Rue des Coquelicots</example>
        public string Area { get; set; }
        /// <summary>
        /// The user's town
        /// </summary>
        /// <example>Rennes</example>
        public string Town { get; set; }
        /// <summary>
        /// The user's zipcode
        /// </summary>
        /// <example>85250</example>
        public string Zipcode { get; set; }
        /// <summary>
        /// The user's country
        /// </summary>
        /// <example>France</example>
        public string Country { get; set; }
        /// <summary>
        /// The movers's region 
        /// </summary>
        /// <example>Provence-Alpes-Côte d'Azur</example>
        public string Region { get; set; }
        /// <summary>
        /// The mover's vip status
        /// </summary>
        public Boolean VIP { get; set; }
        /// <summary>
        /// The mover's society Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SocietyId { get; set; }
        /// <summary>
        /// Average customer of the mover's
        /// </summary>
        /// <example>8.5</example>
        public float AverageCustomer { get; set; }

    }
}
