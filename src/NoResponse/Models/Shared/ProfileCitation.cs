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

    public class ProfileCitation
    {
        /// <summary>
        /// record: a record in lookup.records or profiles[].record. post: an item in posts. check: an item in lookup.checks.
        /// </summary>
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public ProfileCitationKind Kind { get; set; } = default!;

        /// <summary>
        /// The record or post id. For a check, c followed by its 1-based position in lookup.checks.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;
    }
}