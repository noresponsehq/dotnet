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
    /// POST /v1/workspaces/{workspace_id}/keys body. Creates an API key in one environment of the workspace with the scopes named, and returns its secret once. A live key needs the admin or owner role; a test key needs developer or above. An API key with the management scope creates keys in its own environment with scopes it holds itself. Each scope may appear once.
    /// </summary>
    public class KeyRequest
    {
        /// <summary>
        /// The environment the key belongs to. A live key assesses with live sources; a test key answers from the deterministic simulator.
        /// </summary>
        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public KeyEnvironment Environment { get; set; } = default!;

        /// <summary>
        /// The key's scopes, each at most once.
        /// </summary>
        [JsonProperty("scopes", Required = Newtonsoft.Json.Required.Always)]
        public List<KeyRequestScope> Scopes { get; set; } = default!;
    }
}