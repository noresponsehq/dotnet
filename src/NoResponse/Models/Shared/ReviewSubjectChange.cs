#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// The link and resulting review version.
    /// </summary>
    public class ReviewSubjectChange
    {
        [JsonProperty("review_version", Required = Newtonsoft.Json.Required.Always)]
        public long ReviewVersion { get; set; } = default!;

        /// <summary>
        /// A tenant-local investigation link. risk_version is the account risk version captured when the link was added. It does not assert a relationship or change feedback or enforcement.
        /// </summary>
        [JsonProperty("subject", Required = Newtonsoft.Json.Required.Always)]
        public ReviewSubject Subject { get; set; } = new();
    }
}