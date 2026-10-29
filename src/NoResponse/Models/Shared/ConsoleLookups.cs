#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// The live environment's daily cap on assessments members make through the console (the playground), as spent on the current UTC day. Each console submission with a new Idempotency-Key spends one lookup; a replay spends nothing, and API keys and the test environment spend none. When used reaches limit, the next lookup answers 429 lookup_limit_reached until resets_at. The count is the one the cap enforces.
    /// </summary>
    public class ConsoleLookups
    {
        /// <summary>
        /// Lookups spent since the last reset.
        /// </summary>
        [JsonProperty("used", Required = Newtonsoft.Json.Required.Always)]
        public long Used { get; set; } = default!;

        /// <summary>
        /// Lookups allowed per UTC day.
        /// </summary>
        [JsonProperty("limit", Required = Newtonsoft.Json.Required.Always)]
        public long Limit { get; set; } = default!;

        /// <summary>
        /// The next reset: the coming midnight UTC, when used returns to 0.
        /// </summary>
        [JsonProperty("resets_at", Required = Newtonsoft.Json.Required.Always)]
        public string ResetsAt { get; set; } = default!;
    }
}