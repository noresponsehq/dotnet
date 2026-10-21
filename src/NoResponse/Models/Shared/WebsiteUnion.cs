#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Reflection;

    public class WebsiteUnionType
    {
        private WebsiteUnionType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static WebsiteUnionType Str
        {
            get {
                return new WebsiteUnionType("str");
            }
        }

        public static WebsiteUnionType Number
        {
            get {
                return new WebsiteUnionType("number");
            }
        }

        public static WebsiteUnionType Boolean
        {
            get {
                return new WebsiteUnionType("boolean");
            }
        }

        public static WebsiteUnionType Website
        {
            get {
                return new WebsiteUnionType("website");
            }
        }

        public static WebsiteUnionType ArrayOfAny
        {
            get {
                return new WebsiteUnionType("arrayOfAny");
            }
        }

        public static WebsiteUnionType Unknown
        {
            get {
                return new WebsiteUnionType("UNKNOWN");
            }
        }

        public override string ToString()
        {
            return Value;
        }
        public static implicit operator String(WebsiteUnionType v)
        {
            return v.Value;
        }
        public static WebsiteUnionType FromString(string v)
        {
            switch (v)
            {
                case "str":
                    return Str;
                case "number":
                    return Number;
                case "boolean":
                    return Boolean;
                case "website":
                    return Website;
                case "arrayOfAny":
                    return ArrayOfAny;
                case "UNKNOWN":
                    return Unknown;
                default:
                    throw new ArgumentException("Invalid value for WebsiteUnionType");
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Value.Equals(((WebsiteUnionType)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    [JsonConverter(typeof(WebsiteUnion.WebsiteUnionConverter))]
    public class WebsiteUnion : NoResponse.Utils.IOpenUnion
    {
        public WebsiteUnion(WebsiteUnionType type)
        {
            Type = type;
        }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public string? Str { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public double? Number { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public bool? Boolean { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public Website? Website { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public List<object>? ArrayOfAny { get; set; }

        public WebsiteUnionType Type { get; set; }

        [JsonIgnore]
        public JToken? UnknownRaw { get; private set; }

        public static WebsiteUnion CreateUnknown(JToken raw)
        {
            return new WebsiteUnion(WebsiteUnionType.Unknown) { UnknownRaw = raw };
        }

        public bool IsUnknown()
        {
            return Type.Equals(WebsiteUnionType.Unknown);
        }

        bool NoResponse.Utils.IOpenUnion.IsUnknown() => IsUnknown();

        public static WebsiteUnion CreateStr(string str)
        {
            WebsiteUnionType typ = WebsiteUnionType.Str;

            WebsiteUnion res = new WebsiteUnion(typ);
            res.Str = str;
            return res;
        }
        public static WebsiteUnion CreateNumber(double number)
        {
            WebsiteUnionType typ = WebsiteUnionType.Number;

            WebsiteUnion res = new WebsiteUnion(typ);
            res.Number = number;
            return res;
        }
        public static WebsiteUnion CreateBoolean(bool boolean)
        {
            WebsiteUnionType typ = WebsiteUnionType.Boolean;

            WebsiteUnion res = new WebsiteUnion(typ);
            res.Boolean = boolean;
            return res;
        }
        public static WebsiteUnion CreateWebsite(Website website)
        {
            WebsiteUnionType typ = WebsiteUnionType.Website;

            WebsiteUnion res = new WebsiteUnion(typ);
            res.Website = website;
            return res;
        }
        public static WebsiteUnion CreateArrayOfAny(List<object> arrayOfAny)
        {
            WebsiteUnionType typ = WebsiteUnionType.ArrayOfAny;

            WebsiteUnion res = new WebsiteUnion(typ);
            res.ArrayOfAny = arrayOfAny;
            return res;
        }

        public class WebsiteUnionConverter : JsonConverter
        {
            public override bool CanConvert(System.Type objectType) => objectType == typeof(WebsiteUnion);

            public override bool CanRead => true;

            public override object? ReadJson(JsonReader reader, System.Type objectType, object? existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return CreateUnknown(JValue.CreateNull());
                }

                JToken jt = JToken.Load(reader);
                var unionCandidates = new UnionCandidatePool<WebsiteUnionType>();
                unionCandidates.Add<string>("Str", 0, WebsiteUnionType.Str);
                unionCandidates.Add<double>("Number", 1, WebsiteUnionType.Number);
                unionCandidates.Add<bool>("Boolean", 2, WebsiteUnionType.Boolean);
                unionCandidates.Add<Website>("Website", 3, WebsiteUnionType.Website);
                unionCandidates.Add<List<object>>("ArrayOfAny", 4, WebsiteUnionType.ArrayOfAny);

                var (winner, winnerValue) = unionCandidates.PickBest(
                    jt,
                    Utilities.GetDefaultJsonDeserializerSettings()
                );
                if (winner is null || winnerValue is null)
                {
                    return CreateUnknown(jt);
                }

                var result = new WebsiteUnion(winner.TypeName);
                typeof(WebsiteUnion).GetProperty(winner.PropertyName)!.SetValue(result, winnerValue);
                return result;
            }

            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Unexpected null JSON value.");
                }

                WebsiteUnion res = (WebsiteUnion)value;

                if (res.IsUnknown())
                {
                    if (res.UnknownRaw != null)
                    {
                        res.UnknownRaw.WriteTo(writer);
                        return;
                    }
                    throw new InvalidOperationException("Unknown union value has no raw payload; construct via CreateUnknown(JToken).");
                }

                if (res.Str != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.Str));
                    return;
                }

                if (res.Number != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.Number));
                    return;
                }

                if (res.Boolean != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.Boolean));
                    return;
                }

                if (res.Website != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.Website));
                    return;
                }

                if (res.ArrayOfAny != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.ArrayOfAny));
                    return;
                }

                throw new InvalidOperationException(
                    "Could not serialize union to JSON: no variant value was set. " +
                    "Construct this union using one of the Create* factory methods."
                );
            }
        }
    }
}