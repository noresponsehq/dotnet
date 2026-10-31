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
    /// One privileged workspace action with its actor, target, outcome and time. Successful data changes commit their audit evidence atomically. Sensitive exports record access before returning bytes. Details exclude secrets and subject identifiers.
    /// </summary>
    public class AuditEvent
    {
        /// <summary>
        /// Opaque, server-assigned event id: aud_ and a 26-character ULID.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// The workspace id, which is also its live environment's workspace id; the test environment's is the same id with :test appended. A workspace created through the API is tnt_ and a 26-character ULID.
        /// </summary>
        [JsonProperty("workspace_id", Required = Newtonsoft.Json.Required.Always)]
        public string WorkspaceId { get; set; } = default!;

        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public AuditSettingsEnvironment Environment { get; set; } = default!;

        /// <summary>
        /// What happened, as resource.verb: workspace.created, member.role_changed, member.removed, invite.created, invite.revoked, invite.accepted, key.rotated, webhook_endpoint.created, webhook_endpoint.deleted, webhook_endpoint.secret_rotated, integration.created, integration.deleted, integration.secret_rotated, list_entry.created, list_entry.deleted, webhook_endpoint.test_sent, webhook_endpoint.verification_requested, webhook_endpoint.delivery_replayed, assessment.created, assessment.outcome_recorded, assessment.deleted, account.outcome_recorded, account.enforcement_recorded, account.deleted, account.exported. A change with no finer name carries the permission it used, such as keys.live.write. A member's read of raw evidence is breach_records.read or review_locations.read, with the assessment and counts and no value. New actions may appear; treat an unknown one as a change to its target.
        /// </summary>
        [JsonProperty("action", Required = Newtonsoft.Json.Required.Always)]
        public string Action { get; set; } = default!;

        [JsonProperty("outcome", Required = Newtonsoft.Json.Required.Always)]
        public CustomerExportRequestFilterOutcome Outcome { get; set; } = default!;

        [JsonProperty("actor", Required = Newtonsoft.Json.Required.Always, NullValueHandling = NullValueHandling.Include)]
        public Actor Actor { get; set; } = default!;

        public UserActor? GetActorUser()
        {
            return Actor.UserActor;
        }

        public KeyActor? GetActorApiKey()
        {
            return Actor.KeyActor;
        }

        [JsonProperty("target", Required = Newtonsoft.Json.Required.Always)]
        public AuditEventTarget Target { get; set; } = new();

        /// <summary>
        /// At most 8 short facts about the change, keyed by lowercase names: the role a member was given or accepted (role), the invite an accepted member used (invite_id), the key that replaced a rotated one (new_key_id), the assessment a member's change acted on (assessment_id). Absent when there are none.
        /// </summary>
        [JsonProperty("detail", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Dictionary<string, string>? Detail { get; set; }

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("affected_environment", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Models.Shared.Environment? AffectedEnvironment { get; set; }

        /// <summary>
        /// Validated request correlation id, also returned in X-Request-ID, linking this event to request diagnostics. Absent on historical events without correlation.
        /// </summary>
        [JsonProperty("request_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? RequestId { get; set; }
    }
}