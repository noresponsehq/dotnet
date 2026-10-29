#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class ReviewViewDeleteRequest
    {
        [JsonProperty("expected_version", Required = Newtonsoft.Json.Required.Always)]
        public long ExpectedVersion { get; set; } = default!;
    }
}