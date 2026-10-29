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
    /// Acknowledged propagation of a retained erasure record in the calling environment. Pending means the target has not acknowledged completion. Completion covers these serving-system targets only; backup and point-in-time recovery copies expire according to their own retention policy. This receipt contains no subject id or digest. Workspace teardown revokes customer credentials, so obtain required receipts before teardown.
    /// </summary>
    public class ErasureReceipt
    {
        /// <summary>
        /// Opaque, server-issued erasure receipt id. It contains no subject identifier.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public ErasureReceiptKind Kind { get; set; } = default!;

        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public ListChangeState Status { get; set; } = default!;

        [JsonProperty("assessments_deleted", Required = Newtonsoft.Json.Required.Always)]
        public long AssessmentsDeleted { get; set; } = default!;

        [JsonProperty("requested_at", Required = Newtonsoft.Json.Required.Always)]
        public string RequestedAt { get; set; } = default!;

        [JsonProperty("completed_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? CompletedAt { get; set; }

        [JsonProperty("targets", Required = Newtonsoft.Json.Required.Always)]
        public List<ErasureReceiptTarget> Targets { get; set; } = default!;

        [JsonProperty("backup_policy", Required = Newtonsoft.Json.Required.Always)]
        public string BackupPolicy { get; set; } = default!;
    }
}