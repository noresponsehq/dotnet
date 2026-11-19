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
    /// account_added and account_removed: an account or profile appeared or disappeared. breach_added and breach_removed: a breach record appeared or disappeared. identifier_added and identifier_removed: an email, phone, username, name or account id on an account the person already held. field_changed: another field on such an account. finding_added: a new finding. posts_added: new posts, counted in count.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class PersonChangeKind : IEquatable<PersonChangeKind>, IOpenEnum<string>
    {
        public static readonly PersonChangeKind AccountAdded = new PersonChangeKind("account_added");
        public static readonly PersonChangeKind AccountRemoved = new PersonChangeKind("account_removed");
        public static readonly PersonChangeKind BreachAdded = new PersonChangeKind("breach_added");
        public static readonly PersonChangeKind BreachRemoved = new PersonChangeKind("breach_removed");
        public static readonly PersonChangeKind IdentifierAdded = new PersonChangeKind("identifier_added");
        public static readonly PersonChangeKind IdentifierRemoved = new PersonChangeKind("identifier_removed");
        public static readonly PersonChangeKind FieldChanged = new PersonChangeKind("field_changed");
        public static readonly PersonChangeKind FindingAdded = new PersonChangeKind("finding_added");
        public static readonly PersonChangeKind PostsAdded = new PersonChangeKind("posts_added");

        private static readonly Dictionary<string, PersonChangeKind> _knownValues =
            new Dictionary<string, PersonChangeKind>() {
                ["account_added"] = AccountAdded,
                ["account_removed"] = AccountRemoved,
                ["breach_added"] = BreachAdded,
                ["breach_removed"] = BreachRemoved,
                ["identifier_added"] = IdentifierAdded,
                ["identifier_removed"] = IdentifierRemoved,
                ["field_changed"] = FieldChanged,
                ["finding_added"] = FindingAdded,
                ["posts_added"] = PostsAdded
            };

        private static readonly ConcurrentDictionary<string, PersonChangeKind> _values =
            new ConcurrentDictionary<string, PersonChangeKind>(_knownValues);

        private PersonChangeKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static PersonChangeKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new PersonChangeKind(value));
        }

        public static implicit operator PersonChangeKind(string value) => Of(value);
        public static implicit operator string(PersonChangeKind personchangekind) => personchangekind.Value;

        public static PersonChangeKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as PersonChangeKind);

        public bool Equals(PersonChangeKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}