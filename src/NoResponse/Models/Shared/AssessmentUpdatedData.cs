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

    public class AssessmentUpdatedDataType
    {
        private AssessmentUpdatedDataType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static AssessmentUpdatedDataType Complete
        {
            get {
                return new AssessmentUpdatedDataType("complete");
            }
        }

        public static AssessmentUpdatedDataType Partial
        {
            get {
                return new AssessmentUpdatedDataType("partial");
            }
        }

        public static AssessmentUpdatedDataType Pending
        {
            get {
                return new AssessmentUpdatedDataType("pending");
            }
        }

        public static AssessmentUpdatedDataType Expired
        {
            get {
                return new AssessmentUpdatedDataType("expired");
            }
        }

        public override string ToString()
        {
            return Value;
        }
        public static implicit operator String(AssessmentUpdatedDataType v)
        {
            return v.Value;
        }
        public static AssessmentUpdatedDataType FromString(string v)
        {
            switch (v)
            {
                case "complete":
                    return Complete;
                case "partial":
                    return Partial;
                case "pending":
                    return Pending;
                case "expired":
                    return Expired;
                default:
                    throw new ArgumentException("Invalid value for AssessmentUpdatedDataType");
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Value.Equals(((AssessmentUpdatedDataType)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    /// <summary>
    /// The assessment, or the minimal notice that it expired.
    /// </summary>
    [JsonConverter(typeof(AssessmentUpdatedData.AssessmentUpdatedDataConverter))]
    public class AssessmentUpdatedData
    {
        public AssessmentUpdatedData(AssessmentUpdatedDataType type)
        {
            Type = type;
        }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public Assessment? Assessment { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public AssessmentExpired? AssessmentExpired { get; set; }

        public AssessmentUpdatedDataType Type { get; set; }

        public static AssessmentUpdatedData CreateComplete(Assessment complete)
        {
            AssessmentUpdatedDataType typ = AssessmentUpdatedDataType.Complete;
            string typStr = AssessmentUpdatedDataType.Complete.ToString();
            complete.Status = Status.Of(AssessmentUpdatedDataType.Complete.ToString());
            AssessmentUpdatedData res = new AssessmentUpdatedData(typ);
            res.Assessment = complete;
            return res;
        }

        public static AssessmentUpdatedData CreatePartial(Assessment partialT)
        {
            AssessmentUpdatedDataType typ = AssessmentUpdatedDataType.Partial;
            string typStr = AssessmentUpdatedDataType.Partial.ToString();
            partialT.Status = Status.Of(AssessmentUpdatedDataType.Partial.ToString());
            AssessmentUpdatedData res = new AssessmentUpdatedData(typ);
            res.Assessment = partialT;
            return res;
        }

        public static AssessmentUpdatedData CreatePending(Assessment pending)
        {
            AssessmentUpdatedDataType typ = AssessmentUpdatedDataType.Pending;
            string typStr = AssessmentUpdatedDataType.Pending.ToString();
            pending.Status = Status.Of(AssessmentUpdatedDataType.Pending.ToString());
            AssessmentUpdatedData res = new AssessmentUpdatedData(typ);
            res.Assessment = pending;
            return res;
        }

        public static AssessmentUpdatedData CreateExpired(AssessmentExpired expired)
        {
            AssessmentUpdatedDataType typ = AssessmentUpdatedDataType.Expired;
            AssessmentUpdatedData res = new AssessmentUpdatedData(typ);
            res.AssessmentExpired = expired;
            return res;
        }

        public class AssessmentUpdatedDataConverter : JsonConverter
        {
            public override bool CanConvert(System.Type objectType) => objectType == typeof(AssessmentUpdatedData);

            public override bool CanRead => true;

            public override object? ReadJson(JsonReader reader, System.Type objectType, object? existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {

                    throw new JsonSerializationException("Received unexpected null JSON value");
                }

                JObject jo = JObject.Load(reader);
                string discriminator = jo.GetValue("status")?.ToString() ?? throw new ArgumentNullException("Could not find discriminator field.");
                if (discriminator == AssessmentUpdatedDataType.Complete.ToString())
                {
                    Assessment assessment = ResponseBodyDeserializer.DeserializeNotNull<Assessment>(jo.ToString());
                    return CreateComplete(assessment);
                }
                if (discriminator == AssessmentUpdatedDataType.Partial.ToString())
                {
                    Assessment assessment = ResponseBodyDeserializer.DeserializeNotNull<Assessment>(jo.ToString());
                    return CreatePartial(assessment);
                }
                if (discriminator == AssessmentUpdatedDataType.Pending.ToString())
                {
                    Assessment assessment = ResponseBodyDeserializer.DeserializeNotNull<Assessment>(jo.ToString());
                    return CreatePending(assessment);
                }
                if (discriminator == AssessmentUpdatedDataType.Expired.ToString())
                {
                    AssessmentExpired assessmentExpired = ResponseBodyDeserializer.DeserializeNotNull<AssessmentExpired>(jo.ToString());
                    return CreateExpired(assessmentExpired);
                }

                throw new InvalidOperationException("Could not deserialize into any supported types.");
            }

            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Unexpected null JSON value.");
                }

                AssessmentUpdatedData res = (AssessmentUpdatedData)value;

                if (res.Assessment != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.Assessment));
                    return;
                }

                if (res.AssessmentExpired != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.AssessmentExpired));
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