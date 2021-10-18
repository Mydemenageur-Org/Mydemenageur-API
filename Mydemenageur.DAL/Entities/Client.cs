using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.Entities
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
        /// The user's Address
        /// </summary>
        /// <example>36 Rue des Coquelicots</example>
        public string Address { get; set; }

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
        /// The user's civility
        /// </summary>
        /// <example>France</example>
        public string Civility { get; set; }

        /// <summary>
        /// The user's firstname
        /// </summary>
        /// <example>France</example>
        public string Firstname { get; set; }

        /// <summary>
        /// The user's lastname
        /// </summary>
        /// <example>France</example>
        public string Lastname { get; set; }

        /// <summary>
        /// The user's phoneNumber
        /// </summary>
        /// <example>France</example>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        /// <example>France</example>
        public string Email { get; set; }
    }
}
