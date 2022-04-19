namespace Mydemenageur.DAL.Models.Users
{
    public class CheckUserName
    {
        /// <summary>
        /// The user's email
        /// </summary>
        /// <example>admin@feldrise.com</example>
        public string Email { get; set; }
        /// <summary>
        /// The user's username
        /// </summary>
        /// <example>Feldrise</example>
        public string Username { get; set; }
    }
}