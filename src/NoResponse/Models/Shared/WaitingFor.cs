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
    public class WaitingFor : IEquatable<WaitingFor>, IOpenEnum<string>
    {
        public static readonly WaitingFor Outcome = new WaitingFor("outcome");
        public static readonly WaitingFor Review = new WaitingFor("review");

        private static readonly Dictionary<string, WaitingFor> _knownValues =
            new Dictionary<string, WaitingFor>() {
                ["outcome"] = Outcome,
                ["review"] = Review
            };

        private static readonly ConcurrentDictionary<string, WaitingFor> _values =
            new ConcurrentDictionary<string, WaitingFor>(_knownValues);

        private WaitingFor(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static WaitingFor Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new WaitingFor(value));
        }

        public static implicit operator WaitingFor(string value) => Of(value);
        public static implicit operator string(WaitingFor waitingfor) => waitingfor.Value;

        public static WaitingFor[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as WaitingFor);

        public bool Equals(WaitingFor? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}