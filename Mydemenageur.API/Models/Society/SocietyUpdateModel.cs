using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Models.Society
{
    public class SocietyUpdateModel
    {
        [Required]
        /// <summary>
        /// The society name
        /// </summary>
        public string SocietyName { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        /// <summary>
        /// The society manager first name
        /// </summary>
        public string ManagerId { get; set; }
        [Required]
        /// <summary>
        /// The employee number
        /// </summary>
        /// <example>36</example>
        public int EmployeeNumber { get; set; }
        [Required]
        /// <summary>
        /// The user's Address
        /// </summary>
        /// <example>36 Rue des Coquelicots</example>
        public string Address { get; set; }
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

    }
}
