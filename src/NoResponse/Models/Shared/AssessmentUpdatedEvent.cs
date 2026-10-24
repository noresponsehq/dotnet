#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// The assessment.updated event.
    /// </summary>
    public class AssessmentUpdatedEvent
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
        /// Always assessment.updated.
        /// </summary>
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public string Type { get; } = "assessment.updated";

        /// <summary>
        /// Present, and true, only on an event sent by POST /v1/webhooks/{endpoint_id}/test: its data is a fixed example assessment, not one of the workspace's, so a receiver acknowledges it and applies nothing. Real events have no test member.
        /// </summary>
        [JsonProperty("test", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Test { get; set; }

        /// <summary>
        /// The assessment, or the minimal notice that it expired.
        /// </summary>
        [JsonProperty("data", Required = Newtonsoft.Json.Required.Always, NullValueHandling = NullValueHandling.Include)]
        public AssessmentUpdatedData Data { get; set; } = default!;

        public Assessment? GetDataComplete()
        {
            return Data.Assessment;
        }

        public Assessment? GetDataPartial()
        {
            return Data.Assessment;
        }

        public Assessment? GetDataPending()
        {
            return Data.Assessment;
        }

        public AssessmentExpired? GetDataExpired()
        {
            return Data.AssessmentExpired;
        }
    }
}