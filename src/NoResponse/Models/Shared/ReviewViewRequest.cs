#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class ReviewViewRequest
    {
        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public string Name { get; set; } = default!;

        [JsonProperty("shared", Required = Newtonsoft.Json.Required.Always)]
        public bool Shared { get; set; } = default!;

        [JsonProperty("expected_version", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ExpectedVersion { get; set; }

        [JsonProperty("filter", Required = Newtonsoft.Json.Required.Always)]
        public Filter Filter { get; set; } = new();
    }
}