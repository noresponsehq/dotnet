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

    public class EmailUnionType
    {
        private EmailUnionType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static EmailUnionType Str
        {
            get {
                return new EmailUnionType("str");
            }
        }

        public static EmailUnionType Number
        {
            get {
                return new EmailUnionType("number");
            }
        }

        public static EmailUnionType Boolean
        {
            get {
                return new EmailUnionType("boolean");
            }
        }

        public static EmailUnionType Email
        {
            get {
                return new EmailUnionType("email");
            }
        }

        public static EmailUnionType ArrayOfAny
        {
            get {
                return new EmailUnionType("arrayOfAny");
            }
        }

        public static EmailUnionType Unknown
        {
            get {
                return new EmailUnionType("UNKNOWN");
            }
        }

        public override string ToString()
        {
            return Value;
        }
        public static implicit operator String(EmailUnionType v)
        {
            return v.Value;
        }
        public static EmailUnionType FromString(string v)
        {
            switch (v)
            {
                case "str":
                    return Str;
                case "number":
                    return Number;
                case "boolean":
                    return Boolean;
                case "email":
                    return Email;
                case "arrayOfAny":
                    return ArrayOfAny;
                case "UNKNOWN":
                    return Unknown;
                default:
                    throw new ArgumentException("Invalid value for EmailUnionType");
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Value.Equals(((EmailUnionType)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    [JsonConverter(typeof(EmailUnion.EmailUnionConverter))]
    public class EmailUnion : NoResponse.Utils.IOpenUnion
    {
        public EmailUnion(EmailUnionType type)
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
        public Email? Email { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public List<object>? ArrayOfAny { get; set; }

        public EmailUnionType Type { get; set; }

        [JsonIgnore]
        public JToken? UnknownRaw { get; private set; }

        public static EmailUnion CreateUnknown(JToken raw)
        {
            return new EmailUnion(EmailUnionType.Unknown) { UnknownRaw = raw };
        }

        public bool IsUnknown()
        {
            return Type.Equals(EmailUnionType.Unknown);
        }

        bool NoResponse.Utils.IOpenUnion.IsUnknown() => IsUnknown();

        public static EmailUnion CreateStr(string str)
        {
            EmailUnionType typ = EmailUnionType.Str;

            EmailUnion res = new EmailUnion(typ);
            res.Str = str;
            return res;
        }
        public static EmailUnion CreateNumber(double number)
        {
            EmailUnionType typ = EmailUnionType.Number;

            EmailUnion res = new EmailUnion(typ);
            res.Number = number;
            return res;
        }
        public static EmailUnion CreateBoolean(bool boolean)
        {
            EmailUnionType typ = EmailUnionType.Boolean;

            EmailUnion res = new EmailUnion(typ);
            res.Boolean = boolean;
            return res;
        }
        public static EmailUnion CreateEmail(Email email)
        {
            EmailUnionType typ = EmailUnionType.Email;

            EmailUnion res = new EmailUnion(typ);
            res.Email = email;
            return res;
        }
        public static EmailUnion CreateArrayOfAny(List<object> arrayOfAny)
        {
            EmailUnionType typ = EmailUnionType.ArrayOfAny;

            EmailUnion res = new EmailUnion(typ);
            res.ArrayOfAny = arrayOfAny;
            return res;
        }

        public class EmailUnionConverter : JsonConverter
        {
            public override bool CanConvert(System.Type objectType) => objectType == typeof(EmailUnion);

            public override bool CanRead => true;

            public override object? ReadJson(JsonReader reader, System.Type objectType, object? existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return CreateUnknown(JValue.CreateNull());
                }

                JToken jt = JToken.Load(reader);
                var unionCandidates = new UnionCandidatePool<EmailUnionType>();
                unionCandidates.Add<string>("Str", 0, EmailUnionType.Str);
                unionCandidates.Add<double>("Number", 1, EmailUnionType.Number);
                unionCandidates.Add<bool>("Boolean", 2, EmailUnionType.Boolean);
                unionCandidates.Add<Email>("Email", 3, EmailUnionType.Email);
                unionCandidates.Add<List<object>>("ArrayOfAny", 4, EmailUnionType.ArrayOfAny);

                var (winner, winnerValue) = unionCandidates.PickBest(
                    jt,
                    Utilities.GetDefaultJsonDeserializerSettings()
                );
                if (winner is null || winnerValue is null)
                {
                    return CreateUnknown(jt);
                }

                var result = new EmailUnion(winner.TypeName);
                typeof(EmailUnion).GetProperty(winner.PropertyName)!.SetValue(result, winnerValue);
                return result;
            }

            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Unexpected null JSON value.");
                }

                EmailUnion res = (EmailUnion)value;

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

                if (res.Email != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.Email));
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