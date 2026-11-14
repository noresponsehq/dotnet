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
        /// The accounts the latest screening matched, with how each was matched.
        /// </summary>
        [JsonProperty("profiles", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningProfile> Profiles { get; set; } = default!;

        /// <summary>
        /// What the latest screening flagged, most severe first, each citing its evidence.
        /// </summary>
        [JsonProperty("findings", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningFinding> Findings { get; set; } = default!;

        /// <summary>
        /// The posts the findings cite.
        /// </summary>
        [JsonProperty("posts", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningPost> Posts { get; set; } = default!;

        /// <summary>
        /// The images and videos the findings cite.
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
        /// Where else in the environment the person appears, read live from those groups: deleting one removes it here.
        /// </summary>
        [JsonProperty("other_groups", Required = Newtonsoft.Json.Required.Always)]
        public OtherGroups OtherGroups { get; set; } = new();
    }
}