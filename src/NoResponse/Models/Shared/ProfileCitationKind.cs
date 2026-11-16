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
    /// record: a record in lookup.records or profiles[].record. post: an item in posts. check: an item in lookup.checks.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ProfileCitationKind : IEquatable<ProfileCitationKind>, IOpenEnum<string>
    {
        public static readonly ProfileCitationKind Record = new ProfileCitationKind("record");
        public static readonly ProfileCitationKind Post = new ProfileCitationKind("post");
        public static readonly ProfileCitationKind Check = new ProfileCitationKind("check");

        private static readonly Dictionary<string, ProfileCitationKind> _knownValues =
            new Dictionary<string, ProfileCitationKind>() {
                ["record"] = Record,
                ["post"] = Post,
                ["check"] = Check
            };

        private static readonly ConcurrentDictionary<string, ProfileCitationKind> _values =
            new ConcurrentDictionary<string, ProfileCitationKind>(_knownValues);

        private ProfileCitationKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ProfileCitationKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ProfileCitationKind(value));
        }

        public static implicit operator ProfileCitationKind(string value) => Of(value);
        public static implicit operator string(ProfileCitationKind profilecitationkind) => profilecitationkind.Value;

        public static ProfileCitationKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ProfileCitationKind);

        public bool Equals(ProfileCitationKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}