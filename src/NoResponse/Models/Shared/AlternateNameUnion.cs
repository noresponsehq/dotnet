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

    public class AlternateNameUnionType
    {
        private AlternateNameUnionType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static AlternateNameUnionType Str
        {
            get {
                return new AlternateNameUnionType("str");
            }
        }

        public static AlternateNameUnionType Number
        {
            get {
                return new AlternateNameUnionType("number");
            }
        }

        public static AlternateNameUnionType Boolean
        {
            get {
                return new AlternateNameUnionType("boolean");
            }
        }

        public static AlternateNameUnionType AlternateName
        {
            get {
                return new AlternateNameUnionType("alternate_name");
            }
        }

        public static AlternateNameUnionType ArrayOfAny
        {
            get {
                return new AlternateNameUnionType("arrayOfAny");
            }
        }

        public static AlternateNameUnionType Unknown
        {
            get {
                return new AlternateNameUnionType("UNKNOWN");
            }
        }

        public override string ToString()
        {
            return Value;
        }
        public static implicit operator String(AlternateNameUnionType v)
        {
            return v.Value;
        }
        public static AlternateNameUnionType FromString(string v)
        {
            switch (v)
            {
                case "str":
                    return Str;
                case "number":
                    return Number;
                case "boolean":
                    return Boolean;
                case "alternate_name":
                    return AlternateName;
                case "arrayOfAny":
                    return ArrayOfAny;
                case "UNKNOWN":
                    return Unknown;
                default:
                    throw new ArgumentException("Invalid value for AlternateNameUnionType");
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Value.Equals(((AlternateNameUnionType)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    [JsonConverter(typeof(AlternateNameUnion.AlternateNameUnionConverter))]
    public class AlternateNameUnion : NoResponse.Utils.IOpenUnion
    {
        public AlternateNameUnion(AlternateNameUnionType type)
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
        public AlternateName? AlternateName { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public List<object>? ArrayOfAny { get; set; }

        public AlternateNameUnionType Type { get; set; }

        [JsonIgnore]
        public JToken? UnknownRaw { get; private set; }

        public static AlternateNameUnion CreateUnknown(JToken raw)
        {
            return new AlternateNameUnion(AlternateNameUnionType.Unknown) { UnknownRaw = raw };
        }

        public bool IsUnknown()
        {
            return Type.Equals(AlternateNameUnionType.Unknown);
        }

        bool NoResponse.Utils.IOpenUnion.IsUnknown() => IsUnknown();

        public static AlternateNameUnion CreateStr(string str)
        {
            AlternateNameUnionType typ = AlternateNameUnionType.Str;

            AlternateNameUnion res = new AlternateNameUnion(typ);
            res.Str = str;
            return res;
        }
        public static AlternateNameUnion CreateNumber(double number)
        {
            AlternateNameUnionType typ = AlternateNameUnionType.Number;

            AlternateNameUnion res = new AlternateNameUnion(typ);
            res.Number = number;
            return res;
        }
        public static AlternateNameUnion CreateBoolean(bool boolean)
        {
            AlternateNameUnionType typ = AlternateNameUnionType.Boolean;

            AlternateNameUnion res = new AlternateNameUnion(typ);
            res.Boolean = boolean;
            return res;
        }
        public static AlternateNameUnion CreateAlternateName(AlternateName alternateName)
        {
            AlternateNameUnionType typ = AlternateNameUnionType.AlternateName;

            AlternateNameUnion res = new AlternateNameUnion(typ);
            res.AlternateName = alternateName;
            return res;
        }
        public static AlternateNameUnion CreateArrayOfAny(List<object> arrayOfAny)
        {
            AlternateNameUnionType typ = AlternateNameUnionType.ArrayOfAny;

            AlternateNameUnion res = new AlternateNameUnion(typ);
            res.ArrayOfAny = arrayOfAny;
            return res;
        }

        public class AlternateNameUnionConverter : JsonConverter
        {
            public override bool CanConvert(System.Type objectType) => objectType == typeof(AlternateNameUnion);

            public override bool CanRead => true;

            public override object? ReadJson(JsonReader reader, System.Type objectType, object? existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return CreateUnknown(JValue.CreateNull());
                }

                JToken jt = JToken.Load(reader);
                var unionCandidates = new UnionCandidatePool<AlternateNameUnionType>();
                unionCandidates.Add<string>("Str", 0, AlternateNameUnionType.Str);
                unionCandidates.Add<double>("Number", 1, AlternateNameUnionType.Number);
                unionCandidates.Add<bool>("Boolean", 2, AlternateNameUnionType.Boolean);
                unionCandidates.Add<AlternateName>("AlternateName", 3, AlternateNameUnionType.AlternateName);
                unionCandidates.Add<List<object>>("ArrayOfAny", 4, AlternateNameUnionType.ArrayOfAny);

                var (winner, winnerValue) = unionCandidates.PickBest(
                    jt,
                    Utilities.GetDefaultJsonDeserializerSettings()
                );
                if (winner is null || winnerValue is null)
                {
                    return CreateUnknown(jt);
                }

                var result = new AlternateNameUnion(winner.TypeName);
                typeof(AlternateNameUnion).GetProperty(winner.PropertyName)!.SetValue(result, winnerValue);
                return result;
            }

            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Unexpected null JSON value.");
                }

                AlternateNameUnion res = (AlternateNameUnion)value;

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

                if (res.AlternateName != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.AlternateName));
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