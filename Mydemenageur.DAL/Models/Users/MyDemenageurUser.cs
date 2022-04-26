using MongoDB.Bson.Serialization.Attributes;
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
        /// The user's email
        /// </summary>
        /// <example>Victor</example>
        public string Email { get; set; } = "";
        /// <summary>
        /// The user's firebase profilPicture
        /// </summary>
        /// <example>Victor</example>
        public string ProfilPictureFirebase { get; set; } = "";
        /// <summary>
        /// The user's first name
        /// </summary>
        /// <example>Victor</example>
        public string FirstName { get; set; } = "";
        /// <summary>
        /// The user's last name
        /// </summary>
        /// <example>DENIS</example>
        public string LastName { get; set; } = "";
        /// <summary>
        /// The user's gender
        /// </summary>
        public string Gender { get; set; } = "";
        /// <summary>
        /// The user's phone
        /// </summary>
        /// <example>+33652809335</example>
        public string Phone { get; set; } = "";
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
        public string Username { get; set; } = "";
        /// <summary>
        /// The user's about
        /// </summary>
        /// <example>Je suis un super jeune qui déménage tous les jours</example>
        public string About { get; set; } = "";
        /// <summary>
        /// The user's role. The roles are : ServiceProvider, User
        /// </summary>
        /// <example>User</example>
        public string Role { get; set; } = "User";
        /// <summary>
        /// The user's role type.
        /// </summary>
        /// <example>Free</example>
        public string RoleType { get; set; } = "";
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
        /// The user's tokens
        /// </summary>
        /// <example>50</example>
        public string MDToken { get; set; } = "0";
        /// <summary>
        /// The user's given tokens
        /// </summary>
        /// <example>50</example>
        public int FreeTokens { get; set; } = 0;
        /// <summary>
        /// The user's paid tokens
        /// </summary>
        /// <example>50</example>
        public int PaidTokens { get; set; } = 0;
        /// <summary>
        /// The user's notification settings for email
        /// </summary>
        /// <example>1</example>
        public string[] EmailNotification { get; set; }
        /// <summary>
        /// The user's notification settings for phone
        /// </summary>
        /// <example>1</example>
        public string PhoneNotification { get; set; } = "0";
        /// <summary>
        /// Correspond to the services the user can propose as a potential "Gros Bras"
        /// </summary>
        /// <example>1</example>
        public string[] ServicesProposed { get; set; }

        /// <summary>
        /// Token duplicate
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Bool that provide firebase account informations 
        /// </summary>
        public Boolean IsFirebaseAccount { get; set; } = false;
        /// <summary>
        /// Formula subscribed by the "Gros Bras"
        /// UNUSED ANYMORE TO DELETE
        /// </summary>
        public string Formula { get; set; }
        /// <summary>
        /// Stripe customer's id
        /// </summary>
        public string StripeId { get; set; }
    }
}
