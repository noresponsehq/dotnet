#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// The list action captured at the first answer. Requires revision first. Older answers without captured list state do not match, including none.
    /// </summary>
    public enum ListMatch
    {
        [JsonProperty("allow")] Allow,
        [JsonProperty("block")] Block,
        [JsonProperty("none")] None,
    }

    public static class ListMatchExtension
    {
        public static string Value(this ListMatch value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static ListMatch ToEnum(this string value)
        {
            foreach (var field in typeof(ListMatch).GetFields())
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

                    if (enumVal is ListMatch)
                    {
                        return (ListMatch)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum ListMatch");
        }
    }
}