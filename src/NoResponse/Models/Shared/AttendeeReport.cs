#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Everything known about one attendee.
    /// </summary>
    public class AttendeeReport
    {
        /// <summary>
        /// An attendee and where their screening and review stand.
        /// </summary>
        [JsonProperty("attendee", Required = Newtonsoft.Json.Required.Always)]
        public Attendee Attendee { get; set; } = new();

        /// <summary>
        /// The accounts the screening matched, with how each was matched.
        /// </summary>
        [JsonProperty("profiles", Required = Newtonsoft.Json.Required.Always)]
        public List<Profile> Profiles { get; set; } = default!;

        /// <summary>
        /// What the screening flagged, with its evidence.
        /// </summary>
        [JsonProperty("findings", Required = Newtonsoft.Json.Required.Always)]
        public List<Finding> Findings { get; set; } = default!;

        /// <summary>
        /// Every recorded decision, oldest first.
        /// </summary>
        [JsonProperty("reviews", Required = Newtonsoft.Json.Required.Always)]
        public List<AttendeeReview> Reviews { get; set; } = default!;
    }
}