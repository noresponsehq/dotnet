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
        /// The kind of searched value: email, phone, username, name, or handle for a profile read of a handle.
        /// </summary>
        [JsonProperty("by", Required = Newtonsoft.Json.Required.Always)]
        public string By { get; set; } = default!;

        /// <summary>
        /// The field the searched value hit, when the record holds it.
        /// </summary>
        [JsonProperty("field", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Field { get; set; }
    }
}