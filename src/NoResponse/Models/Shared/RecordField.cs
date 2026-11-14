#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// One value a source returned.
    /// </summary>
    public class RecordField
    {
        /// <summary>
        /// The source's own name for the field. A nested value's name is dotted, and a list element's carries its index.
        /// </summary>
        [JsonProperty("field", Required = Newtonsoft.Json.Required.Always)]
        public string Field { get; set; } = default!;

        /// <summary>
        /// The value as the source wrote it. A count is written in decimal and a time in RFC 3339; a breach column's value is as the leak holds it.
        /// </summary>
        [JsonProperty("original", Required = Newtonsoft.Json.Required.Always)]
        public string Original { get; set; } = default!;

        /// <summary>
        /// What sort of value the field holds. Absent until the field's kind is known.
        /// </summary>
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Kind { get; set; }

        /// <summary>
        /// Which common field it is, where many sources share it, such as handle, display_name, account_id, profile_link, picture, bio, website, linked_account (named by its network), account_status, joined, last_updated, organisation, location, followers, following or posts. Absent for a field only this source returns.
        /// </summary>
        [JsonProperty("role", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Role { get; set; }

        /// <summary>
        /// The cleaned value. Absent when it cannot be cleaned, or until the field's kind is known.
        /// </summary>
        [JsonProperty("value", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Value { get; set; }
    }
}