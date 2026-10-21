#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// Client platform. Closed vocabulary keeps integrations stable.
    /// </summary>
    public enum Platform
    {
        [JsonProperty("web")] Web,
        [JsonProperty("ios")] Ios,
        [JsonProperty("android")] Android,
        [JsonProperty("other")] Other,
    }

    public static class PlatformExtension
    {
        public static string Value(this Platform value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static Platform ToEnum(this string value)
        {
            foreach (var field in typeof(Platform).GetFields())
            {
                var attributes = field.GetCustomAttributes(typeof(JsonPropertyAttribute), false);
                if (attributes.Length == 0)
                {
                    continue;
                }

                var attribute = attributes[0] as JsonPropertyAttribute;
                if (attribute != null && attribute.PropertyName == value)
                {
                    var enumVal = field.GetValue(null);

                    if (enumVal is Platform)
                    {
                        return (Platform)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum Platform");
        }
    }
}