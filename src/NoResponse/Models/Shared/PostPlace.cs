#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// A place a post names, as the platform shows it.
    /// </summary>
    public class PostPlace
    {
        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// The platform's own id of the place.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Id { get; set; }

        [JsonProperty("address", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Address { get; set; }

        [JsonProperty("city", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? City { get; set; }

        [JsonProperty("latitude", Required = Newtonsoft.Json.Required.DisallowNull)]
        public double? Latitude { get; set; }

        [JsonProperty("longitude", Required = Newtonsoft.Json.Required.DisallowNull)]
        public double? Longitude { get; set; }
    }
}