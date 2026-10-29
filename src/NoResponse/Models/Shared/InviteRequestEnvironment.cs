#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    public enum InviteRequestEnvironment
    {
        [JsonProperty("live")] Live,
        [JsonProperty("test")] Test,
    }

    public static class InviteRequestEnvironmentExtension
    {
        public static string Value(this InviteRequestEnvironment value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static InviteRequestEnvironment ToEnum(this string value)
        {
            foreach (var field in typeof(InviteRequestEnvironment).GetFields())
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

                    if (enumVal is InviteRequestEnvironment)
                    {
                        return (InviteRequestEnvironment)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum InviteRequestEnvironment");
        }
    }
}