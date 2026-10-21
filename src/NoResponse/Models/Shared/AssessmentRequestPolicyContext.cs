#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class AssessmentRequestPolicyContext
    {
        /// <summary>
        /// The provider flow the assessment gates. Absent means signup.
        /// </summary>
        [JsonProperty("flow", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Flow? Flow { get; set; }

        /// <summary>
        /// ISO 3166-1 alpha-2 region of the signup, as the provider determines it.
        /// </summary>
        [JsonProperty("region", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Region { get; set; }
    }
}