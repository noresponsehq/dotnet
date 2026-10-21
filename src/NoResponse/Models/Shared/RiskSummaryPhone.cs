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

    public class RiskSummaryPhone
    {
        [JsonProperty("line_type", Required = Newtonsoft.Json.Required.DisallowNull)]
        public LineType? LineType { get; set; }

        [JsonProperty("country", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Country { get; set; }

        /// <summary>
        /// Highest report count any source gave for the number.
        /// </summary>
        [JsonProperty("report_count", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ReportCount { get; set; }

        /// <summary>
        /// Carrier-network metadata from privately weighted sources; not upstream provider attribution.
        /// </summary>
        [JsonProperty("carrier", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Carrier { get; set; }
    }
}