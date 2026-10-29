#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class Cost
    {
        [JsonProperty("allow", Required = Newtonsoft.Json.Required.Always)]
        public double Allow { get; set; } = default!;

        [JsonProperty("step_up", Required = Newtonsoft.Json.Required.Always)]
        public double StepUp { get; set; } = default!;

        [JsonProperty("review", Required = Newtonsoft.Json.Required.Always)]
        public double Review { get; set; } = default!;

        [JsonProperty("block", Required = Newtonsoft.Json.Required.Always)]
        public double Block { get; set; } = default!;
    }
}