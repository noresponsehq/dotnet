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
    }
}