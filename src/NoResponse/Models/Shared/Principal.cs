#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// A person the group protects.
    /// </summary>
    public class Principal
    {
        /// <summary>
        /// The principal's name.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// Other names the principal goes by, at most 10.
        /// </summary>
        [JsonProperty("aliases", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Aliases { get; set; }
    }
}