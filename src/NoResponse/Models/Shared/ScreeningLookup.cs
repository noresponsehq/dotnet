#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// What the signup check's lookups found for a person: account presence for the email, phone and usernames (the person's handles), the phone lookup, the email domain's registration and the breach search. Context and coverage for the reviewer, not a finding: nothing here sets a band or a match state.
    /// </summary>
    public class ScreeningLookup
    {
        /// <summary>
        /// Each lane each identifier was looked up in, identifiers in the order email, phone, username and lanes in the order accounts, phone, breach, domain. Every lane the person's identifiers are looked up in is listed: one that no source answered, or that no source could be asked in, is not_covered. Empty when the person has no email, phone or handle to look up.
        /// </summary>
        [JsonProperty("checks", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningLookupCheck> Checks { get; set; } = default!;

        /// <summary>
        /// A few typed facts per identifier: an assessment's risk.summary, and a screened person's lookup summary. Every fact is optional: absent means the evidence did not answer and makes no claim, while false or 0 is a definite answer.
        /// </summary>
        [JsonProperty("summary", Required = Newtonsoft.Json.Required.DisallowNull)]
        public RiskSummary? Summary { get; set; }

        /// <summary>
        /// Merged observations behind the summary, served only to a caller who may read evidence: no provider, source, origin, raw breach record or score.
        /// </summary>
        [JsonProperty("details", Required = Newtonsoft.Json.Required.DisallowNull)]
        public RiskDetails? Details { get; set; }

        /// <summary>
        /// When the lookups ran.
        /// </summary>
        [JsonProperty("looked_at", Required = Newtonsoft.Json.Required.Always)]
        public string LookedAt { get; set; } = default!;
    }
}