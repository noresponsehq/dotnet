#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// The provider event a provider_hook context was sent for. pre_registration runs before the user exists, so subject and the request's account_id may be absent there. Sent only with transport provider_hook.
    /// </summary>
    public enum HookPoint
    {
        [JsonProperty("pre_registration")] PreRegistration,
        [JsonProperty("post_registration")] PostRegistration,
        [JsonProperty("pre_login")] PreLogin,
        [JsonProperty("post_login")] PostLogin,
        [JsonProperty("token_issuance")] TokenIssuance,
    }

    public static class HookPointExtension
    {
        public static string Value(this HookPoint value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static HookPoint ToEnum(this string value)
        {
            foreach (var field in typeof(HookPoint).GetFields())
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

                    if (enumVal is HookPoint)
                    {
                        return (HookPoint)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum HookPoint");
        }
    }
}