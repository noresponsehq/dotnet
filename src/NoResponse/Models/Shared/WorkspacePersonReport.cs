#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Read a workspace person.
    /// </summary>
    public class WorkspacePersonReport
    {
        /// <summary>
        /// Your reference for this person within the workspace environment. Only an explicit reference or person_id can reuse an existing person.
        /// </summary>
        [JsonProperty("external_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExternalId { get; set; }

        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Name { get; set; }

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
        /// A username the person uses, with no platform. A search looks it up on every platform that finds accounts by username.
        /// </summary>
        [JsonProperty("username", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Username { get; set; }

        /// <summary>
        /// Accounts the person is known to hold, at most 20.
        /// </summary>
        [JsonProperty("handles", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<Handle>? Handles { get; set; }

        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("updated_at", Required = Newtonsoft.Json.Required.Always)]
        public string UpdatedAt { get; set; } = default!;

        /// <summary>
        /// The time the workspace retention policy removes this person, their evidence and investigation history. Shortening workspace retention can move this time earlier.
        /// </summary>
        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.Always)]
        public string ExpiresAt { get; set; } = default!;

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
        /// What the signup check's lookups found for a person: account presence for the email, phone and usernames (the person's handles), the phone lookup, the email domain's registration and the breach search. Context and coverage for the reviewer, not a finding: nothing here sets a band or a match state.
        /// </summary>
        [JsonProperty("lookup", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ScreeningLookup? Lookup { get; set; }

        /// <summary>
        /// Every search on this person's values, newest first, whether a person started it or enrichment did: each names the value, the record and field it came from, and the search that found that record. Absent when nothing was searched.
        /// </summary>
        [JsonProperty("searches", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<Search>? Searches { get; set; }

        /// <summary>
        /// The searches enrichment would start on this person, likeliest record first, under the environment's enrichment setting for a person's own investigation. In off mode the list is empty. In suggest mode enrichment starts none of them by itself; in automatic mode it starts them as its limits allow. Empty when evidence.read is denied.
        /// </summary>
        [JsonProperty("suggested_searches", Required = Newtonsoft.Json.Required.Always)]
        public List<SuggestedSearch> SuggestedSearches { get; set; } = default!;

        /// <summary>
        /// True when evidence.read is explicitly denied. Submitted details, coverage, job status and decisions remain available. Collected records, profiles, findings, posts, media, discovered values and free-text decision notes are withheld.
        /// </summary>
        [JsonProperty("evidence_restricted", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? EvidenceRestricted { get; set; }

        /// <summary>
        /// The most recent retained completed or partial investigation, or a migrated result whose original per-person status is unknown. The evidence below belongs to this investigation. Read investigation history for earlier results and pending jobs.
        /// </summary>
        [JsonProperty("evidence_investigation", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public Investigation? EvidenceInvestigation { get; set; }

        /// <summary>
        /// An investigator's profile of this person, written from the evidence of a settled investigation. Null until an investigation completes or partially completes, and null when evidence.read is denied.
        /// </summary>
        [JsonProperty("written_profile", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public WrittenProfile? WrittenProfile { get; set; }
    }
}