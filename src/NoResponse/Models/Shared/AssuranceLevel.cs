#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// Provider or OIDC assurance level, when the customer has mapped one.
    /// </summary>
    public enum AssuranceLevel
    {
        [JsonProperty("unknown")] Unknown,
        [JsonProperty("low")] Low,
        [JsonProperty("medium")] Medium,
        [JsonProperty("high")] High,
    }

    public static class AssuranceLevelExtension
    {
        public static string Value(this AssuranceLevel value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static AssuranceLevel ToEnum(this string value)
        {
            foreach (var field in typeof(AssuranceLevel).GetFields())
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

                    if (enumVal is AssuranceLevel)
                    {
                        return (AssuranceLevel)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum AssuranceLevel");
        }
    }
}