#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// The environment whose endpoints receive this event: live, or test for the workspace's test environment.
    /// </summary>
    public enum Environment
    {
        [JsonProperty("live")] Live,
        [JsonProperty("test")] Test,
    }

    public static class EnvironmentExtension
    {
        public static string Value(this Environment value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static Environment ToEnum(this string value)
        {
            foreach (var field in typeof(Environment).GetFields())
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

                    if (enumVal is Environment)
                    {
                        return (Environment)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum Environment");
        }
    }
}