namespace Mydemenageur.DAL.Models.Users
{
    public class MyDemenageurUserTokens
    {
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
    }
}
