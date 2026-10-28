#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Associate the SHA-256 of a high-entropy key issued by your service. Send the hash instead of the key. Repeating a hash returns its original association, including its retirement state. A hash associated with another account returns a conflict.
    /// </summary>
    public class CredentialRequest
    {
        [JsonProperty("sha256", Required = Newtonsoft.Json.Required.Always)]
        public string Sha256 { get; set; } = default!;
    }
}