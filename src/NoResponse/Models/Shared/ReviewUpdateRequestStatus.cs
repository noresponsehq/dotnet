#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    public enum ReviewUpdateRequestStatus
    {
        [JsonProperty("open")] Open,
        [JsonProperty("in_progress")] InProgress,
    }

    public static class ReviewUpdateRequestStatusExtension
    {
        public static string Value(this ReviewUpdateRequestStatus value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static ReviewUpdateRequestStatus ToEnum(this string value)
        {
            foreach (var field in typeof(ReviewUpdateRequestStatus).GetFields())
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

                    if (enumVal is ReviewUpdateRequestStatus)
                    {
                        return (ReviewUpdateRequestStatus)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum ReviewUpdateRequestStatus");
        }
    }
}