#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// GET /v1/workspaces/{workspace_id}/lists/{environment}: every entry of the environment's list, newest first. An environment holds at most 10,000 entries.
    /// </summary>
    public class ListEntryList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<ListEntry> Items { get; set; } = default!;
    }
}