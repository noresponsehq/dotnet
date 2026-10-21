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
    /// Machine codes an HTTP 404 answer can carry.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class NotFoundErrorCode : IEquatable<NotFoundErrorCode>, IOpenEnum<string>
    {
        public static readonly NotFoundErrorCode NotFound = new NotFoundErrorCode("not_found");

        private static readonly Dictionary<string, NotFoundErrorCode> _knownValues =
            new Dictionary<string, NotFoundErrorCode>() {
                ["not_found"] = NotFound
            };

        private static readonly ConcurrentDictionary<string, NotFoundErrorCode> _values =
            new ConcurrentDictionary<string, NotFoundErrorCode>(_knownValues);

        private NotFoundErrorCode(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static NotFoundErrorCode Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new NotFoundErrorCode(value));
        }

        public static implicit operator NotFoundErrorCode(string value) => Of(value);
        public static implicit operator string(NotFoundErrorCode notfounderrorcode) => notfounderrorcode.Value;

        public static NotFoundErrorCode[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as NotFoundErrorCode);

        public bool Equals(NotFoundErrorCode? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}