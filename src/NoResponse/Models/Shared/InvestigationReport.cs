#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Read an investigation and its evidence.
    /// </summary>
    public class InvestigationReport
    {
        /// <summary>
        /// The accounts this result matched, with how each was matched.
        /// </summary>
        [JsonProperty("profiles", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningProfile> Profiles { get; set; } = default!;

        /// <summary>
        /// All posts retained for this result.
        /// </summary>
        [JsonProperty("posts", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningPost> Posts { get; set; } = default!;

        /// <summary>
        /// All images and videos retained for this result.
        /// </summary>
        [JsonProperty("media", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningMedia> Media { get; set; } = default!;

        /// <summary>
        /// What the signup check's lookups found for a person: account presence for the email, phone and usernames (the person's handles), the phone lookup, the email domain's registration and the breach search. Nothing here sets a match state.
        /// </summary>
        [JsonProperty("lookup", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ScreeningLookup? Lookup { get; set; }

        /// <summary>
        /// True when evidence.read is explicitly denied. Submitted details, coverage and job status remain available. Collected records, profiles, posts, media and discovered values are withheld.
        /// </summary>
        [JsonProperty("evidence_restricted", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? EvidenceRestricted { get; set; }

        [JsonProperty("person", Required = Newtonsoft.Json.Required.Always)]
        public WorkspacePerson Person { get; set; } = new();

        [JsonProperty("investigation", Required = Newtonsoft.Json.Required.Always)]
        public Investigation Investigation { get; set; } = new();
    }
}