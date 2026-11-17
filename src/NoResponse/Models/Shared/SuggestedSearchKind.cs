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
    /// The kind of the value: one of the kinds the environment's enrichment setting names.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class SuggestedSearchKind : IEquatable<SuggestedSearchKind>, IOpenEnum<string>
    {
        public static readonly SuggestedSearchKind Username = new SuggestedSearchKind("username");
        public static readonly SuggestedSearchKind Email = new SuggestedSearchKind("email");
        public static readonly SuggestedSearchKind Phone = new SuggestedSearchKind("phone");

        private static readonly Dictionary<string, SuggestedSearchKind> _knownValues =
            new Dictionary<string, SuggestedSearchKind>() {
                ["username"] = Username,
                ["email"] = Email,
                ["phone"] = Phone
            };

        private static readonly ConcurrentDictionary<string, SuggestedSearchKind> _values =
            new ConcurrentDictionary<string, SuggestedSearchKind>(_knownValues);

        private SuggestedSearchKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static SuggestedSearchKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new SuggestedSearchKind(value));
        }

        public static implicit operator SuggestedSearchKind(string value) => Of(value);
        public static implicit operator string(SuggestedSearchKind suggestedsearchkind) => suggestedsearchkind.Value;

        public static SuggestedSearchKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as SuggestedSearchKind);

        public bool Equals(SuggestedSearchKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}