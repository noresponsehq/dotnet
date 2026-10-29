#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    public enum MemberUpdateRequestEnvironment
    {
        [JsonProperty("live")] Live,
        [JsonProperty("test")] Test,
    }

    public static class MemberUpdateRequestEnvironmentExtension
    {
        public static string Value(this MemberUpdateRequestEnvironment value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static MemberUpdateRequestEnvironment ToEnum(this string value)
        {
            foreach (var field in typeof(MemberUpdateRequestEnvironment).GetFields())
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

                    if (enumVal is MemberUpdateRequestEnvironment)
                    {
                        return (MemberUpdateRequestEnvironment)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum MemberUpdateRequestEnvironment");
        }
    }
}