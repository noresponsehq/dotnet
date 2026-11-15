#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    public class WorkspacePersonRequest
    {
        /// <summary>
        /// Your reference for this person within the workspace environment. Only an explicit reference or person_id can reuse an existing person.
        /// </summary>
        [JsonProperty("external_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExternalId { get; set; }

        /// <summary>
        /// The person's full name.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Name { get; set; }

        /// <summary>
        /// The person's email.
        /// </summary>
        [JsonProperty("email", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Email { get; set; }

        /// <summary>
        /// The person's phone in E.164 form.
        /// </summary>
        [JsonProperty("phone", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Phone { get; set; }

        /// <summary>
        /// Where the person works.
        /// </summary>
        [JsonProperty("company", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Company { get; set; }

        /// <summary>
        /// Where the person lives.
        /// </summary>
        [JsonProperty("city", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? City { get; set; }

        [JsonProperty("handles", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<Handle>? Handles { get; set; }

        [JsonProperty("person_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? PersonId { get; set; }
    }
}