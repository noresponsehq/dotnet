#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// The decision of the assessment's latest stored revision: block when its stored risk.block is true, otherwise its recommendation. This is not the account's current decision or held-admission state. Combined with recommendation using AND.
    /// </summary>
    public enum Decision
    {
        [JsonProperty("allow")] Allow,
        [JsonProperty("step_up")] StepUp,
        [JsonProperty("review")] Review,
        [JsonProperty("block")] Block,
    }

    public static class DecisionExtension
    {
        public static string Value(this Decision value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static Decision ToEnum(this string value)
        {
            foreach (var field in typeof(Decision).GetFields())
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

                    if (enumVal is Decision)
                    {
                        return (Decision)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum Decision");
        }
    }
}