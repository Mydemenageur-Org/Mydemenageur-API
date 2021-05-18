using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Models.Society
{
    public class SocietyRegisterModel
    {
        /// <summary>
        /// The society name
        /// </summary>
        public string SocietyName { get; set; }
        /// <summary>
        /// The society manager first name
        /// </summary>
        public string ManagerFirstName { get; set; }
        /// <summary>
        /// The society manager last name
        /// </summary>
        public string ManagerLastName { get; set; }
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
        /// <summary>
        /// The movers's region 
        /// </summary>
        /// <example>Provence-Alpes-Côte d'Azur</example>
        public string Region { get; set; }

    }
}
