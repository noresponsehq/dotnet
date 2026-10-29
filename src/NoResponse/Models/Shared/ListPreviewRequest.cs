#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// Choose an addition or a removal. The preview performs no list, account, audit, outcome or outbox write.
    /// </summary>
    public class ListPreviewRequest
    {
        /// <summary>
        /// POST /v1/workspaces/{workspace_id}/lists/{environment} body. Adds one entry to the environment's list and, in the same change, re-decides every account it matches: each sends account.risk_changed when a decision changes. Needs the admin or owner role, or a management key of the environment.
        /// </summary>
        [JsonProperty("add", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ListEntryRequest? Add { get; set; }

        [JsonProperty("remove", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Remove { get; set; }
    }
}