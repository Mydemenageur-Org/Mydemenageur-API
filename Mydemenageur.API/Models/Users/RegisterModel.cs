using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Models.Users
{
    public class RegisterModel
    {
        /// <summary>
        /// The user's profile picture id 
        /// </summary>
        public string ProfilePicture { get; set; }
        /// <summary>
        /// The user's first name
        /// </summary>
        /// <example>Victor</example>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// The user's last name
        /// </summary>
        /// <example>DENIS</example>
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// The user's email
        /// </summary>
        /// <example>admin@feldrise.com</example>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// The user's phone
        /// </summary>
        /// <example>+33652809335</example>
        [Required]
        public string Phone { get; set; }
        /// <summary>
        /// The user's username
        /// </summary>
        /// <example>Feldrise</example>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// The user's about
        /// </summary>
        /// <example>Hello it's me</example>
        public string About { get; set; }
        [Required]
        /// <summary>
        /// The user's password
        /// </summary>
        /// <example>My very secure password</example>
        public string Password { get; set; }
        [Required]
        /// <summary>
        /// The role of user 
        /// </summary>
        /// <example>Client</example>
        public string Role { get; set; }
    }
}
