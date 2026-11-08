#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Adds or updates 1 to 500 attendees. A row naming an existing attendee of the event (the same external_id, else the same email, else the same phone) updates it.
    /// </summary>
    public class AttendeeBatchRequest
    {
        [JsonProperty("attendees", Required = Newtonsoft.Json.Required.Always)]
        public List<AttendeeIdentity> Attendees { get; set; } = default!;
    }
}