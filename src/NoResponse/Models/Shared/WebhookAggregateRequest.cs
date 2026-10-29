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
    /// Count every retained matching attempt in [from,to). Capture is best effort; missing attempts cannot be inferred. UTC buckets may be partial at the edges. The window is at most 30 days.
    /// </summary>
    public class WebhookAggregateRequest
    {
        [JsonProperty("from", Required = Newtonsoft.Json.Required.Always)]
        public string From { get; set; } = default!;

        [JsonProperty("to", Required = Newtonsoft.Json.Required.Always)]
        public string To { get; set; } = default!;

        [JsonProperty("bucket", Required = Newtonsoft.Json.Required.DisallowNull)]
        public RequestAggregateRequestBucket? Bucket { get; set; }

        [JsonProperty("status", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Status { get; set; }

        /// <summary>
        /// Opaque id of the endpoint registration.
        /// </summary>
        [JsonProperty("endpoint_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? EndpointId { get; set; }
    }
}