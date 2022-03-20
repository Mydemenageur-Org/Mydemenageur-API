using System.Text.RegularExpressions;

namespace Mydemenageur.BLL.Helpers
{
    public class Censure
    {
        public static string PhoneNumbers(string value)
        {
            return Regex.Replace(value, 
                @"(?:(?:\+|00)33|0)\s*[1-9](?:[\s.-]*\d{2}){4}", 
                "******");
        }
        
        public static string Emails(string value)
        {
            return Regex.Replace(value, 
                @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", 
                "******");
        }
        
        public static string All(string value)
        {
            return PhoneNumbers(Emails(value));
        }
    }
}