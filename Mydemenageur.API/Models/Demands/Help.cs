using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Mydemenageur.API.Models.Demands
{
    public class Help
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

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

        /// <summary>
        /// The firstname of the author
        /// </summary>
        /// <example> Léo </example>
        public string FirstName { get; set; }

        /// <summary>
        /// The lastname of the author
        /// </summary>
        /// <example> Delpon </example>
        public string LastName { get; set; }

    }
}
