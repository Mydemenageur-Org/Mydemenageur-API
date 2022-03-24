using Newtonsoft.Json;

namespace Mydemenageur.DAL.Models.Stripe
{
    public class CreateSubscriptionRequest
    {
        [JsonProperty("priceId")]
        public string PriceId { get; set; }
    }
}
