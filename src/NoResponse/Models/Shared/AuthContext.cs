#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Normalised claims from a session your backend has already verified with WorkOS, Clerk, Auth0 or any OIDC provider. Pass the output of the SDK's auth adapters, not a raw token.
    /// </summary>
    public class AuthContext
    {
        /// <summary>
        /// Lowercase provider slug. WorkOS, Clerk and Auth0 have first-class adapters; every other provider uses a customer-specific OIDC slug. Provider brand is metadata; it does not change risk by itself.
        /// </summary>
        [JsonProperty("provider", Required = Newtonsoft.Json.Required.Always)]
        public string Provider { get; set; } = default!;

        /// <summary>
        /// The verified token issuer, normally the provider's HTTPS issuer URL.
        /// </summary>
        [JsonProperty("issuer", Required = Newtonsoft.Json.Required.Always)]
        public string Issuer { get; set; } = default!;

        /// <summary>
        /// The verified audience/resource identifier used by the customer's API. Use audiences when the provider emits multiple values.
        /// </summary>
        [JsonProperty("audience", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Audience { get; set; }

        /// <summary>
        /// All verified audience/resource identifiers from an OAuth or OIDC token. The first value may also be copied to audience for simple integrations.
        /// </summary>
        [JsonProperty("audiences", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Audiences { get; set; }

        /// <summary>
        /// The verified stable subject identifier (`sub`), opaque to NoResponse. Required, except from a provider hook at pre_registration, where no user exists yet.
        /// </summary>
        [JsonProperty("subject", Required = Newtonsoft.Json.Required.Always)]
        public string Subject { get; set; } = default!;

        /// <summary>
        /// The verified session identifier (`sid` or provider equivalent).
        /// </summary>
        [JsonProperty("session_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? SessionId { get; set; }

        /// <summary>
        /// The active organization or workspace identifier, when the provider issued one.
        /// </summary>
        [JsonProperty("organization_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? OrganizationId { get; set; }

        /// <summary>
        /// The provider tenant identifier, when distinct from organization_id.
        /// </summary>
        [JsonProperty("tenant_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? TenantId { get; set; }

        /// <summary>
        /// The verified token identifier (`jti`), used for replay and rotation correlation.
        /// </summary>
        [JsonProperty("token_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? TokenId { get; set; }

        /// <summary>
        /// Verified token issue time (`iat`).
        /// </summary>
        [JsonProperty("issued_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? IssuedAt { get; set; }

        /// <summary>
        /// Verified token expiry (`exp`).
        /// </summary>
        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExpiresAt { get; set; }

        /// <summary>
        /// Creation time of the provider session from the verified backend session record (for example Clerk Session.createdAt or WorkOS Session.created_at). This is separate from token issuance and auth_time.
        /// </summary>
        [JsonProperty("session_created_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? SessionCreatedAt { get; set; }

        /// <summary>
        /// Verified end-user authentication time (`auth_time`), when present.
        /// </summary>
        [JsonProperty("auth_time", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? AuthTime { get; set; }

        /// <summary>
        /// Account creation time from the customer's verified provider record, when available.
        /// </summary>
        [JsonProperty("account_created_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? AccountCreatedAt { get; set; }

        /// <summary>
        /// Whether the customer's provider asserts that the email identifier was verified.
        /// </summary>
        [JsonProperty("email_verified", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? EmailVerified { get; set; }

        /// <summary>
        /// Whether the customer's provider asserts that the phone identifier was verified.
        /// </summary>
        [JsonProperty("phone_verified", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? PhoneVerified { get; set; }

        /// <summary>
        /// Provider or OIDC assurance level, when the customer has mapped one.
        /// </summary>
        [JsonProperty("assurance_level", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AssuranceLevel? AssuranceLevel { get; set; }

        /// <summary>
        /// Whether the verified session is acting on behalf of another subject.
        /// </summary>
        [JsonProperty("impersonated", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Impersonated { get; set; }

        /// <summary>
        /// Where this context was derived. Backend is the trusted integration path; browser values are treated as weak hints. provider_hook is a hosted provider hook (an Auth0 or WorkOS Action) calling at hook_point; it needs hook_point and verified false, and its authentication facts are not scored.
        /// </summary>
        [JsonProperty("transport", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Transport? Transport { get; set; }

        /// <summary>
        /// The provider event a provider_hook context was sent for. pre_registration runs before the user exists, so subject and the request's account_id may be absent there. Sent only with transport provider_hook.
        /// </summary>
        [JsonProperty("hook_point", Required = Newtonsoft.Json.Required.DisallowNull)]
        public HookPoint? HookPoint { get; set; }

        /// <summary>
        /// Whether the customer completed signature, issuer, audience and expiry validation before calling NoResponse. False contexts are accepted only as weak hints.
        /// </summary>
        [JsonProperty("verified", Required = Newtonsoft.Json.Required.Always)]
        public bool Verified { get; set; } = default!;

        /// <summary>
        /// Normalized multi-factor evidence from a verified provider session.
        /// </summary>
        [JsonProperty("mfa", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AuthContextMfa? Mfa { get; set; }

        /// <summary>
        /// Bounded role slugs from the verified provider; roles do not change risk by themselves.
        /// </summary>
        [JsonProperty("roles", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Roles { get; set; }

        /// <summary>
        /// Bounded scopes from the verified access token; scopes do not change risk by themselves.
        /// </summary>
        [JsonProperty("scopes", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Scopes { get; set; }
    }
}