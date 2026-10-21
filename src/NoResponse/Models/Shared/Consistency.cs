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
    /// Cross-identifier agreement, computed from facts only. Every member is optional; absent makes no claim.
    /// </summary>
    public class Consistency
    {
        /// <summary>
        /// How a submitted identifier agrees with what the evidence holds for the other identifiers. unknown is a definite 'cannot tell' (for example a truncated or too-common breach result); absent means the comparison was not made.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ConsistencyValue? Name { get; set; }

        /// <summary>
        /// How a submitted identifier agrees with what the evidence holds for the other identifiers. unknown is a definite 'cannot tell' (for example a truncated or too-common breach result); absent means the comparison was not made.
        /// </summary>
        [JsonProperty("phone", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ConsistencyValue? Phone { get; set; }

        /// <summary>
        /// How a submitted identifier agrees with what the evidence holds for the other identifiers. unknown is a definite 'cannot tell' (for example a truncated or too-common breach result); absent means the comparison was not made.
        /// </summary>
        [JsonProperty("ip", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ConsistencyValue? Ip { get; set; }

        /// <summary>
        /// IP country, phone country, breach countries and policy_context.region together.
        /// </summary>
        [JsonProperty("country", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Country? Country { get; set; }
    }
}