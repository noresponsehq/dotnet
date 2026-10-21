
#nullable enable
namespace NoResponse.Models.Errors
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// Raised if the response data could not be deserialized into the expected model.
    /// </summary>
    public class ResponseValidationException : NoResponseException
    {

        public ResponseValidationException(
            string message,
            HttpResponseMessage rawResponse,
            string body,
            Exception innerException
        ) : base(message, rawResponse, body, innerException)
        {
        }
    }
}
