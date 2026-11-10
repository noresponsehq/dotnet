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
    /// What one implementation cost.
    /// </summary>
    public class ScreeningSpendLine
    {
        /// <summary>
        /// The resolver, collector or analyser that produced a record, by its catalogue name.
        /// </summary>
        [JsonProperty("implementation", Required = Newtonsoft.Json.Required.Always)]
        public string Implementation { get; set; } = default!;

        [JsonProperty("role", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningSpendLineRole Role { get; set; } = default!;

        [JsonProperty("spend_minor", Required = Newtonsoft.Json.Required.Always)]
        public long SpendMinor { get; set; } = default!;
    }
}