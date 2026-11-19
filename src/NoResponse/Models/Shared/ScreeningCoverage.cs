#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// The denominator of a screening: accounts found, how many could be read, items read and the period they span, so a clean result is distinct from a person nobody could see.
    /// </summary>
    public class ScreeningCoverage
    {
        [JsonProperty("accounts_found", Required = Newtonsoft.Json.Required.Always)]
        public long AccountsFound { get; set; } = default!;

        [JsonProperty("accounts_accessible", Required = Newtonsoft.Json.Required.Always)]
        public long AccountsAccessible { get; set; } = default!;

        [JsonProperty("items_read", Required = Newtonsoft.Json.Required.Always)]
        public long ItemsRead { get; set; } = default!;

        /// <summary>
        /// Items read that the analyser could not analyse. Above zero, the person is partial: a finding may be missing from them.
        /// </summary>
        [JsonProperty("items_not_analysed", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ItemsNotAnalysed { get; set; }

        /// <summary>
        /// items_not_analysed split by reason, each reason once with at least one item.
        /// </summary>
        [JsonProperty("not_analysed_by_reason", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<ScreeningNotAnalysedCount>? NotAnalysedByReason { get; set; }

        /// <summary>
        /// Videos the analyser read from their cover frame and caption alone, without their sound or later frames. They count as analysed and do not make the person partial. Left out when none.
        /// </summary>
        [JsonProperty("items_frame_only", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ItemsFrameOnly { get; set; }

        /// <summary>
        /// The oldest item read on any platform.
        /// </summary>
        [JsonProperty("period_from", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? PeriodFrom { get; set; }

        /// <summary>
        /// The newest item read on any platform.
        /// </summary>
        [JsonProperty("period_to", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? PeriodTo { get; set; }

        [JsonProperty("platforms", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningPlatformCoverage> Platforms { get; set; } = default!;

        /// <summary>
        /// Requested platforms where the person gave the screening nothing to look up, such as no handle for a platform that reads supplied handles only. Nothing looked and nothing failed: such a platform is not in platforms, is not none_found and does not make the person partial.
        /// </summary>
        [JsonProperty("not_searched", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? NotSearched { get; set; }

        /// <summary>
        /// How many sources this search asked and how many answered, across the lookup stage (sources asked whether an account exists for an identifier) and the platform stage (profiles read on each platform). A source that answered may have found nothing; one that did not answer failed or ran out of time.
        /// </summary>
        [JsonProperty("sources", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ScreeningCoverageSources? Sources { get; set; }

        /// <summary>
        /// How long this search's latest attempt took, stage by stage. started_at minus the investigation's creation is the time spent queued or retrying.
        /// </summary>
        [JsonProperty("timing", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ScreeningCoverageTiming? Timing { get; set; }
    }
}