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
    /// What you know about a person. Stored encrypted under the workspace's key.
    /// </summary>
    public class PersonIdentity
    {
        /// <summary>
        /// Your own id for the person, unique within the group. A later row with the same external id updates this person.
        /// </summary>
        [JsonProperty("external_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExternalId { get; set; }

        /// <summary>
        /// The person's full name.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// The person's email.
        /// </summary>
        [JsonProperty("email", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Email { get; set; }

        /// <summary>
        /// The person's phone in E.164 form.
        /// </summary>
        [JsonProperty("phone", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Phone { get; set; }

        /// <summary>
        /// Where the person works.
        /// </summary>
        [JsonProperty("company", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Company { get; set; }

        /// <summary>
        /// Where the person lives.
        /// </summary>
        [JsonProperty("city", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? City { get; set; }

        /// <summary>
        /// Accounts the person is known to hold, at most 20.
        /// </summary>
        [JsonProperty("handles", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<Handle>? Handles { get; set; }

        /// <summary>
        /// The person's role in the group: guest (a ticket holder), crew, vendor or press. Leave it out when you do not know it: a person without one has no role, not guest. A later row naming the same person replaces it, and a change of role alone keeps the person's screening results.
        /// </summary>
        [JsonProperty("role", Required = Newtonsoft.Json.Required.DisallowNull)]
        public PersonRole? Role { get; set; }
    }
}