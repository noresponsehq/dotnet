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
    /// The identity fields of the auth context the assessment was made with, echoed for the workspace's own correlation: the request's auth_context, or the one a hosted provider hook (POST /hooks/{integration_id}) built, with transport provider_hook and its hook_point. Present exactly when the request carried an auth_context, and the same on every revision of the assessment. Values are as the request or the hook sent them; tokens, timestamps, roles and scopes are not echoed, and the echo does not change the score.
    /// </summary>
    public class AssessmentDetailAuthContext
    {
        /// <summary>
        /// The provider slug the context named.
        /// </summary>
        [JsonProperty("provider", Required = Newtonsoft.Json.Required.Always)]
        public string Provider { get; set; } = default!;

        /// <summary>
        /// The issuer the context named.
        /// </summary>
        [JsonProperty("issuer", Required = Newtonsoft.Json.Required.Always)]
        public string Issuer { get; set; } = default!;

        /// <summary>
        /// The subject the context named; absent when it named none (a pre_registration hook).
        /// </summary>
        [JsonProperty("subject", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Subject { get; set; }

        /// <summary>
        /// The session identifier the context named, when it named one.
        /// </summary>
        [JsonProperty("session_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? SessionId { get; set; }

        /// <summary>
        /// The organization identifier the context named, when it named one.
        /// </summary>
        [JsonProperty("organization_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? OrganizationId { get; set; }

        /// <summary>
        /// The verified flag the context carried. A provider_hook context is always false.
        /// </summary>
        [JsonProperty("verified", Required = Newtonsoft.Json.Required.Always)]
        public bool Verified { get; set; } = default!;

        /// <summary>
        /// Where the context was derived: backend (also when the request named no transport), browser, or provider_hook for a hosted provider hook.
        /// </summary>
        [JsonProperty("transport", Required = Newtonsoft.Json.Required.Always)]
        public Transport Transport { get; set; } = default!;

        /// <summary>
        /// The provider event a provider_hook context was sent for; absent for any other transport.
        /// </summary>
        [JsonProperty("hook_point", Required = Newtonsoft.Json.Required.DisallowNull)]
        public HookPoint? HookPoint { get; set; }
    }
}