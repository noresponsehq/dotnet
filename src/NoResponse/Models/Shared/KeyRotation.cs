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
    /// The 201 answer of POST /v1/keys/rotate: the new key, shown once, and when the key that was presented stops working. The new key has the scopes and the environment of the presented key.
    /// </summary>
    public class KeyRotation
    {
        /// <summary>
        /// The new credential, nrk_&lt;mode&gt;_&lt;key id&gt;_&lt;secret&gt;, in the environment of the presented key: nrk_live_ or nrk_test_. It is shown only in this answer; store it now.
        /// </summary>
        [JsonProperty("key", Required = Newtonsoft.Json.Required.Always)]
        public string Key { get; set; } = default!;

        /// <summary>
        /// The public id of the new key.
        /// </summary>
        [JsonProperty("key_id", Required = Newtonsoft.Json.Required.Always)]
        public string KeyId { get; set; } = default!;

        /// <summary>
        /// The scopes of the new key: those of the presented key.
        /// </summary>
        [JsonProperty("scopes", Required = Newtonsoft.Json.Required.Always)]
        public List<KeyRotationScope> Scopes { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        /// <summary>
        /// The id of the key that signed this request.
        /// </summary>
        [JsonProperty("previous_key_id", Required = Newtonsoft.Json.Required.Always)]
        public string PreviousKeyId { get; set; } = default!;

        /// <summary>
        /// When the previous key stops authenticating. Until then both keys work.
        /// </summary>
        [JsonProperty("previous_key_expires_at", Required = Newtonsoft.Json.Required.Always)]
        public string PreviousKeyExpiresAt { get; set; } = default!;
    }
}