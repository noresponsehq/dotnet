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

    public class Investigation
    {
        /// <summary>
        /// The denominator of a screening: accounts found, how many could be read, items read and the period they span, so a clean result is distinct from a person nobody could see.
        /// </summary>
        [JsonProperty("coverage", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningCoverage Coverage { get; set; } = new();

        /// <summary>
        /// What screening cost, in US cents, in total and per implementation.
        /// </summary>
        [JsonProperty("spend", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningSpend Spend { get; set; } = new();

        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("person_id", Required = Newtonsoft.Json.Required.Always)]
        public string PersonId { get; set; } = default!;

        [JsonProperty("group_id", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string? GroupId { get; set; }

        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public InvestigationStatus Status { get; set; } = default!;

        [JsonProperty("depth", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningDepth Depth { get; set; } = default!;

        [JsonProperty("platforms", Required = Newtonsoft.Json.Required.Always)]
        public List<string> Platforms { get; set; } = default!;

        [JsonProperty("match_state", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public MatchState? MatchState { get; set; }

        [JsonProperty("error_code", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ErrorCode { get; set; }

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("started_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? StartedAt { get; set; }

        [JsonProperty("finished_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? FinishedAt { get; set; }

        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.Always)]
        public string ExpiresAt { get; set; } = default!;

        [JsonProperty("deadline_at", Required = Newtonsoft.Json.Required.Always)]
        public string DeadlineAt { get; set; } = default!;
    }
}