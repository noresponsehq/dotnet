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

    public class SocialProfileUnionType
    {
        private SocialProfileUnionType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static SocialProfileUnionType Str
        {
            get {
                return new SocialProfileUnionType("str");
            }
        }

        public static SocialProfileUnionType Number
        {
            get {
                return new SocialProfileUnionType("number");
            }
        }

        public static SocialProfileUnionType Boolean
        {
            get {
                return new SocialProfileUnionType("boolean");
            }
        }

        public static SocialProfileUnionType SocialProfile
        {
            get {
                return new SocialProfileUnionType("social_profile");
            }
        }

        public static SocialProfileUnionType ArrayOfAny
        {
            get {
                return new SocialProfileUnionType("arrayOfAny");
            }
        }

        public static SocialProfileUnionType Unknown
        {
            get {
                return new SocialProfileUnionType("UNKNOWN");
            }
        }

        public override string ToString()
        {
            return Value;
        }
        public static implicit operator String(SocialProfileUnionType v)
        {
            return v.Value;
        }
        public static SocialProfileUnionType FromString(string v)
        {
            switch (v)
            {
                case "str":
                    return Str;
                case "number":
                    return Number;
                case "boolean":
                    return Boolean;
                case "social_profile":
                    return SocialProfile;
                case "arrayOfAny":
                    return ArrayOfAny;
                case "UNKNOWN":
                    return Unknown;
                default:
                    throw new ArgumentException("Invalid value for SocialProfileUnionType");
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Value.Equals(((SocialProfileUnionType)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    [JsonConverter(typeof(SocialProfileUnion.SocialProfileUnionConverter))]
    public class SocialProfileUnion : NoResponse.Utils.IOpenUnion
    {
        public SocialProfileUnion(SocialProfileUnionType type)
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
        public SocialProfile? SocialProfile { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public List<object>? ArrayOfAny { get; set; }

        public SocialProfileUnionType Type { get; set; }

        [JsonIgnore]
        public JToken? UnknownRaw { get; private set; }

        public static SocialProfileUnion CreateUnknown(JToken raw)
        {
            return new SocialProfileUnion(SocialProfileUnionType.Unknown) { UnknownRaw = raw };
        }

        public bool IsUnknown()
        {
            return Type.Equals(SocialProfileUnionType.Unknown);
        }

        bool NoResponse.Utils.IOpenUnion.IsUnknown() => IsUnknown();

        public static SocialProfileUnion CreateStr(string str)
        {
            SocialProfileUnionType typ = SocialProfileUnionType.Str;

            SocialProfileUnion res = new SocialProfileUnion(typ);
            res.Str = str;
            return res;
        }
        public static SocialProfileUnion CreateNumber(double number)
        {
            SocialProfileUnionType typ = SocialProfileUnionType.Number;

            SocialProfileUnion res = new SocialProfileUnion(typ);
            res.Number = number;
            return res;
        }
        public static SocialProfileUnion CreateBoolean(bool boolean)
        {
            SocialProfileUnionType typ = SocialProfileUnionType.Boolean;

            SocialProfileUnion res = new SocialProfileUnion(typ);
            res.Boolean = boolean;
            return res;
        }
        public static SocialProfileUnion CreateSocialProfile(SocialProfile socialProfile)
        {
            SocialProfileUnionType typ = SocialProfileUnionType.SocialProfile;

            SocialProfileUnion res = new SocialProfileUnion(typ);
            res.SocialProfile = socialProfile;
            return res;
        }
        public static SocialProfileUnion CreateArrayOfAny(List<object> arrayOfAny)
        {
            SocialProfileUnionType typ = SocialProfileUnionType.ArrayOfAny;

            SocialProfileUnion res = new SocialProfileUnion(typ);
            res.ArrayOfAny = arrayOfAny;
            return res;
        }

        public class SocialProfileUnionConverter : JsonConverter
        {
            public override bool CanConvert(System.Type objectType) => objectType == typeof(SocialProfileUnion);

            public override bool CanRead => true;

            public override object? ReadJson(JsonReader reader, System.Type objectType, object? existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return CreateUnknown(JValue.CreateNull());
                }

                JToken jt = JToken.Load(reader);
                var unionCandidates = new UnionCandidatePool<SocialProfileUnionType>();
                unionCandidates.Add<string>("Str", 0, SocialProfileUnionType.Str);
                unionCandidates.Add<double>("Number", 1, SocialProfileUnionType.Number);
                unionCandidates.Add<bool>("Boolean", 2, SocialProfileUnionType.Boolean);
                unionCandidates.Add<SocialProfile>("SocialProfile", 3, SocialProfileUnionType.SocialProfile);
                unionCandidates.Add<List<object>>("ArrayOfAny", 4, SocialProfileUnionType.ArrayOfAny);

                var (winner, winnerValue) = unionCandidates.PickBest(
                    jt,
                    Utilities.GetDefaultJsonDeserializerSettings()
                );
                if (winner is null || winnerValue is null)
                {
                    return CreateUnknown(jt);
                }

                var result = new SocialProfileUnion(winner.TypeName);
                typeof(SocialProfileUnion).GetProperty(winner.PropertyName)!.SetValue(result, winnerValue);
                return result;
            }

            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Unexpected null JSON value.");
                }

                SocialProfileUnion res = (SocialProfileUnion)value;

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

                if (res.SocialProfile != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.SocialProfile));
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