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
    /// An account the person is known to hold.
    /// </summary>
    public class Handle
    {
        /// <summary>
        /// A platform a screening can cover. github and scratch through flickr are each one read of the public profile a handle names, with no posts; chesscom also reads the profile's stats, clubs and last two months of games, for its activity and contacts, and through a signed-in session up to 100 of the player's friends, each a contact with the date the friendship began. synthetic is the deterministic connector's own platform, for exercising the product end to end with no network.
        /// </summary>
        [JsonProperty("platform", Required = Newtonsoft.Json.Required.Always)]
        public HandlePlatform Platform { get; set; } = default!;

        /// <summary>
        /// The account's handle, without a leading @.
        /// </summary>
        [JsonProperty("handle", Required = Newtonsoft.Json.Required.Always)]
        public string HandleValue { get; set; } = default!;
    }
}