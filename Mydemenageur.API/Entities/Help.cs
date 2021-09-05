using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Mydemenageur.API.Entities
{
    public class Help
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The id of the user associated to the help card
        /// </summary>
        /// <example>6030deb57116e097987bcae5</example>
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        /// <summary>
        /// The title help's card
        /// </summary>
        /// <example>Déménagement d'un 100 m2 à Bordeaux</example>
        public string Title { get; set; }

        /// <summary>
        /// The date when it has been created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The description of the help
        /// </summary>
        /// <example> Bonjour, je recherche de l'aide pour mon déménagement</example>
        public string Description { get; set; }

        /// <summary>
        /// Person number
        /// </summary>
        public int PersonNumber { get; set; }

        /// <summary>
        /// The time needed to finish the work
        /// </summary>
        public DateTime TimeNeeded { get; set; } 

    }
}
