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

    public class ErasureReceiptTarget
    {
        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public ErasureReceiptTargetName Name { get; set; } = default!;

        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public ErasureReceiptTargetStatus Status { get; set; } = default!;

        [JsonProperty("completed_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? CompletedAt { get; set; }
    }
}