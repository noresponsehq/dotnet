#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// Where this context was derived. Backend is the trusted integration path; browser values are treated as weak hints. provider_hook is a hosted provider hook (an Auth0 or WorkOS Action) calling at hook_point; it needs hook_point and verified false, and its authentication facts are not scored.
    /// </summary>
    public enum Transport
    {
        [JsonProperty("backend")] Backend,
        [JsonProperty("browser")] Browser,
        [JsonProperty("provider_hook")] ProviderHook,
    }

    public static class TransportExtension
    {
        public static string Value(this Transport value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static Transport ToEnum(this string value)
        {
            foreach (var field in typeof(Transport).GetFields())
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

                    if (enumVal is Transport)
                    {
                        return (Transport)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum Transport");
        }
    }
}