#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// One authenticated HTTP attempt. An attempt id is independent of the caller correlation id. No raw body, path parameter, credential or source payload is retained. write_complete means the server wrote/flushed its response, not proof the client processed it. Collection is bounded best effort; ii_signup_request_diagnostic_failures_total exposes gaps. Retained for seven days or the shorter environment retention.
    /// </summary>
    public class RequestDiagnostic
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("request_id", Required = Newtonsoft.Json.Required.Always)]
        public string RequestId { get; set; } = default!;

        [JsonProperty("route", Required = Newtonsoft.Json.Required.Always)]
        public string Route { get; set; } = default!;

        [JsonProperty("http_status", Required = Newtonsoft.Json.Required.Always)]
        public long HttpStatus { get; set; } = default!;

        [JsonProperty("duration_ms", Required = Newtonsoft.Json.Required.Always)]
        public long DurationMs { get; set; } = default!;

        [JsonProperty("started_at", Required = Newtonsoft.Json.Required.Always)]
        public string StartedAt { get; set; } = default!;

        [JsonProperty("completed_at", Required = Newtonsoft.Json.Required.Always)]
        public string CompletedAt { get; set; } = default!;

        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.Always)]
        public string ExpiresAt { get; set; } = default!;

        [JsonProperty("error_code", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ErrorCode { get; set; }

        [JsonProperty("idempotent_replayed", Required = Newtonsoft.Json.Required.Always)]
        public bool IdempotentReplayed { get; set; } = default!;

        [JsonProperty("write_complete", Required = Newtonsoft.Json.Required.Always)]
        public bool WriteComplete { get; set; } = default!;

        /// <summary>
        /// The decision prepared for one HTTP attempt. Its account field preserves the account decision returned for that attempt, even after subsequent changes.
        /// </summary>
        [JsonProperty("decision", Required = Newtonsoft.Json.Required.DisallowNull)]
        public DecisionReceipt? Decision { get; set; }

        /// <summary>
        /// Public authenticated API key identifier. Absent for member requests and older unattributed diagnostics.
        /// </summary>
        [JsonProperty("api_key_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ApiKeyId { get; set; }
    }
}