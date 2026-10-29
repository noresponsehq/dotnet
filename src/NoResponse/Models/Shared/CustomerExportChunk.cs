#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Download one authorized export chunk.
    /// </summary>
    public class CustomerExportChunk
    {
        [JsonProperty("export_id", Required = Newtonsoft.Json.Required.Always)]
        public string ExportId { get; set; } = default!;

        [JsonProperty("index", Required = Newtonsoft.Json.Required.Always)]
        public long Index { get; set; } = default!;

        [JsonProperty("generation", Required = Newtonsoft.Json.Required.Always)]
        public long Generation { get; set; } = default!;

        [JsonProperty("sha256", Required = Newtonsoft.Json.Required.Always)]
        public string Sha256 { get; set; } = default!;

        /// <summary>
        /// Base64-encoded UTF-8 JSON Lines, at most 256 KiB decoded. Each line is a typed customer export record. Decode and concatenate chunks in index order within the same generation.
        /// </summary>
        [JsonProperty("data", Required = Newtonsoft.Json.Required.Always)]
        public string Data { get; set; } = default!;

        [JsonProperty("next_chunk", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? NextChunk { get; set; }
    }
}