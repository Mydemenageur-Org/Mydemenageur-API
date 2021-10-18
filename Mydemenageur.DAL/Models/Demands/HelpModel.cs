using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mydemenageur.DAL.Entities;
using System;

namespace Mydemenageur.DAL.Models.Demands
{
    public class HelpModel
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
        /// The purpose is to know if the date are flexible
        /// </summary>
        public bool isFlexible { get; set; }

        /// <summary>
        /// The purpose is to know if it is an emergency
        /// </summary>
        public bool isEmergency { get; set; }

        /// <summary>
        /// The plannified date
        /// </summary>
        public DateTime PlannifiedDate { get; set; }

        /// <summary>
        /// The description of the help
        /// </summary>
        /// <example> Bonjour, je recherche de l'aide pour mon déménagement</example>
        public string Description { get; set; }

        /// <summary>
        /// Person number
        /// </summary>
        public string PersonNumber { get; set; }

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

        /// <summary>
        /// The departure city
        /// </summary>
        public string FromCity { get; set; }

        /// <summary>
        /// The final city
        /// </summary>
        public string ToCity { get; set; }

        /// <summary>
        /// The type of provider
        /// </summary>
        /// <example>1 or 0</example>
        public string Type { get; set; }

        public string Volume { get; set; }

        public bool AskHelpStartedCity { get; set; }

        public bool AskHelpEndCity { get; set; }
    }
}
