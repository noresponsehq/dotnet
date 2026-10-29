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
    public class Action : IEquatable<Action>, IOpenEnum<string>
    {
        public static readonly Action Allow = new Action("allow");
        public static readonly Action Block = new Action("block");

        private static readonly Dictionary<string, Action> _knownValues =
            new Dictionary<string, Action>() {
                ["allow"] = Allow,
                ["block"] = Block
            };

        private static readonly ConcurrentDictionary<string, Action> _values =
            new ConcurrentDictionary<string, Action>(_knownValues);

        private Action(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Action Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Action(value));
        }

        public static implicit operator Action(string value) => Of(value);
        public static implicit operator string(Action action) => action.Value;

        public static Action[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Action);

        public bool Equals(Action? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}