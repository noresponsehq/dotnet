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
    public class Between : IEquatable<Between>, IOpenEnum<string>
    {
        public static readonly Between Email = new Between("email");
        public static readonly Between Phone = new Between("phone");

        private static readonly Dictionary<string, Between> _knownValues =
            new Dictionary<string, Between>() {
                ["email"] = Email,
                ["phone"] = Phone
            };

        private static readonly ConcurrentDictionary<string, Between> _values =
            new ConcurrentDictionary<string, Between>(_knownValues);

        private Between(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Between Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Between(value));
        }

        public static implicit operator Between(string value) => Of(value);
        public static implicit operator string(Between between) => between.Value;

        public static Between[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Between);

        public bool Equals(Between? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}