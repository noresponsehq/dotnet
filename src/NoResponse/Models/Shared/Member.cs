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
    /// One member of a workspace: an external user id holding one role. A removed member loses every permission at once.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Opaque, server-assigned member id: mem_ and a 26-character ULID. It stays the same when a removed member is invited back.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// The workspace id, which is also its live environment's workspace id; the test environment's is the same id with :test appended. A workspace created through the API is tnt_ and a 26-character ULID.
        /// </summary>
        [JsonProperty("workspace_id", Required = Newtonsoft.Json.Required.Always)]
        public string WorkspaceId { get; set; } = default!;

        /// <summary>
        /// The user's id at the identity provider the console signs people in with: 1 to 256 printable ASCII characters with no space, compared exactly.
        /// </summary>
        [JsonProperty("external_user_id", Required = Newtonsoft.Json.Required.Always)]
        public string ExternalUserId { get; set; } = default!;

        /// <summary>
        /// A built-in capability set. Specialist roles are independent; effective access is returned by GET /v1/workspaces/{workspace_id}/capabilities.
        /// </summary>
        [JsonProperty("role", Required = Newtonsoft.Json.Required.Always)]
        public Role Role { get; set; } = default!;

        /// <summary>
        /// Listed and returned members are active.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public MemberStatus Status { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("updated_at", Required = Newtonsoft.Json.Required.Always)]
        public string UpdatedAt { get; set; } = default!;

        /// <summary>
        /// Allowed data environments. Invites default to both. Omitted on member PATCH preserves the current grants; owners retain both.
        /// </summary>
        [JsonProperty("environments", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<MemberEnvironment>? Environments { get; set; }

        /// <summary>
        /// Independent removals from a built-in preset: sensitive evidence, exports, or active policy publication. Owners retain full authority. Omitted member updates preserve exclusions.
        /// </summary>
        [JsonProperty("denied_actions", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<MemberDeniedAction>? DeniedActions { get; set; }

        /// <summary>
        /// Immutable internal workspace principal, equal to member id, preserved across explicit provider-identity migrations.
        /// </summary>
        [JsonProperty("principal_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? PrincipalId { get; set; }

        /// <summary>
        /// Informational display name. Authentication and provider identity use the verified subject binding.
        /// </summary>
        [JsonProperty("display_name", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Unverified contact address for display. Empty clears it; a supplied value must be an address. Identity linking uses the verified provider subject.
        /// </summary>
        [JsonProperty("unverified_email", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? UnverifiedEmail { get; set; }
    }
}