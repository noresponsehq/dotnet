#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    public enum AssessmentAggregateRequestMetric
    {
        [JsonProperty("count")] Count,
        [JsonProperty("score_histogram")] ScoreHistogram,
        [JsonProperty("score_outcomes")] ScoreOutcomes,
        [JsonProperty("late_flips")] LateFlips,
    }

    public static class AssessmentAggregateRequestMetricExtension
    {
        public static string Value(this AssessmentAggregateRequestMetric value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static AssessmentAggregateRequestMetric ToEnum(this string value)
        {
            foreach (var field in typeof(AssessmentAggregateRequestMetric).GetFields())
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

                    if (enumVal is AssessmentAggregateRequestMetric)
                    {
                        return (AssessmentAggregateRequestMetric)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum AssessmentAggregateRequestMetric");
        }
    }
}