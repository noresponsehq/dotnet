#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Everything known about one attendee, and where else in the environment they appear.
    /// </summary>
    public class AttendeeReport
    {
        /// <summary>
        /// An attendee and where their screening and review stand.
        /// </summary>
        [JsonProperty("attendee", Required = Newtonsoft.Json.Required.Always)]
        public Attendee Attendee { get; set; } = new();

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
        public List<AttendeeReview> Reviews { get; set; } = default!;

        /// <summary>
        /// Where else in the environment the attendee appears, read live from those events: deleting one removes it here.
        /// </summary>
        [JsonProperty("other_events", Required = Newtonsoft.Json.Required.Always)]
        public OtherEvents OtherEvents { get; set; } = new();
    }
}