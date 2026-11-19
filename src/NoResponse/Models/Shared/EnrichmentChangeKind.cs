#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    public enum EnrichmentChangeKind
    {
        [JsonProperty("username")] Username,
        [JsonProperty("email")] Email,
        [JsonProperty("phone")] Phone,
        [JsonProperty("person_name")] PersonName,
    }

    public static class EnrichmentChangeKindExtension
    {
        public static string Value(this EnrichmentChangeKind value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static EnrichmentChangeKind ToEnum(this string value)
        {
            foreach (var field in typeof(EnrichmentChangeKind).GetFields())
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

                    if (enumVal is EnrichmentChangeKind)
                    {
                        return (EnrichmentChangeKind)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum EnrichmentChangeKind");
        }
    }
}