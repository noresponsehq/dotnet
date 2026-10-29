#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class KeyActor
    {
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public string Type { get; } = "api_key";

        /// <summary>
        /// The public id of the workspace API key that made the change.
        /// </summary>
        [JsonProperty("key_id", Required = Newtonsoft.Json.Required.Always)]
        public string KeyId { get; set; } = default!;
    }
}