#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class PolicyPreviewRequestFlowsAccountCreation
    {
        [JsonProperty("step_up", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? StepUp { get; set; }

        [JsonProperty("review", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Review { get; set; }

        [JsonProperty("block", Required = Newtonsoft.Json.Required.Default, NullValueHandling = NullValueHandling.Include)]
        public OptionalNullable<long?> Block { get; set; }
        public bool ShouldSerializeBlock() => Block.IsSet;
    }
}