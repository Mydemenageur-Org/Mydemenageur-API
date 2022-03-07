namespace Mydemenageur.DAL.Models.Users
{
    public class MyDemenageurUserRole
    {
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
    }
}
