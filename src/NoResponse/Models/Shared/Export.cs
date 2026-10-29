#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Retained review snapshot.
    /// </summary>
    public class Export
    {
        [JsonProperty("exported_at", Required = Newtonsoft.Json.Required.Always)]
        public string ExportedAt { get; set; } = default!;

        [JsonProperty("review", Required = Newtonsoft.Json.Required.Always)]
        public Review Review { get; set; } = new();

        [JsonProperty("comments", Required = Newtonsoft.Json.Required.Always)]
        public List<ReviewComment> Comments { get; set; } = default!;

        [JsonProperty("resolutions", Required = Newtonsoft.Json.Required.Always)]
        public List<Resolution> Resolutions { get; set; } = default!;

        [JsonProperty("subjects", Required = Newtonsoft.Json.Required.Always)]
        public List<ReviewSubject> Subjects { get; set; } = default!;
    }
}