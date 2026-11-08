#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// How sure the screening is that the accounts it read are the attendee's. A finding on a possible match is at most moderate, on a probable match at most high.
    /// </summary>
    public enum AttendeeFlaggedMatchState
    {
        [JsonProperty("confirmed")] Confirmed,
        [JsonProperty("probable")] Probable,
        [JsonProperty("possible")] Possible,
        [JsonProperty("none_found")] NoneFound,
        [JsonProperty("inaccessible")] Inaccessible,
    }

    public static class AttendeeFlaggedMatchStateExtension
    {
        public static string Value(this AttendeeFlaggedMatchState value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static AttendeeFlaggedMatchState ToEnum(this string value)
        {
            foreach (var field in typeof(AttendeeFlaggedMatchState).GetFields())
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

                    if (enumVal is AttendeeFlaggedMatchState)
                    {
                        return (AttendeeFlaggedMatchState)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum AttendeeFlaggedMatchState");
        }
    }
}