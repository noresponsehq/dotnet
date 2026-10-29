#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Counts of planned source-identifier pairs by evidence outcome, deduplicated across signals and incomplete entries. These counts do not measure identity agreement or prove that processing is still running. A lane that was not searched is not an answered source.
    /// </summary>
    public class RiskCoverage
    {
        /// <summary>
        /// Source-identifier pairs that gave a definite found or not_found answer.
        /// </summary>
        [JsonProperty("answered", Required = Newtonsoft.Json.Required.Always)]
        public long Answered { get; set; } = default!;

        /// <summary>
        /// Source-identifier pairs that could not answer, including unsupported coverage markers and source failures. Read detailed signals to distinguish Not covered from Failed.
        /// </summary>
        [JsonProperty("failed", Required = Newtonsoft.Json.Required.Always)]
        public long Failed { get; set; } = default!;

        /// <summary>
        /// Source-identifier pairs with deferred or timed_out evidence. A terminal timeout can remain here after processing finishes; use assessment status and continuation metadata to determine whether work is still running.
        /// </summary>
        [JsonProperty("pending", Required = Newtonsoft.Json.Required.Always)]
        public long Pending { get; set; } = default!;
    }
}