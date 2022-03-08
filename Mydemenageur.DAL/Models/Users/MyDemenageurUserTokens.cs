namespace Mydemenageur.DAL.Models.Users
{
    public class MyDemenageurUserTokens
    {
        /// <summary>
        /// Amount of token to operate
        /// </summary>
        /// <example>User</example>
        public int Value { get; set; }

        /// <summary>
        /// Operation type
        /// </summary>
        /// <example>add</example>
        public string Operation { get; set; } = "take";
    }
}
