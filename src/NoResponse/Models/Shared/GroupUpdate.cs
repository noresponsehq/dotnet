#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Updates a group. Dates describe an optional event and do not control person retention.
    /// </summary>
    public class GroupUpdate
    {
        /// <summary>
        /// The group's name.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Name { get; set; }

        /// <summary>
        /// The attribute its people share. Omit to keep the current value.
        /// </summary>
        [JsonProperty("attribute", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Attribute { get; set; }

        /// <summary>
        /// Optional event start; null for an ongoing group. Omit to keep the current value; send null to clear it.
        /// </summary>
        [JsonProperty("starts_at", Required = Newtonsoft.Json.Required.Default, NullValueHandling = NullValueHandling.Include)]
        public OptionalNullable<string?> StartsAt { get; set; }
        public bool ShouldSerializeStartsAt() => StartsAt.IsSet;

        /// <summary>
        /// Optional event end; requires starts_at and must be at or after it. Null when no end is set. Omit to keep the current value; send null to clear it.
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

        /// <summary>
        /// When present, the change applies only if the group is still at this version (409 group_conflict otherwise).
        /// </summary>
        [JsonProperty("expected_version", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ExpectedVersion { get; set; }
    }
}