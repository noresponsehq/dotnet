
#nullable enable
namespace NoResponse.Models.Errors
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// Default API Exception.
    /// </summary>
    public class APIStatusException : NoResponseException
    {
        /// <summary>
        /// Default API exception.
        /// </summary>
        public APIStatusException(
            string message,
            HttpResponseMessage rawResponse,
            string body
        ) : base(message, rawResponse, body)
        {
        }
    }
}
