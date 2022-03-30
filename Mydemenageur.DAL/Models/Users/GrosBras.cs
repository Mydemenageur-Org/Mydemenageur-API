using MongoDB.Bson.Serialization.Attributes;
using System;
using Mydemenageur.DAL.Models.Experiences;
using System.Collections.Generic;

namespace Mydemenageur.DAL.Models.Users
{
    public class GrosBras
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("MyDemenageurUserId")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string MyDemenageurUserId { get; set; }
        /// <summary>
        /// Correspond to the services the user can propose as a potential "Gros Bras"
        /// </summary>
        /// <example>1</example>
        public string[] ServicesProposed { get; set; }
        /// <summary>
        /// It displays all diploma or certificates the "Gros Bras" has
        /// </summary>
        /// <example>1</example>
        public Experience[] DiplomaOrExperiences { get; set; }
        /// <summary>
        /// Correspond to a description of the "Gros Bras"
        /// </summary>
        /// <example>1</example>
        public string Description { get; set; }
        /// <summary>
        /// It is all commitments of the "Gros Bras"
        /// </summary>
        /// <example>1</example>
        public string[] Commitment { get; set; }
        /// <summary>
        /// It is the professional status of the "Gros Bras"
        /// </summary>
        /// <example>1</example>
        public string ProStatus { get; set; }
        public string Siren { get; set; }
        public string LicenceTransport { get; set; }
[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CityId { get; set; } = null;
        public string Departement { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int VeryGoodGrade { get; set; } = 0;
        public int GoodGrade { get; set; } = 0;
        public int MediumGrade { get; set; } = 0;
        public int BadGrade { get; set; } = 0;
        public string[] Equipment { get; set; }
        public string Rayon { get; set; } = "30";
        public bool IsPro { get; set; } = false;
        public bool IsVerified { get; set; } = false;
        public bool MyDemCert { get; set; } = false;
        public bool MyJugCert { get; set; } = false;
        public bool Cesu { get; set; } = false;
        public string Title { get; set; }
        public string Formula { get; set; }

        public IList<string> Realisations { get; set; } = new List<string>();
    }
}
