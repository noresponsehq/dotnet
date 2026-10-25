#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// GET /v1/workspaces/{workspace_id}/keys: the keys of both of the workspace's environments, newest first, at most 200 of each, revoked and expired keys included. No item carries a secret.
    /// </summary>
    public class KeyList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<Key> Items { get; set; } = default!;
    }
}