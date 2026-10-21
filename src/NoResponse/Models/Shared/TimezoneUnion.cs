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

    public class TimezoneUnionType
    {
        private TimezoneUnionType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static TimezoneUnionType Str
        {
            get {
                return new TimezoneUnionType("str");
            }
        }

        public static TimezoneUnionType Number
        {
            get {
                return new TimezoneUnionType("number");
            }
        }

        public static TimezoneUnionType Boolean
        {
            get {
                return new TimezoneUnionType("boolean");
            }
        }

        public static TimezoneUnionType Timezone
        {
            get {
                return new TimezoneUnionType("timezone");
            }
        }

        public static TimezoneUnionType ArrayOfAny
        {
            get {
                return new TimezoneUnionType("arrayOfAny");
            }
        }

        public static TimezoneUnionType Unknown
        {
            get {
                return new TimezoneUnionType("UNKNOWN");
            }
        }

        public override string ToString()
        {
            return Value;
        }
        public static implicit operator String(TimezoneUnionType v)
        {
            return v.Value;
        }
        public static TimezoneUnionType FromString(string v)
        {
            switch (v)
            {
                case "str":
                    return Str;
                case "number":
                    return Number;
                case "boolean":
                    return Boolean;
                case "timezone":
                    return Timezone;
                case "arrayOfAny":
                    return ArrayOfAny;
                case "UNKNOWN":
                    return Unknown;
                default:
                    throw new ArgumentException("Invalid value for TimezoneUnionType");
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Value.Equals(((TimezoneUnionType)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    [JsonConverter(typeof(TimezoneUnion.TimezoneUnionConverter))]
    public class TimezoneUnion : NoResponse.Utils.IOpenUnion
    {
        public TimezoneUnion(TimezoneUnionType type)
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
        public Timezone? Timezone { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public List<object>? ArrayOfAny { get; set; }

        public TimezoneUnionType Type { get; set; }

        [JsonIgnore]
        public JToken? UnknownRaw { get; private set; }

        public static TimezoneUnion CreateUnknown(JToken raw)
        {
            return new TimezoneUnion(TimezoneUnionType.Unknown) { UnknownRaw = raw };
        }

        public bool IsUnknown()
        {
            return Type.Equals(TimezoneUnionType.Unknown);
        }

        bool NoResponse.Utils.IOpenUnion.IsUnknown() => IsUnknown();

        public static TimezoneUnion CreateStr(string str)
        {
            TimezoneUnionType typ = TimezoneUnionType.Str;

            TimezoneUnion res = new TimezoneUnion(typ);
            res.Str = str;
            return res;
        }
        public static TimezoneUnion CreateNumber(double number)
        {
            TimezoneUnionType typ = TimezoneUnionType.Number;

            TimezoneUnion res = new TimezoneUnion(typ);
            res.Number = number;
            return res;
        }
        public static TimezoneUnion CreateBoolean(bool boolean)
        {
            TimezoneUnionType typ = TimezoneUnionType.Boolean;

            TimezoneUnion res = new TimezoneUnion(typ);
            res.Boolean = boolean;
            return res;
        }
        public static TimezoneUnion CreateTimezone(Timezone timezone)
        {
            TimezoneUnionType typ = TimezoneUnionType.Timezone;

            TimezoneUnion res = new TimezoneUnion(typ);
            res.Timezone = timezone;
            return res;
        }
        public static TimezoneUnion CreateArrayOfAny(List<object> arrayOfAny)
        {
            TimezoneUnionType typ = TimezoneUnionType.ArrayOfAny;

            TimezoneUnion res = new TimezoneUnion(typ);
            res.ArrayOfAny = arrayOfAny;
            return res;
        }

        public class TimezoneUnionConverter : JsonConverter
        {
            public override bool CanConvert(System.Type objectType) => objectType == typeof(TimezoneUnion);

            public override bool CanRead => true;

            public override object? ReadJson(JsonReader reader, System.Type objectType, object? existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return CreateUnknown(JValue.CreateNull());
                }

                JToken jt = JToken.Load(reader);
                var unionCandidates = new UnionCandidatePool<TimezoneUnionType>();
                unionCandidates.Add<string>("Str", 0, TimezoneUnionType.Str);
                unionCandidates.Add<double>("Number", 1, TimezoneUnionType.Number);
                unionCandidates.Add<bool>("Boolean", 2, TimezoneUnionType.Boolean);
                unionCandidates.Add<Timezone>("Timezone", 3, TimezoneUnionType.Timezone);
                unionCandidates.Add<List<object>>("ArrayOfAny", 4, TimezoneUnionType.ArrayOfAny);

                var (winner, winnerValue) = unionCandidates.PickBest(
                    jt,
                    Utilities.GetDefaultJsonDeserializerSettings()
                );
                if (winner is null || winnerValue is null)
                {
                    return CreateUnknown(jt);
                }

                var result = new TimezoneUnion(winner.TypeName);
                typeof(TimezoneUnion).GetProperty(winner.PropertyName)!.SetValue(result, winnerValue);
                return result;
            }

            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Unexpected null JSON value.");
                }

                TimezoneUnion res = (TimezoneUnion)value;

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

                if (res.Timezone != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.Timezone));
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