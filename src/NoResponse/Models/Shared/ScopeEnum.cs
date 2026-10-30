#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// assessment requires one submission to satisfy the complete expression. account permits witnesses across retained submissions of one account and is supported only by POST /v1/accounts/search. No default: select scope explicitly.
    /// </summary>
    public enum ScopeEnum
    {
        [JsonProperty("assessment")] Assessment,
        [JsonProperty("account")] Account,
    }

    public static class ScopeEnumExtension
    {
        public static string Value(this ScopeEnum value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static ScopeEnum ToEnum(this string value)
        {
            foreach (var field in typeof(ScopeEnum).GetFields())
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

                    if (enumVal is ScopeEnum)
                    {
                        return (ScopeEnum)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum ScopeEnum");
        }
    }
}