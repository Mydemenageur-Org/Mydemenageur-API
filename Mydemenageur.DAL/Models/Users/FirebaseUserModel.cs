using System;

namespace Mydemenageur.DAL.Models.Users
{
    public class FirebaseUserModel
    {
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
        /// The user's email
        /// </summary>
        /// <example>Victor</example>
        public string Email { get; set; }
        /// <summary>
        /// The user's phone
        /// </summary>
        /// <example>+33652809335</example>
        public string Phone { get; set; }
        /// <summary>
        /// The user's firebase profilPicture
        /// </summary>
        /// <example>Victor</example>
        public string ProfilPictureFirebase { get; set; }
        /// <summary>
        /// The user's birthday
        /// </summary>     
        public DateTime Birthday { get; set; }
        /// <summary>
        /// The last connection of the mover
        /// </summary>
        public DateTime LastConnexion { get; set; }
        /// <summary>
        /// The user's role. The roles are : Admin, Client, Mover
        /// </summary>
        /// <example>Client</example>
        public string Role { get; set; }
        public DateTime SignUpDate { get; set; }
        /// <summary>
        /// The user's gender
        /// </summary>
        public string Gender { get; set; }
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
        /// Bool that provide firebase account informations 
        /// </summary>
        public Boolean IsFirebaseAccount { get; set; }
    }
}
