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
    /// assessments only.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class UsageCountDepth : IEquatable<UsageCountDepth>, IOpenEnum<string>
    {
        public static readonly UsageCountDepth Quick = new UsageCountDepth("quick");
        public static readonly UsageCountDepth Full = new UsageCountDepth("full");

        private static readonly Dictionary<string, UsageCountDepth> _knownValues =
            new Dictionary<string, UsageCountDepth>() {
                ["quick"] = Quick,
                ["full"] = Full
            };

        private static readonly ConcurrentDictionary<string, UsageCountDepth> _values =
            new ConcurrentDictionary<string, UsageCountDepth>(_knownValues);

        private UsageCountDepth(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static UsageCountDepth Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new UsageCountDepth(value));
        }

        public static implicit operator UsageCountDepth(string value) => Of(value);
        public static implicit operator string(UsageCountDepth usagecountdepth) => usagecountdepth.Value;

        public static UsageCountDepth[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as UsageCountDepth);

        public bool Equals(UsageCountDepth? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}