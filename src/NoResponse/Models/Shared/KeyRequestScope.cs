#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    public enum KeyRequestScope
    {
        [JsonProperty("assess")] Assess,
        [JsonProperty("history")] History,
        [JsonProperty("delete")] Delete,
        [JsonProperty("webhooks")] Webhooks,
        [JsonProperty("detailed")] Detailed,
        [JsonProperty("integrations")] Integrations,
        [JsonProperty("management")] Management,
    }

    public static class KeyRequestScopeExtension
    {
        public static string Value(this KeyRequestScope value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static KeyRequestScope ToEnum(this string value)
        {
            foreach (var field in typeof(KeyRequestScope).GetFields())
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

                    if (enumVal is KeyRequestScope)
                    {
                        return (KeyRequestScope)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum KeyRequestScope");
        }
    }
}