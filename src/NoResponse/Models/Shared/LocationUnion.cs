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

    public class LocationUnionType
    {
        private LocationUnionType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static LocationUnionType Str
        {
            get {
                return new LocationUnionType("str");
            }
        }

        public static LocationUnionType Number
        {
            get {
                return new LocationUnionType("number");
            }
        }

        public static LocationUnionType Boolean
        {
            get {
                return new LocationUnionType("boolean");
            }
        }

        public static LocationUnionType Location
        {
            get {
                return new LocationUnionType("location");
            }
        }

        public static LocationUnionType ArrayOfAny
        {
            get {
                return new LocationUnionType("arrayOfAny");
            }
        }

        public static LocationUnionType Unknown
        {
            get {
                return new LocationUnionType("UNKNOWN");
            }
        }

        public override string ToString()
        {
            return Value;
        }
        public static implicit operator String(LocationUnionType v)
        {
            return v.Value;
        }
        public static LocationUnionType FromString(string v)
        {
            switch (v)
            {
                case "str":
                    return Str;
                case "number":
                    return Number;
                case "boolean":
                    return Boolean;
                case "location":
                    return Location;
                case "arrayOfAny":
                    return ArrayOfAny;
                case "UNKNOWN":
                    return Unknown;
                default:
                    throw new ArgumentException("Invalid value for LocationUnionType");
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Value.Equals(((LocationUnionType)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    [JsonConverter(typeof(LocationUnion.LocationUnionConverter))]
    public class LocationUnion : NoResponse.Utils.IOpenUnion
    {
        public LocationUnion(LocationUnionType type)
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
        public Location? Location { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public List<object>? ArrayOfAny { get; set; }

        public LocationUnionType Type { get; set; }

        [JsonIgnore]
        public JToken? UnknownRaw { get; private set; }

        public static LocationUnion CreateUnknown(JToken raw)
        {
            return new LocationUnion(LocationUnionType.Unknown) { UnknownRaw = raw };
        }

        public bool IsUnknown()
        {
            return Type.Equals(LocationUnionType.Unknown);
        }

        bool NoResponse.Utils.IOpenUnion.IsUnknown() => IsUnknown();

        public static LocationUnion CreateStr(string str)
        {
            LocationUnionType typ = LocationUnionType.Str;

            LocationUnion res = new LocationUnion(typ);
            res.Str = str;
            return res;
        }
        public static LocationUnion CreateNumber(double number)
        {
            LocationUnionType typ = LocationUnionType.Number;

            LocationUnion res = new LocationUnion(typ);
            res.Number = number;
            return res;
        }
        public static LocationUnion CreateBoolean(bool boolean)
        {
            LocationUnionType typ = LocationUnionType.Boolean;

            LocationUnion res = new LocationUnion(typ);
            res.Boolean = boolean;
            return res;
        }
        public static LocationUnion CreateLocation(Location location)
        {
            LocationUnionType typ = LocationUnionType.Location;

            LocationUnion res = new LocationUnion(typ);
            res.Location = location;
            return res;
        }
        public static LocationUnion CreateArrayOfAny(List<object> arrayOfAny)
        {
            LocationUnionType typ = LocationUnionType.ArrayOfAny;

            LocationUnion res = new LocationUnion(typ);
            res.ArrayOfAny = arrayOfAny;
            return res;
        }

        public class LocationUnionConverter : JsonConverter
        {
            public override bool CanConvert(System.Type objectType) => objectType == typeof(LocationUnion);

            public override bool CanRead => true;

            public override object? ReadJson(JsonReader reader, System.Type objectType, object? existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return CreateUnknown(JValue.CreateNull());
                }

                JToken jt = JToken.Load(reader);
                var unionCandidates = new UnionCandidatePool<LocationUnionType>();
                unionCandidates.Add<string>("Str", 0, LocationUnionType.Str);
                unionCandidates.Add<double>("Number", 1, LocationUnionType.Number);
                unionCandidates.Add<bool>("Boolean", 2, LocationUnionType.Boolean);
                unionCandidates.Add<Location>("Location", 3, LocationUnionType.Location);
                unionCandidates.Add<List<object>>("ArrayOfAny", 4, LocationUnionType.ArrayOfAny);

                var (winner, winnerValue) = unionCandidates.PickBest(
                    jt,
                    Utilities.GetDefaultJsonDeserializerSettings()
                );
                if (winner is null || winnerValue is null)
                {
                    return CreateUnknown(jt);
                }

                var result = new LocationUnion(winner.TypeName);
                typeof(LocationUnion).GetProperty(winner.PropertyName)!.SetValue(result, winnerValue);
                return result;
            }

            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Unexpected null JSON value.");
                }

                LocationUnion res = (LocationUnion)value;

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

                if (res.Location != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.Location));
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