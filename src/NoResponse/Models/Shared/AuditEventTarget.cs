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

    public class AuditEventTarget
    {
        /// <summary>
        /// The kind of resource the change acted on.
        /// </summary>
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public AuditEventTargetType Type { get; set; } = default!;

        /// <summary>
        /// The resource's id, when it has one.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Id { get; set; }
    }
}