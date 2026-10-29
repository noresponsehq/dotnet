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
    public class From : IEquatable<From>, IOpenEnum<string>
    {
        public static readonly From Allow = new From("allow");
        public static readonly From StepUp = new From("step_up");
        public static readonly From Review = new From("review");
        public static readonly From Block = new From("block");
        public static readonly From Held = new From("held");

        private static readonly Dictionary<string, From> _knownValues =
            new Dictionary<string, From>() {
                ["allow"] = Allow,
                ["step_up"] = StepUp,
                ["review"] = Review,
                ["block"] = Block,
                ["held"] = Held
            };

        private static readonly ConcurrentDictionary<string, From> _values =
            new ConcurrentDictionary<string, From>(_knownValues);

        private From(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static From Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new From(value));
        }

        public static implicit operator From(string value) => Of(value);
        public static implicit operator string(From fromvar) => fromvar.Value;

        public static From[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as From);

        public bool Equals(From? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}