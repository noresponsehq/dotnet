#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class Thresholds
    {
        [JsonProperty("step_up", Required = Newtonsoft.Json.Required.Always)]
        public long StepUp { get; set; } = default!;

        [JsonProperty("review", Required = Newtonsoft.Json.Required.Always)]
        public long Review { get; set; } = default!;

        [JsonProperty("block", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public long? Block { get; set; }
    }
}