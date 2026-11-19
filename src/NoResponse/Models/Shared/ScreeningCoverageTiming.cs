#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// How long this search's latest attempt took, stage by stage. started_at minus the investigation's creation is the time spent queued or retrying.
    /// </summary>
    public class ScreeningCoverageTiming
    {
        /// <summary>
        /// When the attempt that wrote this result started.
        /// </summary>
        [JsonProperty("started_at", Required = Newtonsoft.Json.Required.Always)]
        public string StartedAt { get; set; } = default!;

        /// <summary>
        /// Milliseconds from started_at to the end of the last stage listed.
        /// </summary>
        [JsonProperty("total_ms", Required = Newtonsoft.Json.Required.Always)]
        public long TotalMs { get; set; } = default!;

        /// <summary>
        /// Stages finished so far, in order. The result first written lists stages up to written; the settled result lists them all.
        /// </summary>
        [JsonProperty("stages", Required = Newtonsoft.Json.Required.Always)]
        public List<StageTime> Stages { get; set; } = default!;
    }
}