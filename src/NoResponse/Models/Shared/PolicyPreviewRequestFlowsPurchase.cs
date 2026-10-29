#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class PolicyPreviewRequestFlowsPurchase
    {
        [JsonProperty("step_up", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? StepUp { get; set; }

        [JsonProperty("review", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Review { get; set; }

        [JsonProperty("block", Required = Newtonsoft.Json.Required.Default, NullValueHandling = NullValueHandling.Include)]
        public OptionalNullable<long?> Block { get; set; }
        public bool ShouldSerializeBlock() => Block.IsSet;

        /// <summary>
        /// Send true; false is refused.
        /// </summary>
        [JsonProperty("inherits_account_creation", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? InheritsAccountCreation { get; set; }
    }
}