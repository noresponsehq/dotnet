#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class ServiceActor
    {
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public string Type { get; } = "service";

        /// <summary>
        /// The NoResponse service that made the change with no person acting, such as replenisher, which replaces dead platform sessions in NoResponse's own workspace.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public string Name { get; set; } = default!;
    }
}