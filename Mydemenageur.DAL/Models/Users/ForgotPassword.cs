
namespace Mydemenageur.DAL.Models.Users
{
    public class ForgotPassword
    {
        public string email { get; set; }
        public string token { get; set; }
        public string password { get; set; }
    }
}
