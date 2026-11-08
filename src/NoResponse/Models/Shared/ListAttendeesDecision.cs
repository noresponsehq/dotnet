#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    public enum ListAttendeesDecision
    {
        [JsonProperty("cleared")] Cleared,
        [JsonProperty("watch")] Watch,
        [JsonProperty("escalated")] Escalated,
        [JsonProperty("undecided")] Undecided,
    }

    public static class ListAttendeesDecisionExtension
    {
        public static string Value(this ListAttendeesDecision value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static ListAttendeesDecision ToEnum(this string value)
        {
            foreach (var field in typeof(ListAttendeesDecision).GetFields())
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

                    if (enumVal is ListAttendeesDecision)
                    {
                        return (ListAttendeesDecision)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum ListAttendeesDecision");
        }
    }
}