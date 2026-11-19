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
        /// Each source asked, with what it answered and when. answered_at is absent while the source has not answered.
        /// </summary>
        [JsonProperty("answers", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<ScreeningLookupAnswer>? Answers { get; set; }

        /// <summary>
        /// What each source answered, whole, each answer once: every field of each account a connector found, and every column of each breach row the person's email or usernames matched, under the source's own names, and what each search a person started found, each match naming its search. Anyone who may read the person reads them: no extra permission applies, breach passwords and hashes included.
        /// </summary>
        [JsonProperty("records", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<Record>? Records { get; set; }

        /// <summary>
        /// For each search and source whose matched rows records leave out, how many: a breach search returns at most 20 rows of a table and 100 in all, from its first 64 tables by name. A search a person starts on one table of its source fetches the rest of that table, up to 100 rows. Absent when records hold every matched row.
        /// </summary>
        [JsonProperty("remaining", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<RecordRemaining>? Remaining { get; set; }

        /// <summary>
        /// When the lookups ran.
        /// </summary>
        [JsonProperty("looked_at", Required = Newtonsoft.Json.Required.Always)]
        public string LookedAt { get; set; } = default!;
    }
}