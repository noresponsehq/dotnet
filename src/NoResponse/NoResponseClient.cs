#nullable enable
namespace NoResponse
{
    using Newtonsoft.Json;
    using NoResponse.Hooks;
    using NoResponse.Models.Errors;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using NoResponse.Utils.Retries;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    /// <summary>
    /// NoResponse API: Server-side signup and gateway risk assessment: a recommendation, polling and signed webhooks.
    /// </summary>
    /// <remarks>
    /// Assess a signup or gateway event, poll a pending assessment, page through an account's history, record what you later learned, rotate your API key, and register hosted endpoints that answer your WorkOS or Auth0 sign-up and sign-in hooks. Every response carries X-Request-ID; every error is an RFC 9457 problem document. Keep the API key on your server.
    /// </remarks>
    public interface INoResponseClient
    {
        /// <summary>
        /// Signup and gateway assessments.
        /// </summary>
        public IAssessments Assessments { get; }

        /// <summary>
        /// Your own API keys.
        /// </summary>
        public IKeys Keys { get; }

        /// <summary>
        /// Hosted endpoints that answer your sign-up and sign-in hooks.
        /// </summary>
        public IIntegrations Integrations { get; }
    }

    /// <summary>
    /// NoResponse API: Server-side signup and gateway risk assessment: a recommendation, polling and signed webhooks.
    /// </summary>
    /// <remarks>
    /// Assess a signup or gateway event, poll a pending assessment, page through an account's history, record what you later learned, rotate your API key, and register hosted endpoints that answer your WorkOS or Auth0 sign-up and sign-in hooks. Every response carries X-Request-ID; every error is an RFC 9457 problem document. Keep the API key on your server.
    /// </remarks>
    public class NoResponseClient : INoResponseClient
    {
        /// <summary>
        /// The main SDK Configuration.
        /// </summary>
        public SDKConfig SDKConfiguration { get; private set; }
        /// <summary>
        /// The Assessments sub-SDK.
        /// </summary>
        public IAssessments Assessments { get; private set; }
        /// <summary>
        /// The Keys sub-SDK.
        /// </summary>
        public IKeys Keys { get; private set; }
        /// <summary>
        /// The Integrations sub-SDK.
        /// </summary>
        public IIntegrations Integrations { get; private set; }

        /// <summary>
        /// Initializes a new instance of the SDK based on a <see cref="SDKConfig"/> configuration object.
        /// </summary>
        /// <param name="config">The SDK configuration object.</param>
        public NoResponseClient(SDKConfig config)
        {
            SDKConfiguration = config;
            InitHooks();

            Assessments = new Assessments(SDKConfiguration);

            Keys = new Keys(SDKConfiguration);

            Integrations = new Integrations(SDKConfiguration);
        }

        /// <summary>
        /// Initializes a new instance of the SDK with optional configuration parameters.
        /// </summary>
        /// <param name="apiKey">The security configuration to use for API requests. If provided, this will be used as a static security configuration.</param>
        /// <param name="apiKeySource">A function that returns the security configuration dynamically. This takes precedence over the static security parameter if both are provided.</param>
        /// <param name="serverIndex">The index of the server to use from the predefined server list. Must be between 0 and the length of the server list. Defaults to 0 if not specified.</param>
        /// <param name="serverUrl">A custom server URL to use instead of the predefined server list. If provided with urlParams, the URL will be templated with the provided parameters.</param>
        /// <param name="urlParams">A dictionary of parameters to use for templating the serverUrl. Only used when serverUrl is provided.</param>
        /// <param name="client">A custom HTTP client implementation to use for making API requests. If not provided, the default NoResponseHttpClient will be used.</param>
        /// <param name="retryConfig">Configuration for retry behavior when API requests fail. Defines retry strategies, backoff policies, and maximum retry attempts.</param>
        /// <exception cref="ArgumentOutOfRangeException">Invalid value provided for <paramref name="serverIndex"/>: must be between 0 (inclusive) and 1 (exclusive).</exception>
        /// <exception cref="ArgumentException">None of <paramref name="apiKey"/> and <paramref name="apiKeySource"/> were provided.</exception>
        public NoResponseClient(
            string? apiKey = null,
            Func<string>? apiKeySource = null,
            int? serverIndex = null,
            string? serverUrl = null,
            Dictionary<string, string>? urlParams = null,
            INoResponseHttpClient? client = null,
            RetryConfig? retryConfig = null
        )
        {
            if (serverIndex != null)
            {
                if (serverIndex.Value < 0 || serverIndex.Value >= SDKConfig.ServerList.Length)
                {
                    throw new ArgumentOutOfRangeException($"Invalid server index {serverIndex}: must be between 0 (inclusive) and {SDKConfig.ServerList.Length} (exclusive).");
                }
            }

            if (serverUrl != null)
            {
                if (urlParams != null)
                {
                    serverUrl = Utilities.TemplateUrl(serverUrl, urlParams);
                }
            }
            Func<NoResponse.Models.Shared.Security>? _securitySource = null;

            if (apiKeySource != null)
            {
                _securitySource = () => new NoResponse.Models.Shared.Security() { ApiKey = apiKeySource() };
            }
            else if (apiKey != null)
            {
                _securitySource = () => new NoResponse.Models.Shared.Security() { ApiKey = apiKey };
            }
            else
            {
                throw new ArgumentException("apiKey and apiKeySource cannot both be null");
            }

            SDKConfiguration = new SDKConfig(client) {
                ServerIndex = serverIndex == null ? 0 : serverIndex.Value,
                ServerUrl = serverUrl == null ? "" : serverUrl,
                SecuritySource = _securitySource,
                RetryConfig = retryConfig
            };

            InitHooks();

            Assessments = new Assessments(SDKConfiguration);

            Keys = new Keys(SDKConfiguration);

            Integrations = new Integrations(SDKConfiguration);
        }

        private void InitHooks()
        {
            SDKConfiguration = SDKConfiguration.Hooks.SDKInit(SDKConfiguration);
        }

        /// <summary>
        /// Builder class for constructing an instance of the SDK.
        /// </summary>
        public class SDKBuilder
        {
            private SDKConfig _sdkConfig = new SDKConfig(client: new NoResponseHttpClient());

            public SDKBuilder()
            {
            }

            /// <summary>
            /// Overrides the default server by index.
            /// </summary>
            public SDKBuilder WithServerIndex(int serverIndex)
            {
                if (serverIndex < 0 || serverIndex >= SDKConfig.ServerList.Length)
                {
                    throw new ArgumentOutOfRangeException($"Invalid server index {serverIndex}: must be between 0 (inclusive) and {SDKConfig.ServerList.Length} (exclusive).");
                }
                _sdkConfig.ServerIndex = serverIndex;
                return this;
            }

            /// <summary>
            /// Overrides the default server URL for the SDK.
            /// </summary>
            public SDKBuilder WithServerUrl(string serverUrl, Dictionary<string, string>? serverVariables = null)
            {
                if (serverVariables != null)
                {
                    serverUrl = Utilities.TemplateUrl(serverUrl, serverVariables);
                }
                _sdkConfig.ServerUrl = serverUrl;
                return this;
            }

            /// <summary>
            /// Sets the apiKeySource security parameter for the SDK.
            /// </summary>
            public SDKBuilder WithApiKeySource(Func<string> apiKeySource)
            {
                _sdkConfig.SecuritySource = () => new NoResponse.Models.Shared.Security() { ApiKey = apiKeySource() };
                return this;
            }

            /// <summary>
            /// Sets the apiKey security parameter for the SDK.
            /// </summary>
            public SDKBuilder WithApiKey(string apiKey)
            {
                _sdkConfig.SecuritySource = () => new NoResponse.Models.Shared.Security() { ApiKey = apiKey };
                return this;
            }

            /// <summary>
            /// Sets a custom HTTP client to be used by the SDK.
            /// </summary>
            public SDKBuilder WithClient(INoResponseHttpClient client)
            {
                _sdkConfig.Client = client;
                return this;
            }

            /// <summary>
            /// Sets the retry configuration for the SDK.
            /// </summary>
            public SDKBuilder WithRetryConfig(RetryConfig retryConfig)
            {
                _sdkConfig.RetryConfig = retryConfig;
                return this;
            }

            /// <summary>
            /// Builds and returns the SDK instance.
            /// </summary>
            public NoResponseClient Build()
            {
                if (_sdkConfig.SecuritySource == null)
                {
                    throw new ArgumentException("securitySource cannot be null. One of `ApiKey` or `apiKeySource` needs to be defined.");
                }
                return new NoResponseClient(_sdkConfig);
            }
        }

        public static SDKBuilder Builder() => new SDKBuilder();
    }
}