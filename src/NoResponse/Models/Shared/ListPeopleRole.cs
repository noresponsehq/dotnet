#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    public enum ListPeopleRole
    {
        [JsonProperty("guest")] Guest,
        [JsonProperty("crew")] Crew,
        [JsonProperty("vendor")] Vendor,
        [JsonProperty("press")] Press,
        [JsonProperty("unstated")] Unstated,
    }

    public static class ListPeopleRoleExtension
    {
        public static string Value(this ListPeopleRole value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static ListPeopleRole ToEnum(this string value)
        {
            foreach (var field in typeof(ListPeopleRole).GetFields())
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

                    if (enumVal is ListPeopleRole)
                    {
                        return (ListPeopleRole)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum ListPeopleRole");
        }
    }
}