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
    /// Listed and returned members are active.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class MemberStatus : IEquatable<MemberStatus>, IOpenEnum<string>
    {
        public static readonly MemberStatus Active = new MemberStatus("active");

        private static readonly Dictionary<string, MemberStatus> _knownValues =
            new Dictionary<string, MemberStatus>() {
                ["active"] = Active
            };

        private static readonly ConcurrentDictionary<string, MemberStatus> _values =
            new ConcurrentDictionary<string, MemberStatus>(_knownValues);

        private MemberStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static MemberStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new MemberStatus(value));
        }

        public static implicit operator MemberStatus(string value) => Of(value);
        public static implicit operator string(MemberStatus memberstatus) => memberstatus.Value;

        public static MemberStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as MemberStatus);

        public bool Equals(MemberStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}