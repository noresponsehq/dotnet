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
    /// The depth a request for this flow that names none is served at. Defaults: full for account_creation and purchase, quick for login.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class DefaultDepth : IEquatable<DefaultDepth>, IOpenEnum<string>
    {
        public static readonly DefaultDepth Quick = new DefaultDepth("quick");
        public static readonly DefaultDepth Full = new DefaultDepth("full");

        private static readonly Dictionary<string, DefaultDepth> _knownValues =
            new Dictionary<string, DefaultDepth>() {
                ["quick"] = Quick,
                ["full"] = Full
            };

        private static readonly ConcurrentDictionary<string, DefaultDepth> _values =
            new ConcurrentDictionary<string, DefaultDepth>(_knownValues);

        private DefaultDepth(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static DefaultDepth Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new DefaultDepth(value));
        }

        public static implicit operator DefaultDepth(string value) => Of(value);
        public static implicit operator string(DefaultDepth defaultdepth) => defaultdepth.Value;

        public static DefaultDepth[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as DefaultDepth);

        public bool Equals(DefaultDepth? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}