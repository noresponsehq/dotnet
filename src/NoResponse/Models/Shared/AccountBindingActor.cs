#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class AccountBindingActor
    {
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public string Type { get; } = "api_key";

        [JsonProperty("key_id", Required = Newtonsoft.Json.Required.Always)]
        public string KeyId { get; set; } = default!;
    }
}