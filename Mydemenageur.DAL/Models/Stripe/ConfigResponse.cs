using Newtonsoft.Json;


namespace Mydemenageur.DAL.Models.Stripe
{
    public class ConfigResponse
    {
        [JsonProperty("publishableKey")]
        public string PublishableKey { get; set; }
    }
}
