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

    public class Filter
    {
        /// <summary>
        /// Only reviews of this exact account in the authenticated workspace and environment. Applied before pagination.
        /// </summary>
        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? AccountId { get; set; }

        [JsonProperty("status", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ReviewStatus? Status { get; set; }

        [JsonProperty("priority", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Priority? Priority { get; set; }

        [JsonProperty("assignee", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Assignee { get; set; }

        [JsonProperty("unassigned", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Unassigned { get; set; }

        [JsonProperty("assignee_principal_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? AssigneePrincipalId { get; set; }
    }
}