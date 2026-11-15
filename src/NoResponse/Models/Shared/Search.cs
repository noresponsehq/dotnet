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
    /// One search a person started on a value of one of a person's records. Nothing searches again by itself.
    /// </summary>
    public class Search
    {
        [JsonProperty("search_id", Required = Newtonsoft.Json.Required.Always)]
        public string SearchId { get; set; } = default!;

        /// <summary>
        /// The record whose value led to the search.
        /// </summary>
        [JsonProperty("record_id", Required = Newtonsoft.Json.Required.Always)]
        public string RecordId { get; set; } = default!;

        /// <summary>
        /// The field of that record that holds the value.
        /// </summary>
        [JsonProperty("field", Required = Newtonsoft.Json.Required.Always)]
        public string Field { get; set; } = default!;

        /// <summary>
        /// The one breach table searched, when the search was of one table.
        /// </summary>
        [JsonProperty("table", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Table { get; set; }

        /// <summary>
        /// The kind of the value searched. A domain search finds the leaked rows of every email address at the domain.
        /// </summary>
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public SearchKind Kind { get; set; } = default!;

        /// <summary>
        /// The searched value. Empty in person reports when evidence.read is denied.
        /// </summary>
        [JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        public string Value { get; set; } = default!;

        /// <summary>
        /// A password or hash search is counting, then counted: rows says how many breach rows hold the value, and nothing runs until a person runs it (POST .../searches/{search_id}/run). Any other search is queued, then running. completed: every source it asked answered. partial: some did not. failed: none answered or none could be asked, which says nothing about whether the value is held anywhere.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public SearchStatus Status { get; set; } = default!;

        /// <summary>
        /// How many breach rows hold the password or hash, counted before the search runs.
        /// </summary>
        [JsonProperty("rows", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Rows { get; set; }

        /// <summary>
        /// How many sources the search asked: the breach search and the account and phone lookups that take the value's kind.
        /// </summary>
        [JsonProperty("sources", Required = Newtonsoft.Json.Required.Always)]
        public long Sources { get; set; } = default!;

        /// <summary>
        /// How many of them gave a definite answer, found or not found.
        /// </summary>
        [JsonProperty("answered", Required = Newtonsoft.Json.Required.Always)]
        public long Answered { get; set; } = default!;

        /// <summary>
        /// How many records the search found. They are among the person report's lookup records, each match naming this search.
        /// </summary>
        [JsonProperty("found", Required = Newtonsoft.Json.Required.Always)]
        public long Found { get; set; } = default!;

        /// <summary>
        /// What the search may spend, in US cents, held against the environment's daily screening cap while it runs.
        /// </summary>
        [JsonProperty("estimated_spend", Required = Newtonsoft.Json.Required.Always)]
        public long EstimatedSpend { get; set; } = default!;

        /// <summary>
        /// What the search spent, in US cents, metered as screening work.
        /// </summary>
        [JsonProperty("spent", Required = Newtonsoft.Json.Required.Always)]
        public long Spent { get; set; } = default!;

        /// <summary>
        /// Why a failed search found nothing: its sources did not answer, none could be asked, or it could not run before its attempts were spent.
        /// </summary>
        [JsonProperty("error_code", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Who started the search.
        /// </summary>
        [JsonProperty("requested_by", Required = Newtonsoft.Json.Required.Always)]
        public string RequestedBy { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("finished_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? FinishedAt { get; set; }
    }
}