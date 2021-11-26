using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

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
        public string FirstName { get; set; }
        /// <summary>
        /// The user's birthday
        /// </summary>     
        public DateTime Birthday { get; set; }
        /// <summary>
        /// The user's last name
        /// </summary>
        /// <example>DENIS</example>
        public string LastName { get; set; }
        /// <summary>
        /// The user's gender
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// The user's email
        /// </summary>
        /// <example>admin@feldrise.com</example>
        /// 
        public string Email { get; set; }
        /// <summary>
        /// The user's phone
        /// </summary>
        /// <example>+33652809335</example>
        public string Phone { get; set; }
        /// <summary>
        /// The user's username
        /// </summary>
        /// <example>Feldrise</example>
        public string Username { get; set; }
        /// <summary>
        /// The user's about
        /// </summary>
        public string About { get; set; }
        /// <summary>
        /// The user's notification settings for email
        /// </summary>
        /// <example>1</example>
        public string EmailNotification { get; set; } = "";
        /// <summary>
        /// The user's notification settings for phone
        /// </summary>
        /// <example>1</example>
        public string PhoneNotification { get; set; } = "";
        /// <summary>
        /// The user's address
        /// </summary>
        /// <example>119 rue fondaudege</example>
        public string Address { get; set; } = "";
        /// <summary>
        /// The user's complementary addresss
        /// </summary>
        /// <example>de la rue fausse</example>
        public string ComplementaryAddress { get; set; } = "";
        /// <summary>
        /// The user's zipcode
        /// </summary>
        /// <example>01300</example>
        public string ZipCode { get; set; } = "";
        /// <summary>
        /// The user's city
        /// </summary>
        /// <example>Paris</example>
        public string City { get; set; } = "";
        /// <summary>
        /// Token duplicate
        /// </summary>
        /// <summary>
        /// The user's password
        /// </summary>
        /// <example>MySecurePassword</example>
        /// 
        public string Password { get;  set; }

    }
}
