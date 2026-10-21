#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class RiskDetailsBreachName
    {
        [JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        public string Value { get; set; } = default!;

        [JsonProperty("seen_in", Required = Newtonsoft.Json.Required.Always)]
        public long SeenIn { get; set; } = default!;
    }
}