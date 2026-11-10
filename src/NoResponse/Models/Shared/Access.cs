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
    /// public profiles were read; a private or locked one exists but could not be read.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Access : IEquatable<Access>, IOpenEnum<string>
    {
        public static readonly Access Public = new Access("public");
        public static readonly Access Private = new Access("private");
        public static readonly Access Locked = new Access("locked");

        private static readonly Dictionary<string, Access> _knownValues =
            new Dictionary<string, Access>() {
                ["public"] = Public,
                ["private"] = Private,
                ["locked"] = Locked
            };

        private static readonly ConcurrentDictionary<string, Access> _values =
            new ConcurrentDictionary<string, Access>(_knownValues);

        private Access(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Access Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Access(value));
        }

        public static implicit operator Access(string value) => Of(value);
        public static implicit operator string(Access access) => access.Value;

        public static Access[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Access);

        public bool Equals(Access? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}