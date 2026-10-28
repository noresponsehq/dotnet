#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Authenticated attempts in a bounded interval, newest first. Keep the same filters when paging.
    /// </summary>
    public class RequestDiagnosticPage
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<RequestDiagnostic> Items { get; set; } = default!;

        [JsonProperty("next_cursor", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? NextCursor { get; set; }
    }
}