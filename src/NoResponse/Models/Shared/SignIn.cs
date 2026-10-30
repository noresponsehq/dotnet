#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    [JsonConverter(typeof(OpenEnumConverter))]
    public class SignIn : IEquatable<SignIn>, IOpenEnum<string>
    {
        public static readonly SignIn Password = new SignIn("password");
        public static readonly SignIn Google = new SignIn("google");
        public static readonly SignIn Facebook = new SignIn("facebook");
        public static readonly SignIn Apple = new SignIn("apple");
        public static readonly SignIn Microsoft = new SignIn("microsoft");
        public static readonly SignIn Sso = new SignIn("sso");
        public static readonly SignIn Other = new SignIn("other");

        private static readonly Dictionary<string, SignIn> _knownValues =
            new Dictionary<string, SignIn>() {
                ["password"] = Password,
                ["google"] = Google,
                ["facebook"] = Facebook,
                ["apple"] = Apple,
                ["microsoft"] = Microsoft,
                ["sso"] = Sso,
                ["other"] = Other
            };

        private static readonly ConcurrentDictionary<string, SignIn> _values =
            new ConcurrentDictionary<string, SignIn>(_knownValues);

        private SignIn(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static SignIn Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new SignIn(value));
        }

        public static implicit operator SignIn(string value) => Of(value);
        public static implicit operator string(SignIn signin) => signin.Value;

        public static SignIn[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as SignIn);

        public bool Equals(SignIn? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}