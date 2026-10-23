#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// The 200 answer of GET /v1/webhooks/{endpoint_id}/deliveries: one page of the endpoint's delivery attempts, newest first. Each item is one POST to the endpoint. Attempts are kept for 30 days, or the workspace's retention_days when that is shorter, and are deleted with the assessment they carried. next_cursor is present only when another page exists.
    /// </summary>
    public class WebhookDeliveryList
    {
        /// <summary>
        /// Opaque id of the endpoint registration.
        /// </summary>
        [JsonProperty("endpoint_id", Required = Newtonsoft.Json.Required.Always)]
        public string EndpointId { get; set; } = default!;

        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<WebhookDeliveryAttempt> Items { get; set; } = default!;

        /// <summary>
        /// Opaque cursor for the next page, MAC'd by the server and bound to the workspace and endpoint it was issued for. Pass it back unchanged as cursor; a tampered, foreign or expired cursor answers 400 invalid_cursor.
        /// </summary>
        [JsonProperty("next_cursor", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? NextCursor { get; set; }
    }
}