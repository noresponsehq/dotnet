#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    public enum KeyAccessRequestDeniedAction
    {
        [JsonProperty("evidence.read")] EvidenceRead,
        [JsonProperty("exports.read")] ExportsRead,
        [JsonProperty("policy.publish")] PolicyPublish,
    }

    public static class KeyAccessRequestDeniedActionExtension
    {
        public static string Value(this KeyAccessRequestDeniedAction value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static KeyAccessRequestDeniedAction ToEnum(this string value)
        {
            foreach (var field in typeof(KeyAccessRequestDeniedAction).GetFields())
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

                    if (enumVal is KeyAccessRequestDeniedAction)
                    {
                        return (KeyAccessRequestDeniedAction)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum KeyAccessRequestDeniedAction");
        }
    }
}