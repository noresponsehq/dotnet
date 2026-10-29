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

    public class ActorType
    {
        private ActorType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static ActorType User
        {
            get {
                return new ActorType("user");
            }
        }

        public static ActorType ApiKey
        {
            get {
                return new ActorType("api_key");
            }
        }

        public static ActorType Unknown
        {
            get {
                return new ActorType("UNKNOWN");
            }
        }

        public override string ToString()
        {
            return Value;
        }
        public static implicit operator String(ActorType v)
        {
            return v.Value;
        }
        public static ActorType FromString(string v)
        {
            switch (v)
            {
                case "user":
                    return User;
                case "api_key":
                    return ApiKey;
                case "UNKNOWN":
                    return Unknown;
                default:
                    throw new ArgumentException("Invalid value for ActorType");
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Value.Equals(((ActorType)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    [JsonConverter(typeof(Actor.ActorConverter))]
    public class Actor : NoResponse.Utils.IOpenUnion
    {
        public Actor(ActorType type)
        {
            Type = type;
        }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public UserActor? UserActor { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public KeyActor? KeyActor { get; set; }

        public ActorType Type { get; set; }

        [JsonIgnore]
        public JToken? UnknownRaw { get; private set; }

        public static Actor CreateUnknown(JToken raw)
        {
            return new Actor(ActorType.Unknown) { UnknownRaw = raw };
        }

        public bool IsUnknown()
        {
            return Type.Equals(ActorType.Unknown);
        }

        bool NoResponse.Utils.IOpenUnion.IsUnknown() => IsUnknown();

        public static Actor CreateUser(UserActor user)
        {
            ActorType typ = ActorType.User;
            Actor res = new Actor(typ);
            res.UserActor = user;
            return res;
        }

        public static Actor CreateApiKey(KeyActor apiKey)
        {
            ActorType typ = ActorType.ApiKey;
            Actor res = new Actor(typ);
            res.KeyActor = apiKey;
            return res;
        }

        public class ActorConverter : JsonConverter
        {
            public override bool CanConvert(System.Type objectType) => objectType == typeof(Actor);

            public override bool CanRead => true;

            public override object? ReadJson(JsonReader reader, System.Type objectType, object? existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return CreateUnknown(JValue.CreateNull());
                }

                JToken token = JToken.Load(reader);
                JObject? jo = token as JObject;
                if (jo == null)
                {
                    return CreateUnknown(token);
                }

                JToken? discriminatorToken = jo.GetValue("type");
                if (discriminatorToken == null || discriminatorToken.Type != JTokenType.String)
                {
                    return CreateUnknown(jo);
                }

                string discriminator = discriminatorToken.ToString();
                if (discriminator == ActorType.User.ToString())
                {
                    UserActor userActor = ResponseBodyDeserializer.DeserializeNotNull<UserActor>(jo.ToString());
                    return CreateUser(userActor);
                }
                if (discriminator == ActorType.ApiKey.ToString())
                {
                    KeyActor keyActor = ResponseBodyDeserializer.DeserializeNotNull<KeyActor>(jo.ToString());
                    return CreateApiKey(keyActor);
                }

                return CreateUnknown(jo);
            }

            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Unexpected null JSON value.");
                }

                Actor res = (Actor)value;

                if (res.IsUnknown())
                {
                    if (res.UnknownRaw != null)
                    {
                        res.UnknownRaw.WriteTo(writer);
                        return;
                    }
                    throw new InvalidOperationException("Unknown union value has no raw payload; construct via CreateUnknown(JToken).");
                }

                if (res.UserActor != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.UserActor));
                    return;
                }

                if (res.KeyActor != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.KeyActor));
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