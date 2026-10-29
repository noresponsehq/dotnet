#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class ReviewCommentRequest
    {
        [JsonProperty("expected_version", Required = Newtonsoft.Json.Required.Always)]
        public long ExpectedVersion { get; set; } = default!;

        [JsonProperty("body", Required = Newtonsoft.Json.Required.Always)]
        public string Body { get; set; } = default!;
    }
}