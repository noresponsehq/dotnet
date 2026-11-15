#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    public enum InvestigationCompletedStatus
    {
        [JsonProperty("completed")] Completed,
        [JsonProperty("partial")] Partial,
        [JsonProperty("failed")] Failed,
        [JsonProperty("cancelled")] Cancelled,
    }

    public static class InvestigationCompletedStatusExtension
    {
        public static string Value(this InvestigationCompletedStatus value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static InvestigationCompletedStatus ToEnum(this string value)
        {
            foreach (var field in typeof(InvestigationCompletedStatus).GetFields())
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

                    if (enumVal is InvestigationCompletedStatus)
                    {
                        return (InvestigationCompletedStatus)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum InvestigationCompletedStatus");
        }
    }
}