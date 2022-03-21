using Newtonsoft.Json;
namespace Mydemenageur.DAL.Models.Stripe
{
    public class CustomerPortal
    {
        [JsonProperty("customerId")]
        public string CustomerId { get; set; }
        
        [JsonProperty("returnURL")]
        public string ReturnURL { get; set; }
    }
}
