#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Retained tenant-local subject links and the current review version.
    /// </summary>
    public class ReviewSubjectList
    {
        [JsonProperty("review_version", Required = Newtonsoft.Json.Required.Always)]
        public long ReviewVersion { get; set; } = default!;

        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<ReviewSubject> Items { get; set; } = default!;
    }
}