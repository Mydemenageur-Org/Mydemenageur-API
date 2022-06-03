using Newtonsoft.Json;

namespace Mydemenageur.DAL.Models.Stripe
{
    public class CreateCheckoutSessionRequest
    {
        [JsonProperty("priceId")]
        public string PriceId { get; set; }

        [JsonProperty("stripeId")]
        public string StripeId { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }
    }
}
