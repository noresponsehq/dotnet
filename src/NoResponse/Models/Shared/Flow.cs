#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// The provider flow the assessment gates. Use the top-level flow instead; this member is ignored when the top-level flow is present. Absent means signup.
    /// </summary>
    public enum Flow
    {
        [JsonProperty("signup")] Signup,
        [JsonProperty("login")] Login,
        [JsonProperty("recovery")] Recovery,
        [JsonProperty("profile_change")] ProfileChange,
    }

    public static class FlowExtension
    {
        public static string Value(this Flow value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static Flow ToEnum(this string value)
        {
            foreach (var field in typeof(Flow).GetFields())
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

                    if (enumVal is Flow)
                    {
                        return (Flow)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum Flow");
        }
    }
}