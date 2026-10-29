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
    /// Required for username: the username submitted by this customer in this workspace/environment. It does not name or join a source platform. Omit for email and phone.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Namespace : IEquatable<Namespace>, IOpenEnum<string>
    {
        public static readonly Namespace Customer = new Namespace("customer");

        private static readonly Dictionary<string, Namespace> _knownValues =
            new Dictionary<string, Namespace>() {
                ["customer"] = Customer
            };

        private static readonly ConcurrentDictionary<string, Namespace> _values =
            new ConcurrentDictionary<string, Namespace>(_knownValues);

        private Namespace(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Namespace Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Namespace(value));
        }

        public static implicit operator Namespace(string value) => Of(value);
        public static implicit operator string(Namespace namespacevar) => namespacevar.Value;

        public static Namespace[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Namespace);

        public bool Equals(Namespace? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}