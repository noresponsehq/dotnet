#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class SourceCount
    {
        [JsonProperty("asked", Required = Newtonsoft.Json.Required.Always)]
        public long Asked { get; set; } = default!;

        [JsonProperty("answered", Required = Newtonsoft.Json.Required.Always)]
        public long Answered { get; set; } = default!;
    }
}