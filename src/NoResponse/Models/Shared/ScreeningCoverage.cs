#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

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
        /// Items read that the analyser could not analyse. Above zero, the attendee is partial: a finding may be missing from them.
        /// </summary>
        [JsonProperty("items_not_analysed", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ItemsNotAnalysed { get; set; }

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
        /// Requested platforms where the attendee gave the screening nothing to look up, such as no handle for a platform that reads supplied handles only. Nothing looked and nothing failed: such a platform is not in platforms, is not none_found and does not make the attendee partial.
        /// </summary>
        [JsonProperty("not_searched", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<HandlePlatform>? NotSearched { get; set; }
    }
}