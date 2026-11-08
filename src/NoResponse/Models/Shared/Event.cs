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
    /// An event and its screening context.
    /// </summary>
    public class Event
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public AuditSettingsEnvironment Environment { get; set; } = default!;

        /// <summary>
        /// The event's name.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// When the event starts.
        /// </summary>
        [JsonProperty("starts_at", Required = Newtonsoft.Json.Required.Always)]
        public string StartsAt { get; set; } = default!;

        /// <summary>
        /// When the event ends, at or after its start; the start when omitted.
        /// </summary>
        [JsonProperty("ends_at", Required = Newtonsoft.Json.Required.Always)]
        public string EndsAt { get; set; } = default!;

        /// <summary>
        /// Where the event is held.
        /// </summary>
        [JsonProperty("venue", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Venue { get; set; }

        /// <summary>
        /// The organisation hosting the event.
        /// </summary>
        [JsonProperty("host", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Host { get; set; }

        /// <summary>
        /// The people the event protects.
        /// </summary>
        [JsonProperty("principals", Required = Newtonsoft.Json.Required.Always)]
        public List<Principal> Principals { get; set; } = default!;

        /// <summary>
        /// Sensitive topics.
        /// </summary>
        [JsonProperty("topics", Required = Newtonsoft.Json.Required.Always)]
        public List<string> Topics { get; set; } = default!;

        /// <summary>
        /// How many attendees the event holds.
        /// </summary>
        [JsonProperty("attendees", Required = Newtonsoft.Json.Required.Always)]
        public long Attendees { get; set; } = default!;

        /// <summary>
        /// Goes up by one with every change.
        /// </summary>
        [JsonProperty("version", Required = Newtonsoft.Json.Required.Always)]
        public long Version { get; set; } = default!;

        /// <summary>
        /// When raw collected items and unmatched candidate profiles are purged: 30 days after the event ends.
        /// </summary>
        [JsonProperty("purge_after", Required = Newtonsoft.Json.Required.Always)]
        public string PurgeAfter { get; set; } = default!;

        /// <summary>
        /// The member or key label that created the event.
        /// </summary>
        [JsonProperty("created_by", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedBy { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("updated_at", Required = Newtonsoft.Json.Required.Always)]
        public string UpdatedAt { get; set; } = default!;
    }
}