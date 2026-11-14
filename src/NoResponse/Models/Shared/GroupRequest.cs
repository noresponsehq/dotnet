#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Creates a group.
    /// </summary>
    public class GroupRequest
    {
        /// <summary>
        /// The group's name.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// When the group starts.
        /// </summary>
        [JsonProperty("starts_at", Required = Newtonsoft.Json.Required.Always)]
        public string StartsAt { get; set; } = default!;

        /// <summary>
        /// When the group ends, at or after its start; the start when omitted.
        /// </summary>
        [JsonProperty("ends_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? EndsAt { get; set; }

        /// <summary>
        /// Where the group is held.
        /// </summary>
        [JsonProperty("venue", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Venue { get; set; }

        /// <summary>
        /// The organisation hosting the group.
        /// </summary>
        [JsonProperty("host", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Host { get; set; }

        /// <summary>
        /// The people the group protects, at most 20.
        /// </summary>
        [JsonProperty("principals", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<Principal>? Principals { get; set; }

        /// <summary>
        /// Sensitive topics the screening reads posts against, at most 50.
        /// </summary>
        [JsonProperty("topics", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Topics { get; set; }
    }
}