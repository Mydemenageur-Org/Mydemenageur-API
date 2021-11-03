﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace Mydemenageur.DAL.Models.Users
{
    public class MyDemenageurUser
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// The user's profile picture id 
        /// </summary>
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ProfilePictureId { get; set; }
        /// <summary>
        /// The user's id
        /// </summary>
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; }
        /// <summary>
        /// The user's first name
        /// </summary>
        /// <example>Victor</example>
        public string FirstName { get; set; }
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
        /// The user's phone
        /// </summary>
        /// <example>+33652809335</example>
        public string Phone { get; set; }
        /// <summary>
        /// The user's birthday
        /// </summary>     
        public DateTime Birthday { get; set; }
        /// <summary>
        /// The user's signup date
        /// </summary>     
        public DateTime SignupDate { get; set; }
        /// <summary>
        /// The last connection of the mover
        /// </summary>
        public DateTime LastConnection { get; set; }
        /// <summary>
        /// The user's username
        /// </summary>
        /// <example>Feldrise</example>
        public string Username { get; set; }
        /// <summary>
        /// The user's about
        /// </summary>
        /// <example>Je suis un super jeune qui déménage tous les jours</example>
        public string About { get; set; }
        /// <summary>
        /// The user's role. The roles are : Admin, Client, Mover
        /// </summary>
        /// <example>Client</example>
        public string Role { get; set; }

        /// <summary>
        /// Token duplicate
        /// </summary>
        public string Token { get; set; }
    }
}