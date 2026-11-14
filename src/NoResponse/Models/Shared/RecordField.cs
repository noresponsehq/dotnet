#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

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
        /// What sort of value a field holds, which decides how it is cleaned, compared and searched again. It comes from the value itself where its shape is certain (an email, an IP, a URL, an international phone number), else from the answer stored for the source's column, else text. A value of any kind is returned.
        /// </summary>
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.DisallowNull)]
        public RecordKind? Kind { get; set; }

        /// <summary>
        /// Which common field a value is, where many sources share it: a join date and a birth date are both dates, with the roles joined and born. A platform account's field and a breach row's column with the same meaning carry the same kind and role. A linked_account field is named by its network. Absent for a field only its source returns.
        /// </summary>
        [JsonProperty("role", Required = Newtonsoft.Json.Required.DisallowNull)]
        public RecordRole? Role { get; set; }

        /// <summary>
        /// The value cleaned to its kind's one form: an email in lower case, a phone number in international form when its country is known, a date in ISO form, a country as its two-letter code, a flag as true or false. Absent when the value cannot be cleaned as its kind; original still holds it.
        /// </summary>
        [JsonProperty("value", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Value { get; set; }

        /// <summary>
        /// What the value shows of itself beyond its kind: the likely algorithm of a hash, the likely coin of a crypto address or the currency of an amount.
        /// </summary>
        [JsonProperty("detail", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Detail { get; set; }
    }
}