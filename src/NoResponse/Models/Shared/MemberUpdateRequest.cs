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
    /// PATCH /v1/workspaces/{workspace_id}/members/{member_id} body: the member's new role. The workspace's last owner cannot be demoted.
    /// </summary>
    public class MemberUpdateRequest
    {
        /// <summary>
        /// A built-in capability set. Specialist roles are independent; effective access is returned by GET /v1/workspaces/{workspace_id}/capabilities.
        /// </summary>
        [JsonProperty("role", Required = Newtonsoft.Json.Required.Always)]
        public Role Role { get; set; } = default!;

        /// <summary>
        /// Allowed data environments. Invites default to both. Omitted on member PATCH preserves the current grants; owners retain both.
        /// </summary>
        [JsonProperty("environments", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<MemberUpdateRequestEnvironment>? Environments { get; set; }

        /// <summary>
        /// Independent removals from a built-in preset: sensitive evidence, exports, or active policy publication. Owners retain full authority. Omitted member updates preserve exclusions.
        /// </summary>
        [JsonProperty("denied_actions", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<MemberUpdateRequestDeniedAction>? DeniedActions { get; set; }
    }
}