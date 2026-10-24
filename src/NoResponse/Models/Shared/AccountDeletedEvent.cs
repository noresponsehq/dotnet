#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// The account.deleted event.
    /// </summary>
    public class AccountDeletedEvent
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
        /// Always account.deleted.
        /// </summary>
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public string Type { get; } = "account.deleted";

        /// <summary>
        /// Present, and true, only on an event sent by POST /v1/webhooks/{endpoint_id}/test: its data is a fixed example assessment, not one of the workspace's, so a receiver acknowledges it and applies nothing. Real events have no test member.
        /// </summary>
        [JsonProperty("test", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Test { get; set; }

        /// <summary>
        /// Terminal for the account's assessments created at or before deleted_at in the receiving endpoint's environment: delete what you hold from them and ignore later-arriving updates of them. deleted_at is no earlier than the creation of any assessment it erased. An assessment created after it belongs to the account assessed again: keep it, even when this event arrives after it.
        /// </summary>
        [JsonProperty("data", Required = Newtonsoft.Json.Required.Always)]
        public AccountDeleted Data { get; set; } = new();
    }
}