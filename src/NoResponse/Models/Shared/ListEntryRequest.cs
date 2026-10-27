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
    /// POST /v1/workspaces/{workspace_id}/lists/{environment} body. Adds one entry to the environment's list and, in the same change, re-decides every account it matches: each sends account.risk_changed when a decision changes. Needs the admin or owner role, or a management key of the environment.
    /// </summary>
    public class ListEntryRequest
    {
        /// <summary>
        /// email matches one address, whatever the case of its local part. email_domain matches every address of the registrable domain and of its subdomains.
        /// </summary>
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public ListEntryKind Kind { get; set; } = default!;

        /// <summary>
        /// An email address for kind email, written as in an assessment request; an LDH domain (A-label for an internationalised one) such as example.com for kind email_domain, which must sit below a public suffix (co.uk alone is refused with 400). NoResponse stores it in canonical form and answers that form.
        /// </summary>
        [JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        public string Value { get; set; } = default!;

        /// <summary>
        /// block decides block for the accounts the entry matches; allow decides allow. Block wins when both match.
        /// </summary>
        [JsonProperty("action", Required = Newtonsoft.Json.Required.Always)]
        public ListEntryAction Action { get; set; } = default!;

        /// <summary>
        /// Why the entry exists, for the people who manage the list. It is shown with the entry and decides nothing.
        /// </summary>
        [JsonProperty("note", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Note { get; set; }
    }
}