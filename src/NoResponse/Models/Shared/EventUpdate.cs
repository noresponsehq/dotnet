#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Changes the fields it names; at least one besides expected_version.
    /// </summary>
    public class EventUpdate
    {
        /// <summary>
        /// The event's name.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Name { get; set; }

        /// <summary>
        /// When the event starts.
        /// </summary>
        [JsonProperty("starts_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? StartsAt { get; set; }

        /// <summary>
        /// When the event ends, at or after its start; the start when omitted.
        /// </summary>
        [JsonProperty("ends_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? EndsAt { get; set; }

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
        /// The people the event protects, at most 20.
        /// </summary>
        [JsonProperty("principals", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<Principal>? Principals { get; set; }

        /// <summary>
        /// Sensitive topics the screening reads posts against, at most 50.
        /// </summary>
        [JsonProperty("topics", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Topics { get; set; }

        /// <summary>
        /// When present, the change applies only if the event is still at this version (409 event_conflict otherwise).
        /// </summary>
        [JsonProperty("expected_version", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ExpectedVersion { get; set; }
    }
}