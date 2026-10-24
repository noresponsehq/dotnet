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
    public class Scope : IEquatable<Scope>, IOpenEnum<string>
    {
        public static readonly Scope Assess = new Scope("assess");
        public static readonly Scope History = new Scope("history");
        public static readonly Scope Delete = new Scope("delete");
        public static readonly Scope Webhooks = new Scope("webhooks");
        public static readonly Scope Detailed = new Scope("detailed");
        public static readonly Scope Integrations = new Scope("integrations");

        private static readonly Dictionary<string, Scope> _knownValues =
            new Dictionary<string, Scope>() {
                ["assess"] = Assess,
                ["history"] = History,
                ["delete"] = Delete,
                ["webhooks"] = Webhooks,
                ["detailed"] = Detailed,
                ["integrations"] = Integrations
            };

        private static readonly ConcurrentDictionary<string, Scope> _values =
            new ConcurrentDictionary<string, Scope>(_knownValues);

        private Scope(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Scope Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Scope(value));
        }

        public static implicit operator Scope(string value) => Of(value);
        public static implicit operator string(Scope scope) => scope.Value;

        public static Scope[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Scope);

        public bool Equals(Scope? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}