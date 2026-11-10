#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// What screening cost, in US cents, in total and per implementation.
    /// </summary>
    public class ScreeningSpend
    {
        /// <summary>
        /// The total, in US cents.
        /// </summary>
        [JsonProperty("total_minor", Required = Newtonsoft.Json.Required.Always)]
        public long TotalMinor { get; set; } = default!;

        [JsonProperty("by_implementation", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningSpendLine> ByImplementation { get; set; } = default!;
    }
}