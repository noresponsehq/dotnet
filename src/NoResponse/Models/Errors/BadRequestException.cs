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

    public class BadRequestExceptionPayload
    {
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public string Type { get; set; } = default!;

        [JsonProperty("title", Required = Newtonsoft.Json.Required.Always)]
        public string Title { get; set; } = default!;

        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public long Status { get; set; } = 400;

        [JsonProperty("detail", Required = Newtonsoft.Json.Required.Always)]
        public string Detail { get; set; } = default!;

        [JsonProperty("instance", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Instance { get; set; }

        /// <summary>
        /// Machine codes an HTTP 400 answer can carry.
        /// </summary>
        [JsonProperty("code", Required = Newtonsoft.Json.Required.Always)]
        public BadRequestErrorCode Code { get; set; } = default!;

        [JsonProperty("request_id", Required = Newtonsoft.Json.Required.Always)]
        public string RequestId { get; set; } = default!;

        [JsonProperty("invalid_params", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<ProblemInvalidParam>? InvalidParams { get; set; }
    }

    /// <summary>
    /// HTTP 400 problem document.
    /// </summary>
    public class BadRequestException : NoResponseException
    {
        /// <summary>
        ///  The original data that was passed to this exception.
        /// </summary>
        public BadRequestExceptionPayload Payload { get; }

        public BadRequestException(
            BadRequestExceptionPayload payload,
            HttpResponseMessage rawResponse,
            string body
        ) : base("API error occurred", rawResponse, body)
        {
            Payload = payload;
        }
    }
}