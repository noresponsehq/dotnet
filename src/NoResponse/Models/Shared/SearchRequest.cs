#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// The value to search again, named by the record and field that hold it rather than sent again.
    /// </summary>
    public class SearchRequest
    {
        /// <summary>
        /// One of the person's records: a lookup's, a profile's or one an earlier search found.
        /// </summary>
        [JsonProperty("record_id", Required = Newtonsoft.Json.Required.Always)]
        public string RecordId { get; set; } = default!;

        /// <summary>
        /// The name of the record's field whose value to search. Its kind must be email, phone, username, person_name, password, hash or ip.
        /// </summary>
        [JsonProperty("field", Required = Newtonsoft.Json.Required.Always)]
        public string Field { get; set; } = default!;

        /// <summary>
        /// Search only this breach table, as a remaining entry names it, to fetch the rows an earlier search left out. Not for a phone.
        /// </summary>
        [JsonProperty("table", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Table { get; set; }
    }
}