#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// One searched value that found the record.
    /// </summary>
    public class RecordMatch
    {
        /// <summary>
        /// The kind of searched value: email, phone, username, ip, name, password, hash, or handle for a profile read of a handle.
        /// </summary>
        [JsonProperty("by", Required = Newtonsoft.Json.Required.Always)]
        public string By { get; set; } = default!;

        /// <summary>
        /// The field the searched value hit, when the record holds it.
        /// </summary>
        [JsonProperty("field", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Field { get; set; }

        /// <summary>
        /// The search a person started on a value of another record that found this one, among the person report's searches, which names the value and the record and field it came from. A record found only by such searches is a lead, whatever kind of value found it. Absent for a match of the lookups.
        /// </summary>
        [JsonProperty("search", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Search { get; set; }
    }
}