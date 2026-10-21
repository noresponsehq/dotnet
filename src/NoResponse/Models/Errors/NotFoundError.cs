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

    public class NotFoundErrorPayload
    {
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public string Type { get; set; } = default!;

        [JsonProperty("title", Required = Newtonsoft.Json.Required.Always)]
        public string Title { get; set; } = default!;

        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public long Status { get; set; } = 404;

        [JsonProperty("detail", Required = Newtonsoft.Json.Required.Always)]
        public string Detail { get; set; } = default!;

        [JsonProperty("instance", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Instance { get; set; }

        /// <summary>
        /// Machine codes an HTTP 404 answer can carry.
        /// </summary>
        [JsonProperty("code", Required = Newtonsoft.Json.Required.Always)]
        public NotFoundErrorCode Code { get; set; } = default!;

        [JsonProperty("request_id", Required = Newtonsoft.Json.Required.Always)]
        public string RequestId { get; set; } = default!;

        [JsonProperty("invalid_params", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<ProblemInvalidParam>? InvalidParams { get; set; }
    }

    /// <summary>
    /// HTTP 404 problem document.
    /// </summary>
    public class NotFoundError : NoResponseException
    {
        /// <summary>
        ///  The original data that was passed to this exception.
        /// </summary>
        public NotFoundErrorPayload Payload { get; }

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use NotFoundError.Payload.Type instead.")]
        public string Type { get; set; } = default!;

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use NotFoundError.Payload.Title instead.")]
        public string Title { get; set; } = default!;

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use NotFoundError.Payload.Status instead.")]
        public long Status { get; set; } = default!;

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use NotFoundError.Payload.Detail instead.")]
        public string Detail { get; set; } = default!;

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use NotFoundError.Payload.Instance instead.")]
        public string? Instance { get; set; }

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use NotFoundError.Payload.Code instead.")]
        public NotFoundErrorCode Code { get; set; } = default!;

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use NotFoundError.Payload.RequestId instead.")]
        public string RequestId { get; set; } = default!;

        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible. Use NotFoundError.Payload.InvalidParams instead.")]
        public List<ProblemInvalidParam>? InvalidParams { get; set; }

        public NotFoundError(
            NotFoundErrorPayload payload,
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