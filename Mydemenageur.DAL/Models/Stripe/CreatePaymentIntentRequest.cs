using Newtonsoft.Json;

namespace Mydemenageur.DAL.Models.Stripe
{
    public class CreatePaymentIntentRequest
    {
        [JsonProperty("paymentMethodType")]
        public string PaymentMethodType { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }
}
