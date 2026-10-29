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
    /// Request an asynchronous customer export.
    /// </summary>
    public class CustomerExport
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public CustomerExportRequestKind Kind { get; set; } = default!;

        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public Models.Shared.Environment Environment { get; set; } = default!;

        /// <summary>
        /// standard returns the consolidated risk response; detailed adds merged provider-neutral facts when the key has detailed scope. There is no raw mode.
        /// </summary>
        [JsonProperty("response_mode", Required = Newtonsoft.Json.Required.Always)]
        public ResponseMode ResponseMode { get; set; } = default!;

        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public CustomerExportStatus Status { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("snapshot_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? SnapshotAt { get; set; }

        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.Always)]
        public string ExpiresAt { get; set; } = default!;

        [JsonProperty("finished_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? FinishedAt { get; set; }

        [JsonProperty("records", Required = Newtonsoft.Json.Required.Always)]
        public long Records { get; set; } = default!;

        [JsonProperty("bytes", Required = Newtonsoft.Json.Required.Always)]
        public long Bytes { get; set; } = default!;

        [JsonProperty("chunks", Required = Newtonsoft.Json.Required.Always)]
        public long Chunks { get; set; } = default!;

        [JsonProperty("attempts", Required = Newtonsoft.Json.Required.Always)]
        public long Attempts { get; set; } = default!;

        [JsonProperty("generation", Required = Newtonsoft.Json.Required.Always)]
        public long Generation { get; set; } = default!;

        [JsonProperty("error_code", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ErrorCode { get; set; }

        [JsonProperty("request_sha256", Required = Newtonsoft.Json.Required.Always)]
        public string RequestSha256 { get; set; } = default!;
    }
}