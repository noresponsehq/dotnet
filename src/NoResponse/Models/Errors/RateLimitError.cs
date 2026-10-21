#nullable enable
namespace NoResponse.Models.Errors
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    public class RateLimitErrorPayload
    {
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public string Type { get; set; } = default!;

        [JsonProperty("title", Required = Newtonsoft.Json.Required.Always)]
        public string Title { get; set; } = default!;

        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public long Status { get; set; } = 429;

        [JsonProperty("detail", Required = Newtonsoft.Json.Required.Always)]
        public string Detail { get; set; } = default!;

        [JsonProperty("instance", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Instance { get; set; }

        /// <summary>
        /// Machine codes an HTTP 429 answer can carry.
        /// </summary>
        [JsonProperty("code", Required = Newtonsoft.Json.Required.Always)]
        public RateLimitErrorCode Code { get; set; } = default!;

        [JsonProperty("request_id", Required = Newtonsoft.Json.Required.Always)]
        public string RequestId { get; set; } = default!;

        [JsonProperty("invalid_params", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<ProblemInvalidParam>? InvalidParams { get; set; }
    }

    /// <summary>
    /// HTTP 429 problem document.
    /// </summary>
    public class RateLimitError : NoResponseException
    {
        /// <summary>
        ///  The original data that was passed to this exception.
        /// </summary>
        public RateLimitErrorPayload Payload { get; }

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use RateLimitError.Payload.Type instead.")]
        public string Type { get; set; } = default!;

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use RateLimitError.Payload.Title instead.")]
        public string Title { get; set; } = default!;

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use RateLimitError.Payload.Status instead.")]
        public long Status { get; set; } = default!;

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use RateLimitError.Payload.Detail instead.")]
        public string Detail { get; set; } = default!;

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use RateLimitError.Payload.Instance instead.")]
        public string? Instance { get; set; }

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use RateLimitError.Payload.Code instead.")]
        public RateLimitErrorCode Code { get; set; } = default!;

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use RateLimitError.Payload.RequestId instead.")]
        public string RequestId { get; set; } = default!;

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use RateLimitError.Payload.InvalidParams instead.")]
        public List<ProblemInvalidParam>? InvalidParams { get; set; }

        public RateLimitError(
            RateLimitErrorPayload payload,
            HttpResponseMessage rawResponse,
            string body
        ) : base("API error occurred", rawResponse, body)
        {
            Payload = payload;

#pragma warning disable CS0618
            Type = payload.Type;
            Title = payload.Title;
            Status = payload.Status;
            Detail = payload.Detail;
            Instance = payload.Instance;
            Code = payload.Code;
            RequestId = payload.RequestId;
            InvalidParams = payload.InvalidParams;
#pragma warning restore CS0618
        }
    }
}