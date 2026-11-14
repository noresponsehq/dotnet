#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// How sure the screening is that the accounts it read are the person's. A finding on a possible match is at most moderate, on a probable match at most high.
    /// </summary>
    public enum PersonFlaggedMatchState
    {
        [JsonProperty("confirmed")] Confirmed,
        [JsonProperty("probable")] Probable,
        [JsonProperty("possible")] Possible,
        [JsonProperty("none_found")] NoneFound,
        [JsonProperty("inaccessible")] Inaccessible,
    }

    public static class PersonFlaggedMatchStateExtension
    {
        public static string Value(this PersonFlaggedMatchState value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static PersonFlaggedMatchState ToEnum(this string value)
        {
            foreach (var field in typeof(PersonFlaggedMatchState).GetFields())
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

                    if (enumVal is PersonFlaggedMatchState)
                    {
                        return (PersonFlaggedMatchState)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum PersonFlaggedMatchState");
        }
    }
}