using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.Models.Users
{
    public class UserUpdateModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// The user's profile picture id 
        /// </summary>
        public byte[] ProfilePicture { get; set; }

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
        [Required]
        /// <summary>
        /// The user's gender
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// The user's email
        /// </summary>
        /// <example>admin@feldrise.com</example>
        /// 
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
        [Required]
        public string About { get; set; }

        /// <summary>
        /// The user's password
        /// </summary>
        /// <example>MySecurePassword</example>
        public string Password { get;  set; }

    }
}
