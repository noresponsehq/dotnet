#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// GET /v1/workspaces/{workspace_id}/invites: the workspace's pending invites, oldest first, at most 50, with no token.
    /// </summary>
    public class InviteList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<Invite> Items { get; set; } = default!;
    }
}