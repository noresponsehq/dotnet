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
        /// What this result flagged, most severe first, each citing its evidence.
        /// </summary>
        [JsonProperty("findings", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningFinding> Findings { get; set; } = default!;

        /// <summary>
        /// All posts retained for this result, including posts that no finding cites.
        /// </summary>
        [JsonProperty("posts", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningPost> Posts { get; set; } = default!;

        /// <summary>
        /// All images and videos retained for this result, including media that no finding cites.
        /// </summary>
        [JsonProperty("media", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningMedia> Media { get; set; } = default!;

        /// <summary>
        /// Every recorded decision, oldest first.
        /// </summary>
        [JsonProperty("reviews", Required = Newtonsoft.Json.Required.Always)]
        public List<PersonReview> Reviews { get; set; } = default!;

        /// <summary>
        /// What the signup check's lookups found for a person: account presence for the email, phone and usernames (the person's handles), the phone lookup, the email domain's registration and the breach search. Context and coverage for the reviewer, not a finding: nothing here sets a band or a match state.
        /// </summary>
        [JsonProperty("lookup", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ScreeningLookup? Lookup { get; set; }

        /// <summary>
        /// True when evidence.read is explicitly denied. Submitted details, coverage, job status and decisions remain available. Collected records, profiles, findings, posts, media, discovered values and free-text decision notes are withheld.
        /// </summary>
        [JsonProperty("evidence_restricted", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? EvidenceRestricted { get; set; }

        [JsonProperty("person", Required = Newtonsoft.Json.Required.Always)]
        public WorkspacePerson Person { get; set; } = new();

        [JsonProperty("investigation", Required = Newtonsoft.Json.Required.Always)]
        public Investigation Investigation { get; set; } = new();
    }
}