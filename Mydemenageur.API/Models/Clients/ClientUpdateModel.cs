using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Models.Clients
{
    public class ClientUpdateModel
    {
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
    }
}
