#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class ReviewComment
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("review_version", Required = Newtonsoft.Json.Required.Always)]
        public long ReviewVersion { get; set; } = default!;

        [JsonProperty("body", Required = Newtonsoft.Json.Required.Always)]
        public string Body { get; set; } = default!;

        [JsonProperty("created_by", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedBy { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;
    }
}