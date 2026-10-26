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
    /// How far independent sources agree on one attribute: values is the number of distinct values reported, support the tables and profiles reporting any, and agreement match (one value), partial (the most reported value holds more than half the support) or mismatch. The values themselves are not served.
    /// </summary>
    public class AttributeAgreement
    {
        [JsonProperty("agreement", Required = Newtonsoft.Json.Required.Always)]
        public Agreement Agreement { get; set; } = default!;

        [JsonProperty("values", Required = Newtonsoft.Json.Required.Always)]
        public long Values { get; set; } = default!;

        [JsonProperty("support", Required = Newtonsoft.Json.Required.Always)]
        public long Support { get; set; } = default!;
    }
}