using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.Models.Movers
{
    public class MoverUpdateModel
    {
        [Required]
        /// <summary>
        /// The mover's vip status
        /// </summary>
        public bool IsVIP { get; set; }
        [Required]
        /// <summary>
        /// Average customer of the mover's
        /// </summary>
        /// <example>8.5</example>
        public double AverageCustomer { get; set; }
    }
}
