#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// POST /v1/workspaces/{workspace_id}/invites body. Creates an invite for role and returns its token once. email is where the console sends the token and is not checked against the user who accepts. With external_user_id set, only that user can accept.
    /// </summary>
    public class InviteRequest
    {
        /// <summary>
        /// A built-in capability set. Specialist roles are independent; effective access is returned by GET /v1/workspaces/{workspace_id}/capabilities.
        /// </summary>
        [JsonProperty("role", Required = Newtonsoft.Json.Required.Always)]
        public Role Role { get; set; } = default!;

        /// <summary>
        /// Where the console sends the invite. Informational: it is not matched against the accepting user.
        /// </summary>
        [JsonProperty("email", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Email { get; set; }

        /// <summary>
        /// The user's id at the identity provider the console signs people in with: 1 to 256 printable ASCII characters with no space, compared exactly.
        /// </summary>
        [JsonProperty("external_user_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExternalUserId { get; set; }

        /// <summary>
        /// Allowed data environments. Invites default to both. Omitted on member PATCH preserves the current grants; owners retain both.
        /// </summary>
        [JsonProperty("environments", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<InviteRequestEnvironment>? Environments { get; set; }

        /// <summary>
        /// Independent removals from a built-in preset: sensitive evidence, exports, or active policy publication. Owners retain full authority. Omitted member updates preserve exclusions.
        /// </summary>
        [JsonProperty("denied_actions", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<InviteRequestDeniedAction>? DeniedActions { get; set; }
    }
}