#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class AuditStreamReplayRequest
    {
        /// <summary>
        /// Opaque, workspace-scoped webhook event id. A redelivery of the same event carries the same event_id, so consumers deduplicate on it.
        /// </summary>
        [JsonProperty("event_id", Required = Newtonsoft.Json.Required.Always)]
        public string EventId { get; set; } = default!;
    }
}