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
    /// Breach facts for one selector. found is the one required member: false is a definite 'in no breach table'. Every other member is optional, and absent makes no claim. No member carries a breach value, password or hash, and unknown members are refused. truncated means the value matched more tables or records than the lookup keeps, so absence-based claims (a missing name match, for example) are withheld; a truncated result is always found.
    /// </summary>
    public class BreachSummary
    {
        [JsonProperty("found", Required = Newtonsoft.Json.Required.Always)]
        public bool Found { get; set; } = default!;

        /// <summary>
        /// Distinct breach tables the value appears in.
        /// </summary>
        [JsonProperty("breach_count", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? BreachCount { get; set; }

        /// <summary>
        /// Year of the oldest dated table.
        /// </summary>
        [JsonProperty("first_year", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? FirstYear { get; set; }

        /// <summary>
        /// Year of the newest dated table.
        /// </summary>
        [JsonProperty("last_year", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? LastYear { get; set; }

        /// <summary>
        /// The union of data kinds the matching tables held, without the values.
        /// </summary>
        [JsonProperty("exposed", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<BreachExposed>? Exposed { get; set; }

        /// <summary>
        /// The value is in a credential_list or stealer_log table.
        /// </summary>
        [JsonProperty("in_credential_lists", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? InCredentialLists { get; set; }

        /// <summary>
        /// A plaintext password sat next to the value in a table dated two years ago or less, or the value is in a stealer_log table.
        /// </summary>
        [JsonProperty("recent_password_exposure", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? RecentPasswordExposure { get; set; }

        [JsonProperty("truncated", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Truncated { get; set; }
    }
}