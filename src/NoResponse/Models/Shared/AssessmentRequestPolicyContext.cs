#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Optional policy inputs: region.
    /// </summary>
    public class AssessmentRequestPolicyContext
    {
        /// <summary>
        /// ISO 3166-1 alpha-2 region of the user, as the provider determines it.
        /// </summary>
        [JsonProperty("region", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Region { get; set; }
    }
}