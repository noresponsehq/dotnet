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
    /// Facts about the submitted IP from the offline database and this workspace's own recent assessments. Every member is optional; absent makes no claim.
    /// </summary>
    public class IpSummary
    {
        /// <summary>
        /// ISO 3166-1 alpha-2 country of the address.
        /// </summary>
        [JsonProperty("country", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Country { get; set; }

        /// <summary>
        /// The address's network type.
        /// </summary>
        [JsonProperty("type", Required = Newtonsoft.Json.Required.DisallowNull)]
        public IpSummaryType? Type { get; set; }

        /// <summary>
        /// Assessments in this workspace with the same IP in the last 24 hours, this one included. A count only.
        /// </summary>
        [JsonProperty("seen_in_workspace", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? SeenInWorkspace { get; set; }
    }
}