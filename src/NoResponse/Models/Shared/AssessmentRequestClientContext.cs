#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// Optional context observed by the client of the user. The server parses these hints into coarse features, leaves raw user_agent and opaque ids out of every answer, and weighs a mismatch with the other evidence.
    /// </summary>
    public class AssessmentRequestClientContext
    {
        /// <summary>
        /// IANA time-zone name such as Europe/London or America/Los_Angeles. Fixed numeric offsets are not accepted.
        /// </summary>
        [JsonProperty("timezone", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Timezone { get; set; }

        /// <summary>
        /// BCP 47 locale, for example en-AU. Treat as a presentation hint, not a nationality signal.
        /// </summary>
        [JsonProperty("locale", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Locale { get; set; }

        /// <summary>
        /// Client platform. Closed vocabulary keeps integrations stable.
        /// </summary>
        [JsonProperty("platform", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Platform? Platform { get; set; }

        /// <summary>
        /// Customer application version, without build metadata that contains secrets.
        /// </summary>
        [JsonProperty("app_version", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? AppVersion { get; set; }

        /// <summary>
        /// The raw User-Agent header of the request that the user sent, at most 512 characters. It is parsed into coarse features and left out of every answer.
        /// </summary>
        [JsonProperty("user_agent", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? UserAgent { get; set; }

        /// <summary>
        /// Workspace-scoped opaque identifier your application mints and rotates when the account or device changes. Send an id of your own, not a hardware fingerprint.
        /// </summary>
        [JsonProperty("device_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? DeviceId { get; set; }

        /// <summary>
        /// Opaque signup session identifier. It is used for workspace-scoped velocity and replay checks only.
        /// </summary>
        [JsonProperty("session_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? SessionId { get; set; }

        /// <summary>
        /// Elapsed time from form start to submit. Values below three seconds are a weak automation hint, weighed with the other evidence.
        /// </summary>
        [JsonProperty("form_duration_ms", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? FormDurationMs { get; set; }

        /// <summary>
        /// RFC 3339 timestamp observed by the client. Server ingress time remains authoritative; a large clock skew is only a low-confidence feature.
        /// </summary>
        [JsonProperty("client_observed_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ClientObservedAt { get; set; }
    }
}