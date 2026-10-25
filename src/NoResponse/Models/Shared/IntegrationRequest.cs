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
    /// POST /v1/integrations body. Registers a hosted endpoint for one authentication provider. The workspace is the API key that signs the request, so the body has no workspace field and refuses one like any other unknown member. A WorkOS integration needs signing_secret, the Actions secret shown in the WorkOS dashboard, which NoResponse uses to verify each Action request and to sign its answer. A Supabase integration needs signing_secret, the hook secret Supabase generates for a Before User Created HTTPS hook (v1,whsec_ and base64), which NoResponse uses to verify each call. An Auth0 integration sends no signing_secret: NoResponse generates the shared secret and returns it once.
    /// </summary>
    public class IntegrationRequest
    {
        /// <summary>
        /// The authentication provider that calls the endpoint.
        /// </summary>
        [JsonProperty("platform", Required = Newtonsoft.Json.Required.Always)]
        public IntegrationPlatform Platform { get; set; } = default!;

        /// <summary>
        /// The provider events the endpoint scores. Default every hook point the platform has: pre_registration (WorkOS user registration Action, Auth0 pre-user-registration trigger, Supabase Before User Created hook) and post_login (WorkOS authentication Action, Auth0 post-login trigger). Supabase has only pre_registration, so a Supabase integration that lists post_login is refused. An event that is not listed is answered allow without an assessment.
        /// </summary>
        [JsonProperty("hook_points", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<IntegrationHookPoint>? HookPoints { get; set; }

        /// <summary>
        /// The WorkOS Actions signing secret or the Supabase hook secret, as the provider's dashboard shows it: 16 to 256 printable ASCII characters with no spaces. It is stored sealed and is not returned.
        /// </summary>
        [JsonProperty("signing_secret", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? SigningSecret { get; set; }
    }
}