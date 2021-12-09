using Newtonsoft.Json;
namespace Mydemenageur.DAL.Models.Stripe
{
    public class CreatePaymentIntentResponse
    {
        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }
    }
}
