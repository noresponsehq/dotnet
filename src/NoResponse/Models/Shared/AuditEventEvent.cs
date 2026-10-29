#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// The audit.event event.
    /// </summary>
    public class AuditEventEvent
    {
        /// <summary>
        /// The event id, equal to the NR-Webhook-Id header and across redeliveries and every endpoint the event fans out to.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// When the event was created. Equal across redeliveries; the signing timestamp header is per delivery.
        /// </summary>
        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        /// <summary>
        /// Always audit.event.
        /// </summary>
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public string Type { get; } = "audit.event";

        /// <summary>
        /// Present, and true, only on an event sent by POST /v1/webhooks/{endpoint_id}/test: its data is a fixed example assessment, not one of the workspace's, so a receiver acknowledges it and applies nothing. Real events have no test member.
        /// </summary>
        [JsonProperty("test", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Test { get; set; }

        /// <summary>
        /// One privileged workspace action with its actor, target, outcome and time. Successful data changes commit their audit evidence atomically. Sensitive exports record access before returning bytes. Details exclude secrets and subject identifiers.
        /// </summary>
        [JsonProperty("data", Required = Newtonsoft.Json.Required.Always)]
        public AuditEvent Data { get; set; } = new();
    }
}