#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Optional. When the signup gate issues an API key to the new account, send SHA-256 of that key, so later usage of the key, or a leaked copy, ties back to this assessment. Each SDK has a helper that computes it. Send only the hash: customer keys are high-entropy, so the hash reveals nothing about them. The assessment stores it, and no response, webhook or log carries it.
    /// </summary>
    public class AssessmentRequestIssuedCredential
    {
        /// <summary>
        /// SHA-256 of the issued key's UTF-8 bytes, as 64 lower-case hex characters.
        /// </summary>
        [JsonProperty("sha256", Required = Newtonsoft.Json.Required.Always)]
        public string Sha256 { get; set; } = default!;
    }
}