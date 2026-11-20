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
    /// Creates an ongoing group or an event with optional dates.
    /// </summary>
    public class GroupRequest
    {
        /// <summary>
        /// The group's name.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// What the group's people share: an event, an organisation, a place or another attribute.
        /// </summary>
        [JsonProperty("type", Required = Newtonsoft.Json.Required.DisallowNull)]
        public GroupType? Type { get; set; }

        /// <summary>
        /// The attribute its people share, such as the employer's or the place's name.
        /// </summary>
        [JsonProperty("attribute", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Attribute { get; set; }

        /// <summary>
        /// Optional event start; null for an ongoing group.
        /// </summary>
        [JsonProperty("starts_at", Required = Newtonsoft.Json.Required.Default, NullValueHandling = NullValueHandling.Include)]
        public OptionalNullable<string?> StartsAt { get; set; }
        public bool ShouldSerializeStartsAt() => StartsAt.IsSet;

        /// <summary>
        /// Optional event end; requires starts_at and must be at or after it. Null when no end is set.
        /// </summary>
        [JsonProperty("ends_at", Required = Newtonsoft.Json.Required.Default, NullValueHandling = NullValueHandling.Include)]
        public OptionalNullable<string?> EndsAt { get; set; }
        public bool ShouldSerializeEndsAt() => EndsAt.IsSet;

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