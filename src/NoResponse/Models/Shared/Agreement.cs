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
    public class Agreement : IEquatable<Agreement>, IOpenEnum<string>
    {
        public static readonly Agreement Match = new Agreement("match");
        public static readonly Agreement Partial = new Agreement("partial");
        public static readonly Agreement Mismatch = new Agreement("mismatch");

        private static readonly Dictionary<string, Agreement> _knownValues =
            new Dictionary<string, Agreement>() {
                ["match"] = Match,
                ["partial"] = Partial,
                ["mismatch"] = Mismatch
            };

        private static readonly ConcurrentDictionary<string, Agreement> _values =
            new ConcurrentDictionary<string, Agreement>(_knownValues);

        private Agreement(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Agreement Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Agreement(value));
        }

        public static implicit operator Agreement(string value) => Of(value);
        public static implicit operator string(Agreement agreement) => agreement.Value;

        public static Agreement[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Agreement);

        public bool Equals(Agreement? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}