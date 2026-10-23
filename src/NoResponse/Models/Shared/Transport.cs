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
    /// Where the context was derived: backend (also when the request named no transport), browser, or provider_hook for a hosted provider hook.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Transport : IEquatable<Transport>, IOpenEnum<string>
    {
        public static readonly Transport Backend = new Transport("backend");
        public static readonly Transport Browser = new Transport("browser");
        public static readonly Transport ProviderHook = new Transport("provider_hook");

        private static readonly Dictionary<string, Transport> _knownValues =
            new Dictionary<string, Transport>() {
                ["backend"] = Backend,
                ["browser"] = Browser,
                ["provider_hook"] = ProviderHook
            };

        private static readonly ConcurrentDictionary<string, Transport> _values =
            new ConcurrentDictionary<string, Transport>(_knownValues);

        private Transport(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Transport Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Transport(value));
        }

        public static implicit operator Transport(string value) => Of(value);
        public static implicit operator string(Transport transport) => transport.Value;

        public static Transport[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Transport);

        public bool Equals(Transport? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}