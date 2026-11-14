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
    /// An account the screening matched to the person, how it was matched, and how much of it was read.
    /// </summary>
    public class ScreeningProfile
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// A platform a screening can cover. synthetic is the deterministic connector's own platform, for exercising the product end to end with no network.
        /// </summary>
        [JsonProperty("platform", Required = Newtonsoft.Json.Required.Always)]
        public HandlePlatform Platform { get; set; } = default!;

        [JsonProperty("handle", Required = Newtonsoft.Json.Required.Always)]
        public string Handle { get; set; } = default!;

        [JsonProperty("url", Required = Newtonsoft.Json.Required.Always)]
        public string Url { get; set; } = default!;

        [JsonProperty("display_name", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? DisplayName { get; set; }

        [JsonProperty("bio", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Bio { get; set; }

        [JsonProperty("avatar_url", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? AvatarUrl { get; set; }

        [JsonProperty("followers", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Followers { get; set; }

        [JsonProperty("account_created_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? AccountCreatedAt { get; set; }

        /// <summary>
        /// public profiles were read; a private or locked one exists but could not be read.
        /// </summary>
        [JsonProperty("access", Required = Newtonsoft.Json.Required.Always)]
        public Access Access { get; set; } = default!;

        /// <summary>
        /// How a profile was matched to the person: the state its evidence earns, and the evidence.
        /// </summary>
        [JsonProperty("match", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningMatch Match { get; set; } = new();

        /// <summary>
        /// The resolver, collector or analyser that produced a record, by its catalogue name.
        /// </summary>
        [JsonProperty("collector", Required = Newtonsoft.Json.Required.Always)]
        public string Collector { get; set; } = default!;

        [JsonProperty("items_read", Required = Newtonsoft.Json.Required.Always)]
        public long ItemsRead { get; set; } = default!;

        /// <summary>
        /// Why fewer posts were read than the screening's depth asks for while the account holds more. platform_limit: the platform shows the screening no more of them. page_limit: the pages a screening reads ran out first. rate_limited or unavailable: a later page was held back or failed; the posts before it are kept and the person is partial. Absent when the account was read to its end or to the depth.
        /// </summary>
        [JsonProperty("posts_stopped", Required = Newtonsoft.Json.Required.DisallowNull)]
        public PostsStopped? PostsStopped { get; set; }

        /// <summary>
        /// The oldest item read.
        /// </summary>
        [JsonProperty("period_from", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? PeriodFrom { get; set; }

        /// <summary>
        /// The newest item read.
        /// </summary>
        [JsonProperty("period_to", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? PeriodTo { get; set; }

        [JsonProperty("collected_at", Required = Newtonsoft.Json.Required.Always)]
        public string CollectedAt { get; set; } = default!;

        /// <summary>
        /// One answer from one source: where it came from, what found it and every field it holds.
        /// </summary>
        [JsonProperty("record", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Record? Record { get; set; }
    }
}