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

    public class NameCandidateUnionType
    {
        private NameCandidateUnionType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static NameCandidateUnionType Str
        {
            get {
                return new NameCandidateUnionType("str");
            }
        }

        public static NameCandidateUnionType Number
        {
            get {
                return new NameCandidateUnionType("number");
            }
        }

        public static NameCandidateUnionType Boolean
        {
            get {
                return new NameCandidateUnionType("boolean");
            }
        }

        public static NameCandidateUnionType NameCandidate
        {
            get {
                return new NameCandidateUnionType("name_candidate");
            }
        }

        public static NameCandidateUnionType ArrayOfAny
        {
            get {
                return new NameCandidateUnionType("arrayOfAny");
            }
        }

        public static NameCandidateUnionType Unknown
        {
            get {
                return new NameCandidateUnionType("UNKNOWN");
            }
        }

        public override string ToString()
        {
            return Value;
        }
        public static implicit operator String(NameCandidateUnionType v)
        {
            return v.Value;
        }
        public static NameCandidateUnionType FromString(string v)
        {
            switch (v)
            {
                case "str":
                    return Str;
                case "number":
                    return Number;
                case "boolean":
                    return Boolean;
                case "name_candidate":
                    return NameCandidate;
                case "arrayOfAny":
                    return ArrayOfAny;
                case "UNKNOWN":
                    return Unknown;
                default:
                    throw new ArgumentException("Invalid value for NameCandidateUnionType");
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Value.Equals(((NameCandidateUnionType)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    [JsonConverter(typeof(NameCandidateUnion.NameCandidateUnionConverter))]
    public class NameCandidateUnion : NoResponse.Utils.IOpenUnion
    {
        public NameCandidateUnion(NameCandidateUnionType type)
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
        public NameCandidate? NameCandidate { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public List<object>? ArrayOfAny { get; set; }

        public NameCandidateUnionType Type { get; set; }

        [JsonIgnore]
        public JToken? UnknownRaw { get; private set; }

        public static NameCandidateUnion CreateUnknown(JToken raw)
        {
            return new NameCandidateUnion(NameCandidateUnionType.Unknown) { UnknownRaw = raw };
        }

        public bool IsUnknown()
        {
            return Type.Equals(NameCandidateUnionType.Unknown);
        }

        bool NoResponse.Utils.IOpenUnion.IsUnknown() => IsUnknown();

        public static NameCandidateUnion CreateStr(string str)
        {
            NameCandidateUnionType typ = NameCandidateUnionType.Str;

            NameCandidateUnion res = new NameCandidateUnion(typ);
            res.Str = str;
            return res;
        }
        public static NameCandidateUnion CreateNumber(double number)
        {
            NameCandidateUnionType typ = NameCandidateUnionType.Number;

            NameCandidateUnion res = new NameCandidateUnion(typ);
            res.Number = number;
            return res;
        }
        public static NameCandidateUnion CreateBoolean(bool boolean)
        {
            NameCandidateUnionType typ = NameCandidateUnionType.Boolean;

            NameCandidateUnion res = new NameCandidateUnion(typ);
            res.Boolean = boolean;
            return res;
        }
        public static NameCandidateUnion CreateNameCandidate(NameCandidate nameCandidate)
        {
            NameCandidateUnionType typ = NameCandidateUnionType.NameCandidate;

            NameCandidateUnion res = new NameCandidateUnion(typ);
            res.NameCandidate = nameCandidate;
            return res;
        }
        public static NameCandidateUnion CreateArrayOfAny(List<object> arrayOfAny)
        {
            NameCandidateUnionType typ = NameCandidateUnionType.ArrayOfAny;

            NameCandidateUnion res = new NameCandidateUnion(typ);
            res.ArrayOfAny = arrayOfAny;
            return res;
        }

        public class NameCandidateUnionConverter : JsonConverter
        {
            public override bool CanConvert(System.Type objectType) => objectType == typeof(NameCandidateUnion);

            public override bool CanRead => true;

            public override object? ReadJson(JsonReader reader, System.Type objectType, object? existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return CreateUnknown(JValue.CreateNull());
                }

                JToken jt = JToken.Load(reader);
                var unionCandidates = new UnionCandidatePool<NameCandidateUnionType>();
                unionCandidates.Add<string>("Str", 0, NameCandidateUnionType.Str);
                unionCandidates.Add<double>("Number", 1, NameCandidateUnionType.Number);
                unionCandidates.Add<bool>("Boolean", 2, NameCandidateUnionType.Boolean);
                unionCandidates.Add<NameCandidate>("NameCandidate", 3, NameCandidateUnionType.NameCandidate);
                unionCandidates.Add<List<object>>("ArrayOfAny", 4, NameCandidateUnionType.ArrayOfAny);

                var (winner, winnerValue) = unionCandidates.PickBest(
                    jt,
                    Utilities.GetDefaultJsonDeserializerSettings()
                );
                if (winner is null || winnerValue is null)
                {
                    return CreateUnknown(jt);
                }

                var result = new NameCandidateUnion(winner.TypeName);
                typeof(NameCandidateUnion).GetProperty(winner.PropertyName)!.SetValue(result, winnerValue);
                return result;
            }

            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Unexpected null JSON value.");
                }

                NameCandidateUnion res = (NameCandidateUnion)value;

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

                if (res.NameCandidate != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.NameCandidate));
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