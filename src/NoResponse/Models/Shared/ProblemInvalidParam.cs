#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class ProblemInvalidParam
    {
        [JsonProperty("field", Required = Newtonsoft.Json.Required.Always)]
        public string Field { get; set; } = default!;

        [JsonProperty("reason", Required = Newtonsoft.Json.Required.Always)]
        public string Reason { get; set; } = default!;

        [JsonProperty("example", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Example { get; set; }
    }
}