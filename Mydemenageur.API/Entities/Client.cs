using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Entities
{
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The id of the user associated with the client
        /// </summary>
        /// <example>6030deb57116e097987bcae5</example>
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        /// <summary>
        /// The user's adress
        /// </summary>
        /// <example>36 Rue des Coquelicots</example>
        public string Adress { get; set; }

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

    }
}
