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

    public class ReviewUpdateRequest
    {
        [JsonProperty("expected_version", Required = Newtonsoft.Json.Required.Always)]
        public long ExpectedVersion { get; set; } = default!;

        [JsonProperty("status", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ReviewUpdateRequestStatus? Status { get; set; }

        [JsonProperty("priority", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Priority? Priority { get; set; }

        [JsonProperty("assignee", Required = Newtonsoft.Json.Required.Default, NullValueHandling = NullValueHandling.Include)]
        public OptionalNullable<string?> Assignee { get; set; }
        public bool ShouldSerializeAssignee() => Assignee.IsSet;
    }
}