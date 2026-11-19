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
    /// One group the person belongs to and their role in it.
    /// </summary>
    public class PersonGroup
    {
        [JsonProperty("group_id", Required = Newtonsoft.Json.Required.Always)]
        public string GroupId { get; set; } = default!;

        [JsonProperty("group_name", Required = Newtonsoft.Json.Required.Always)]
        public string GroupName { get; set; } = default!;

        /// <summary>
        /// The group's optional event start; null for an ongoing group.
        /// </summary>
        [JsonProperty("group_starts_at", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string? GroupStartsAt { get; set; }

        /// <summary>
        /// The person's role in the group: guest, crew, vendor or press, or null when it was not given.
        /// </summary>
        [JsonProperty("role", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public PersonRole? Role { get; set; }

        /// <summary>
        /// When the person was added to the group.
        /// </summary>
        [JsonProperty("added_at", Required = Newtonsoft.Json.Required.Always)]
        public string AddedAt { get; set; } = default!;
    }
}