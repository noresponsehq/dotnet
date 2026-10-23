#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// The customer event an assessment is made for: signup (a new account), login (a returning sign-in) or purchase (a payment or credit grant). The account's risk is reported with the decision for this flow.
    /// </summary>
    public enum AssessmentFlow
    {
        [JsonProperty("signup")] Signup,
        [JsonProperty("login")] Login,
        [JsonProperty("purchase")] Purchase,
    }

    public static class AssessmentFlowExtension
    {
        public static string Value(this AssessmentFlow value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static AssessmentFlow ToEnum(this string value)
        {
            foreach (var field in typeof(AssessmentFlow).GetFields())
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

                    if (enumVal is AssessmentFlow)
                    {
                        return (AssessmentFlow)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum AssessmentFlow");
        }
    }
}