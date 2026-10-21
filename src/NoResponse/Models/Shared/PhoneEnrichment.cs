#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// Facts about the number only: line type, carrier and country. The upstream provider, its scores and its verdicts stay internal.
    /// </summary>
    public class PhoneEnrichment
    {
        [JsonProperty("fields", Required = Newtonsoft.Json.Required.Always)]
        public PhoneEnrichmentFields Fields { get; set; } = new();
    }
}