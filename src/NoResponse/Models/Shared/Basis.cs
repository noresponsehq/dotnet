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
    /// stated: read from a record. inferred: reasoned from several facts. absence: every source answered and none found it.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Basis : IEquatable<Basis>, IOpenEnum<string>
    {
        public static readonly Basis Stated = new Basis("stated");
        public static readonly Basis Inferred = new Basis("inferred");
        public static readonly Basis Absence = new Basis("absence");

        private static readonly Dictionary<string, Basis> _knownValues =
            new Dictionary<string, Basis>() {
                ["stated"] = Stated,
                ["inferred"] = Inferred,
                ["absence"] = Absence
            };

        private static readonly ConcurrentDictionary<string, Basis> _values =
            new ConcurrentDictionary<string, Basis>(_knownValues);

        private Basis(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Basis Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Basis(value));
        }

        public static implicit operator Basis(string value) => Of(value);
        public static implicit operator string(Basis basis) => basis.Value;

        public static Basis[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Basis);

        public bool Equals(Basis? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}