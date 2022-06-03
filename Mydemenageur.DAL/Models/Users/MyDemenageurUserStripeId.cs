using Newtonsoft.Json;

namespace Mydemenageur.DAL.Models.Users
{
    public class MyDemenageurUserStripeId
    {
        [JsonProperty("stripeId")]
        public string StripeId { get; set; }
    }
}
