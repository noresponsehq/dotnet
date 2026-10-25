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
    /// The submitted email or username whose own search matched the table. A submitted full name is searched as well, and what it finds feeds the identity's reasons rather than this list.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class BreachSelector : IEquatable<BreachSelector>, IOpenEnum<string>
    {
        public static readonly BreachSelector Email = new BreachSelector("email");
        public static readonly BreachSelector Username = new BreachSelector("username");
        public static readonly BreachSelector Ip = new BreachSelector("ip");

        private static readonly Dictionary<string, BreachSelector> _knownValues =
            new Dictionary<string, BreachSelector>() {
                ["email"] = Email,
                ["username"] = Username,
                ["ip"] = Ip
            };

        private static readonly ConcurrentDictionary<string, BreachSelector> _values =
            new ConcurrentDictionary<string, BreachSelector>(_knownValues);

        private BreachSelector(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static BreachSelector Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new BreachSelector(value));
        }

        public static implicit operator BreachSelector(string value) => Of(value);
        public static implicit operator string(BreachSelector breachselector) => breachselector.Value;

        public static BreachSelector[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as BreachSelector);

        public bool Equals(BreachSelector? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}