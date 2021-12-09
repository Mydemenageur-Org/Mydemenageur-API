using Newtonsoft.Json;

namespace Mydemenageur.DAL.Models.Stripe
{
    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
