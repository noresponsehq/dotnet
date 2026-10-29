#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Counts only; a zero denominator means no eligible retained records and does not imply a zero rate.
    /// </summary>
    public class Coverage
    {
        [JsonProperty("numerator", Required = Newtonsoft.Json.Required.Always)]
        public long Numerator { get; set; } = default!;

        [JsonProperty("denominator", Required = Newtonsoft.Json.Required.Always)]
        public long Denominator { get; set; } = default!;
    }
}