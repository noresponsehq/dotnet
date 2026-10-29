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
    /// A pending workspace invitation. Creating or reissuing it returns a token once; lists omit it. The token can be accepted once before expiry and is invalidated on revocation or reissue. Token state does not establish email delivery.
    /// </summary>
    public class Invite
    {
        /// <summary>
        /// Opaque, server-assigned invite id: inv_ and a 26-character ULID.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// The workspace id, which is also its live environment's workspace id; the test environment's is the same id with :test appended. A workspace created through the API is tnt_ and a 26-character ULID.
        /// </summary>
        [JsonProperty("workspace_id", Required = Newtonsoft.Json.Required.Always)]
        public string WorkspaceId { get; set; } = default!;

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
        /// The stable member id that issued this invite, or the authenticated live API key prefix for members_manage automation. Reissuing preserves the original creator; the audit event names the reissuing actor.
        /// </summary>
        [JsonProperty("created_by", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedBy { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.Always)]
        public string ExpiresAt { get; set; } = default!;

        /// <summary>
        /// The invite token: nrinv_, the invite id's ULID, _ and 43 unpadded base64url characters (32 random bytes). The new member's console sends it to POST /v1/invites/accept.
        /// </summary>
        [JsonProperty("token", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Token { get; set; }

        /// <summary>
        /// Allowed data environments. Invites default to both. Omitted on member PATCH preserves the current grants; owners retain both.
        /// </summary>
        [JsonProperty("environments", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<InviteEnvironment>? Environments { get; set; }

        /// <summary>
        /// Independent removals from a built-in preset: sensitive evidence, exports, or active policy publication. Owners retain full authority. Omitted member updates preserve exclusions.
        /// </summary>
        [JsonProperty("denied_actions", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<InviteDeniedAction>? DeniedActions { get; set; }

        /// <summary>
        /// Last token reissue, not proof of email delivery.
        /// </summary>
        [JsonProperty("reissued_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ReissuedAt { get; set; }
    }
}