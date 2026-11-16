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

    public class WrittenProfile
    {
        /// <summary>
        /// queued: the profile is being written. written: summary, statements and leads hold it. failed: it could not be written; error_code says why and no content is returned.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public WrittenProfileStatus Status { get; set; } = default!;

        /// <summary>
        /// The investigation whose evidence the profile is written from.
        /// </summary>
        [JsonProperty("investigation_id", Required = Newtonsoft.Json.Required.Always)]
        public string InvestigationId { get; set; } = default!;

        /// <summary>
        /// Present only when status is failed. no_writer: no profile writer serves this environment. refused: the model refused. not_written: the answer was not a profile, or no statement survived grounding. uncited_statement: a statement or lead cited a record the person does not hold. not_answered: the writer did not answer. worker_attempts_exhausted: the worker stopped retrying.
        /// </summary>
        [JsonProperty("error_code", Required = Newtonsoft.Json.Required.DisallowNull)]
        public WrittenProfileErrorCode? ErrorCode { get; set; }

        [JsonProperty("queued_at", Required = Newtonsoft.Json.Required.Always)]
        public string QueuedAt { get; set; } = default!;

        [JsonProperty("finished_at", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string? FinishedAt { get; set; }

        /// <summary>
        /// Present when written: a few plain sentences with the most useful picture first.
        /// </summary>
        [JsonProperty("summary", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Summary { get; set; }

        /// <summary>
        /// Each fact or inference with its certainty and the evidence it cites. Statements that cited evidence the writer was not sent are dropped before storage.
        /// </summary>
        [JsonProperty("statements", Required = Newtonsoft.Json.Required.Always)]
        public List<ProfileStatement> Statements { get; set; } = default!;

        /// <summary>
        /// Values worth searching next, each held by a linked record.
        /// </summary>
        [JsonProperty("leads", Required = Newtonsoft.Json.Required.Always)]
        public List<ProfileLead> Leads { get; set; } = default!;
    }
}