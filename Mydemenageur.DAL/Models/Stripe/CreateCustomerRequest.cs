using Newtonsoft.Json;

public class CreateCustomerRequest
{
    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }
}