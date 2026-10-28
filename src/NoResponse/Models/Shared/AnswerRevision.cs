#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;

    /// <summary>
    /// Which answer of each assessment the filters describe: latest, the latest stored revision, or first, the first answer, as served when the assessment was first answered (POST /v1/assessments/aggregate counts first answers). A cell of an aggregate drills down to POST /v1/assessments/search with the same filters, revision first and the cell's bucket as from and to.
    /// </summary>
    public enum AnswerRevision
    {
        [JsonProperty("latest")] Latest,
        [JsonProperty("first")] First,
    }

    public static class AnswerRevisionExtension
    {
        public static string Value(this AnswerRevision value)
        {
            return ((JsonPropertyAttribute)value.GetType().GetMember(value.ToString()) [0].GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName ?? value.ToString();
        }

        public static AnswerRevision ToEnum(this string value)
        {
            foreach (var field in typeof(AnswerRevision).GetFields())
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

                    if (enumVal is AnswerRevision)
                    {
                        return (AnswerRevision)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum AnswerRevision");
        }
    }
}