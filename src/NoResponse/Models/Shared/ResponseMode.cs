#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// standard returns the consolidated risk response; detailed adds merged provider-neutral facts when the key has detailed scope. There is no raw mode.
    /// </summary>
    public enum ResponseMode
    {
        [JsonProperty("standard")] Standard,
        [JsonProperty("detailed")] Detailed,
    }

    public static class ResponseModeExtension
    {
        public static string Value(this ResponseMode value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static ResponseMode ToEnum(this string value)
        {
            foreach (var field in typeof(ResponseMode).GetFields())
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

                    if (enumVal is ResponseMode)
                    {
                        return (ResponseMode)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum ResponseMode");
        }
    }
}