#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class InvestigationCompleted
    {
        [JsonProperty("investigation_id", Required = Newtonsoft.Json.Required.Always)]
        public string InvestigationId { get; set; } = default!;

        [JsonProperty("person_id", Required = Newtonsoft.Json.Required.Always)]
        public string PersonId { get; set; } = default!;

        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public InvestigationCompletedStatus Status { get; set; } = default!;

        [JsonProperty("error_code", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ErrorCode { get; set; }

        [JsonProperty("finished_at", Required = Newtonsoft.Json.Required.Always)]
        public string FinishedAt { get; set; } = default!;
    }
}