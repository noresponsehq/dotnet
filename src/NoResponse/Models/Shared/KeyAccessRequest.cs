#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    public class KeyAccessRequest
    {
        /// <summary>
        /// Explicit restrictions that override scope grants. An empty list restores the access supplied by the scopes. Rotation preserves these restrictions.
        /// </summary>
        [JsonProperty("denied_actions", Required = Newtonsoft.Json.Required.Always)]
        public List<KeyAccessRequestDeniedAction> DeniedActions { get; set; } = default!;
    }
}