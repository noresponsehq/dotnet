#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// List override changes among the newest 5000 matching accounts. These counts describe override inputs. Customer enforcement and eventual outcomes remain separate. The response reports whether the matching population was truncated.
    /// </summary>
    public class ListImpact
    {
        [JsonProperty("scope", Required = Newtonsoft.Json.Required.Always)]
        public string Scope { get; } = "current_list_override_inputs";

        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public ListImpactKind Kind { get; set; } = default!;

        [JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        public string Value { get; set; } = default!;

        [JsonProperty("action", Required = Newtonsoft.Json.Required.Always)]
        public Action Action { get; set; } = default!;

        [JsonProperty("remove", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Remove { get; set; }

        [JsonProperty("matched_accounts", Required = Newtonsoft.Json.Required.Always)]
        public long MatchedAccounts { get; set; } = default!;

        [JsonProperty("changed_overrides", Required = Newtonsoft.Json.Required.Always)]
        public long ChangedOverrides { get; set; } = default!;

        [JsonProperty("truncated", Required = Newtonsoft.Json.Required.Always)]
        public bool Truncated { get; set; } = default!;

        [JsonProperty("historical_population_known", Required = Newtonsoft.Json.Required.Always)]
        public bool HistoricalPopulationKnown { get; } = false;

        [JsonProperty("transitions", Required = Newtonsoft.Json.Required.Always)]
        public List<ListImpactTransition> Transitions { get; set; } = default!;
    }
}