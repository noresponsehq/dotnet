#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class RiskDetailsPhoneName
    {
        [JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        public string Value { get; set; } = default!;

        [JsonProperty("seen_by", Required = Newtonsoft.Json.Required.Always)]
        public long SeenBy { get; set; } = default!;
    }
}