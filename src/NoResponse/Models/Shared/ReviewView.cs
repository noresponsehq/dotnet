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

    public class ReviewView
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public string Name { get; set; } = default!;

        [JsonProperty("owner", Required = Newtonsoft.Json.Required.Always)]
        public string Owner { get; set; } = default!;

        /// <summary>
        /// The credential namespace of the creator. An external user ID equal to an API-key label does not grant ownership of that key's private view.
        /// </summary>
        [JsonProperty("owner_type", Required = Newtonsoft.Json.Required.Always)]
        public OwnerType OwnerType { get; set; } = default!;

        [JsonProperty("shared", Required = Newtonsoft.Json.Required.Always)]
        public bool Shared { get; set; } = default!;

        [JsonProperty("version", Required = Newtonsoft.Json.Required.Always)]
        public long Version { get; set; } = default!;

        [JsonProperty("filter", Required = Newtonsoft.Json.Required.Always)]
        public Filter Filter { get; set; } = new();

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("updated_at", Required = Newtonsoft.Json.Required.Always)]
        public string UpdatedAt { get; set; } = default!;

        /// <summary>
        /// Immutable member ID or API key ID, interpreted with owner_type. Ownership survives changes to an external user identity.
        /// </summary>
        [JsonProperty("owner_principal_id", Required = Newtonsoft.Json.Required.Always)]
        public string OwnerPrincipalId { get; set; } = default!;
    }
}