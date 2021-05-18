using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Models.Movers
{
    public class MoverRegisterModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// The user id associate with the mover
        /// </summary>
        /// <example>6030deb57116e097987bcae5</example>
        public string UserId { get; set; }
        /// <summary>
        /// The mover's adress
        /// </summary>
        /// <example>36 Rue les Coquelicots</example>
        public string Adress { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// The mover's society id
        /// </summary>
        /// <example>6030deb57116e097987bcae5</example>
        public string SocietyId { get; set; }
        /// <summary>
        /// The mover's town
        /// </summary>
        /// <example>Rennes</example>
        public string Town { get; set; }
        /// <summary>
        /// The mover's zipcode
        /// </summary>
        /// <example>85160</example>
        public string Zipcode { get; set; }
        /// <summary>
        /// The mover's country
        /// </summary>
        /// <example>France</example>
        public string Country { get; set; }

    }
}
