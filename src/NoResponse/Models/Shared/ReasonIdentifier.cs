#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The submitted identifier or client context field a reason concerns; absent when it concerns the whole assessment. Client context is a claimed hint, not a device fingerprint.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ReasonIdentifier : IEquatable<ReasonIdentifier>, IOpenEnum<string>
    {
        public static readonly ReasonIdentifier Email = new ReasonIdentifier("email");
        public static readonly ReasonIdentifier Username = new ReasonIdentifier("username");
        public static readonly ReasonIdentifier Phone = new ReasonIdentifier("phone");
        public static readonly ReasonIdentifier Name = new ReasonIdentifier("name");
        public static readonly ReasonIdentifier Ip = new ReasonIdentifier("ip");
        public static readonly ReasonIdentifier Context = new ReasonIdentifier("context");

        private static readonly Dictionary<string, ReasonIdentifier> _knownValues =
            new Dictionary<string, ReasonIdentifier>() {
                ["email"] = Email,
                ["username"] = Username,
                ["phone"] = Phone,
                ["name"] = Name,
                ["ip"] = Ip,
                ["context"] = Context
            };

        private static readonly ConcurrentDictionary<string, ReasonIdentifier> _values =
            new ConcurrentDictionary<string, ReasonIdentifier>(_knownValues);

        private ReasonIdentifier(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ReasonIdentifier Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ReasonIdentifier(value));
        }

        public static implicit operator ReasonIdentifier(string value) => Of(value);
        public static implicit operator string(ReasonIdentifier reasonidentifier) => reasonidentifier.Value;

        public static ReasonIdentifier[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ReasonIdentifier);

        public bool Equals(ReasonIdentifier? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}