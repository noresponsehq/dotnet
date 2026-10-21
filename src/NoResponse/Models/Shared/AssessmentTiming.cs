#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class AssessmentTiming
    {
        /// <summary>
        /// The budget the server applied to the synchronous answer.
        /// </summary>
        [JsonProperty("deadline_ms", Required = Newtonsoft.Json.Required.Always)]
        public long DeadlineMs { get; set; } = default!;

        /// <summary>
        /// Server time from HTTP ingress to this revision of the assessment.
        /// </summary>
        [JsonProperty("elapsed_ms", Required = Newtonsoft.Json.Required.Always)]
        public long ElapsedMs { get; set; } = default!;
    }
}