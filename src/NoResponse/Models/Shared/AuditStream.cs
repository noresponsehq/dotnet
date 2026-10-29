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

    public class AuditStream
    {
        [JsonProperty("endpoint_id", Required = Newtonsoft.Json.Required.Always)]
        public string EndpointId { get; set; } = default!;

        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public Models.Shared.Environment Environment { get; set; } = default!;

        [JsonProperty("actions", Required = Newtonsoft.Json.Required.Always)]
        public List<string> Actions { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        /// <summary>
        /// unverified until the endpoint echoes an endpoint.verification challenge (send one with POST /v1/webhooks/{endpoint_id}/test); verified receives assessment.updated events; disabled after 72 hours of continuous delivery failure, and verified again by the same challenge.
        /// </summary>
        [JsonProperty("endpoint_state", Required = Newtonsoft.Json.Required.Always)]
        public WebhookEndpointState EndpointState { get; set; } = default!;

        [JsonProperty("last_attempt_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? LastAttemptAt { get; set; }

        [JsonProperty("last_delivered_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? LastDeliveredAt { get; set; }
    }
}