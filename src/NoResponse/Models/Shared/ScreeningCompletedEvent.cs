#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// The screening.completed event.
    /// </summary>
    public class ScreeningCompletedEvent
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
        /// Always screening.completed.
        /// </summary>
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public string Type { get; } = "screening.completed";

        /// <summary>
        /// Present, and true, only on an event sent by POST /v1/webhooks/{endpoint_id}/test: its data is a fixed example assessment, not one of the workspace's, so a receiver acknowledges it and applies nothing. Real events have no test member.
        /// </summary>
        [JsonProperty("test", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Test { get; set; }

        /// <summary>
        /// A screening of an event finished. It counts only the attendees this screening set out to reach, not who they are: read the roster with GET /v1/workspaces/{workspace_id}/events/{environment}/{event_id}/attendees, filtered by band, to act on them. attendees_screened + partial_coverage = attendees_total, attendees_flagged is the sum of by_band, no_footprint counts screened attendees with no flag whose accounts were not found, and no_profile_to_check those the screening had nothing to look up for on any requested platform. A cancelled screening sends no event.
        /// </summary>
        [JsonProperty("data", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningCompleted Data { get; set; } = new();
    }
}