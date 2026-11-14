#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    public enum ListPeopleDecision
    {
        [JsonProperty("cleared")] Cleared,
        [JsonProperty("watch")] Watch,
        [JsonProperty("escalated")] Escalated,
        [JsonProperty("undecided")] Undecided,
    }

    public static class ListPeopleDecisionExtension
    {
        public static string Value(this ListPeopleDecision value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static ListPeopleDecision ToEnum(this string value)
        {
            foreach (var field in typeof(ListPeopleDecision).GetFields())
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

                    if (enumVal is ListPeopleDecision)
                    {
                        return (ListPeopleDecision)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum ListPeopleDecision");
        }
    }
}