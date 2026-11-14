#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// completed reached every person; partial left some not fully covered (partial_coverage); failed covered nobody.
    /// </summary>
    public enum ScreeningCompletedStatus
    {
        [JsonProperty("completed")] Completed,
        [JsonProperty("partial")] Partial,
        [JsonProperty("failed")] Failed,
    }

    public static class ScreeningCompletedStatusExtension
    {
        public static string Value(this ScreeningCompletedStatus value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static ScreeningCompletedStatus ToEnum(this string value)
        {
            foreach (var field in typeof(ScreeningCompletedStatus).GetFields())
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

                    if (enumVal is ScreeningCompletedStatus)
                    {
                        return (ScreeningCompletedStatus)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum ScreeningCompletedStatus");
        }
    }
}