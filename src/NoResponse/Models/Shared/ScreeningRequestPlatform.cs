#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    public enum ScreeningRequestPlatform
    {
        [JsonProperty("instagram")] Instagram,
        [JsonProperty("x")] X,
        [JsonProperty("bluesky")] Bluesky,
        [JsonProperty("youtube")] Youtube,
        [JsonProperty("tiktok")] Tiktok,
        [JsonProperty("facebook")] Facebook,
        [JsonProperty("threads")] Threads,
        [JsonProperty("reddit")] Reddit,
        [JsonProperty("telegram")] Telegram,
        [JsonProperty("linkedin")] Linkedin,
    }

    public static class ScreeningRequestPlatformExtension
    {
        public static string Value(this ScreeningRequestPlatform value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static ScreeningRequestPlatform ToEnum(this string value)
        {
            foreach (var field in typeof(ScreeningRequestPlatform).GetFields())
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

                    if (enumVal is ScreeningRequestPlatform)
                    {
                        return (ScreeningRequestPlatform)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum ScreeningRequestPlatform");
        }
    }
}