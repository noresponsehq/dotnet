#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Everything known about one person, and where else in the environment they appear.
    /// </summary>
    public class PersonReport
    {
        /// <summary>
        /// A person and where their screening and review stand.
        /// </summary>
        [JsonProperty("person", Required = Newtonsoft.Json.Required.Always)]
        public Person Person { get; set; } = new();

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
        /// The searches on this person's values in this group, newest first: each names the value, the record and field it came from, and the search that found that record. Absent when nothing was searched.
        /// </summary>
        [JsonProperty("searches", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<Search>? Searches { get; set; }

        /// <summary>
        /// The searches enrichment would start on this person in this group, likeliest record first. Group screening follows the environment's enrichment setting, with suggest as its mode while the workspace has not set one. In off mode the list is empty. In suggest mode enrichment starts none of them by itself; in automatic mode it starts them as its limits allow. Empty when evidence.read is denied.
        /// </summary>
        [JsonProperty("suggested_searches", Required = Newtonsoft.Json.Required.Always)]
        public List<SuggestedSearch> SuggestedSearches { get; set; } = default!;

        /// <summary>
        /// Where else in the environment the person appears, read live from those groups: deleting one removes it here.
        /// </summary>
        [JsonProperty("other_groups", Required = Newtonsoft.Json.Required.Always)]
        public OtherGroups OtherGroups { get; set; } = new();

        /// <summary>
        /// True when evidence.read is explicitly denied. Submitted details, coverage, job status and decisions remain available. Collected records, profiles, findings, posts, media, discovered values and free-text decision notes are withheld.
        /// </summary>
        [JsonProperty("evidence_restricted", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? EvidenceRestricted { get; set; }
    }
}