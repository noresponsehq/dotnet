#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// api (the default) searches the assessments your integration sent. lookup searches the assessments your team ran by hand on the console's Lookup page. The API sets an assessment's origin from the credential that sent it. A lookup is not in any other list, count, webhook event or account.
    /// </summary>
    public enum Origin
    {
        [JsonProperty("api")] Api,
        [JsonProperty("lookup")] Lookup,
    }

    public static class OriginExtension
    {
        public static string Value(this Origin value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static Origin ToEnum(this string value)
        {
            foreach (var field in typeof(Origin).GetFields())
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

                    if (enumVal is Origin)
                    {
                        return (Origin)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum Origin");
        }
    }
}