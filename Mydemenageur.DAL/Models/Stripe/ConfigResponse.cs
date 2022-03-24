using Newtonsoft.Json;
using Stripe;
using System.Collections.Generic;

namespace Mydemenageur.DAL.Models.Stripe
{
    public class ConfigResponse
    {
        [JsonProperty("publishableKey")]
        public string PublishableKey { get; set; }
        public List<Price> Prices { get; set; }
    }
}
