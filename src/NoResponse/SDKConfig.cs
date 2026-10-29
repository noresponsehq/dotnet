#nullable enable
namespace NoResponse
{
    using NoResponse.Hooks;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using NoResponse.Utils.Retries;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Main configuration for the SDK.
    /// </summary>
    public struct SDKConfig
    {
        /// <summary>
        /// List of server URLs available to the SDK.
        /// </summary>
        public static readonly string[] ServerList = {
            "https://api.noresponse.org",
        };

        public INoResponseHttpClient Client;
        public string ServerUrl;
        public int ServerIndex;
        public string UserAgent;
        public Func<NoResponse.Models.Shared.Security>? SecuritySource;
        public SDKHooks Hooks;
        public RetryConfig? RetryConfig;

        /// <summary>
        /// Initializes a new instance of the SDKConfig struct with default values.
        /// </summary>
        public SDKConfig(INoResponseHttpClient? client = null)
        {
            Client = client ?? new NoResponseHttpClient();
            ServerUrl = "";
            ServerIndex = 0;
            UserAgent = "noresponse-sdk/csharp 9.4.4 internal 1 NoResponse";
            SecuritySource = null;
            Hooks = new SDKHooks();
            RetryConfig = null;
        }

        public string GetTemplatedServerUrl()
        {
            if (!String.IsNullOrEmpty(this.ServerUrl))
            {
                return Utilities.TemplateUrl(Utilities.RemoveSuffix(this.ServerUrl, "/"), new Dictionary<string, string>());
            }
            return Utilities.TemplateUrl(SDKConfig.ServerList[this.ServerIndex], new Dictionary<string, string>());
        }
    }
}
