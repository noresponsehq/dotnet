#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// GET /v1/workspaces/{workspace_id}/members: the workspace's active members, oldest first, at most 100.
    /// </summary>
    public class MemberList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<Member> Items { get; set; } = default!;
    }
}