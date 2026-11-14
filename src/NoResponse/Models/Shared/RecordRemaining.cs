#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// Rows one search matched in one source that the lookup's records leave out, so no answer is cut short in silence.
    /// </summary>
    public class RecordRemaining
    {
        /// <summary>
        /// Where a record came from.
        /// </summary>
        [JsonProperty("source", Required = Newtonsoft.Json.Required.Always)]
        public RecordSource Source { get; set; } = new();

        /// <summary>
        /// The kind of searched value whose search matched the rows, as a record's matched names it.
        /// </summary>
        [JsonProperty("by", Required = Newtonsoft.Json.Required.Always)]
        public string By { get; set; } = default!;

        /// <summary>
        /// How many rows of the source that search matched beyond the records the lookup carries.
        /// </summary>
        [JsonProperty("rows", Required = Newtonsoft.Json.Required.Always)]
        public long Rows { get; set; } = default!;
    }
}