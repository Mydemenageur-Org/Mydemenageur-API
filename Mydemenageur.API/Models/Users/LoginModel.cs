using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Models.Users
{
    public class LoginModel
    {
        /// <summary>
        /// The user's username or the user's
        /// email
        /// </summary>
        /// <example>admin@feldrise.com</example>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// The user's password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
