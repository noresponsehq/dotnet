#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class AuditSettingsRequest
    {
        [JsonProperty("retention_days", Required = Newtonsoft.Json.Required.Always)]
        public long RetentionDays { get; set; } = default!;
    }
}