#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// One request made with an issued key. An event whose ip is not a public address, or whose occurred_at is more than 24 hours old or more than 5 minutes ahead, is counted in ignored and adds nothing.
    /// </summary>
    public class KeyUsageEvent
    {
        /// <summary>
        /// SHA-256 of the issued key's UTF-8 bytes, as 64 lower-case hex characters: the value sent as issued_credential.sha256 when the account was assessed. Each SDK's issued-credential helper computes it.
        /// </summary>
        [JsonProperty("key_sha256", Required = Newtonsoft.Json.Required.Always)]
        public string KeySha256 { get; set; } = default!;

        /// <summary>
        /// The client IP address the request came from, IPv4 or IPv6, as your server saw it. Stored only as a keyed digest and its country.
        /// </summary>
        [JsonProperty("ip", Required = Newtonsoft.Json.Required.Always)]
        public string Ip { get; set; } = default!;

        /// <summary>
        /// When the request was made.
        /// </summary>
        [JsonProperty("occurred_at", Required = Newtonsoft.Json.Required.Always)]
        public string OccurredAt { get; set; } = default!;

        /// <summary>
        /// The request's User-Agent header, at most 512 characters. Stored only as a keyed digest.
        /// </summary>
        [JsonProperty("user_agent", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? UserAgent { get; set; }

        /// <summary>
        /// The model or product the request used, such as gpt-4o-mini. Stored only as a keyed digest.
        /// </summary>
        [JsonProperty("model", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Model { get; set; }

        /// <summary>
        /// Input tokens the request consumed.
        /// </summary>
        [JsonProperty("input_tokens", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? InputTokens { get; set; }

        /// <summary>
        /// Output tokens the request produced.
        /// </summary>
        [JsonProperty("output_tokens", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? OutputTokens { get; set; }

        /// <summary>
        /// The share of requests your sampler records, when it records only some: 0.1 means this event stands for 10 requests. Request and token totals are weighted by its inverse. Distinct IP and country counts see only the events sent.
        /// </summary>
        [JsonProperty("sample_rate", Required = Newtonsoft.Json.Required.DisallowNull)]
        public double? SampleRate { get; set; } = 1D;
    }
}