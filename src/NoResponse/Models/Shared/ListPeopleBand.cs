#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    public enum ListPeopleBand
    {
        [JsonProperty("critical")] Critical,
        [JsonProperty("high")] High,
        [JsonProperty("moderate")] Moderate,
        [JsonProperty("low")] Low,
        [JsonProperty("info")] Info,
        [JsonProperty("none")] None,
    }

    public static class ListPeopleBandExtension
    {
        public static string Value(this ListPeopleBand value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static ListPeopleBand ToEnum(this string value)
        {
            foreach (var field in typeof(ListPeopleBand).GetFields())
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

                    if (enumVal is ListPeopleBand)
                    {
                        return (ListPeopleBand)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum ListPeopleBand");
        }
    }
}