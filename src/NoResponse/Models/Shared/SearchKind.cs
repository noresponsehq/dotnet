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
    /// The kind of the value searched. A domain search finds the leaked rows of every email address at the domain.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class SearchKind : IEquatable<SearchKind>, IOpenEnum<string>
    {
        public static readonly SearchKind Email = new SearchKind("email");
        public static readonly SearchKind Phone = new SearchKind("phone");
        public static readonly SearchKind Username = new SearchKind("username");
        public static readonly SearchKind PersonName = new SearchKind("person_name");
        public static readonly SearchKind Password = new SearchKind("password");
        public static readonly SearchKind Hash = new SearchKind("hash");
        public static readonly SearchKind Ip = new SearchKind("ip");
        public static readonly SearchKind Domain = new SearchKind("domain");

        private static readonly Dictionary<string, SearchKind> _knownValues =
            new Dictionary<string, SearchKind>() {
                ["email"] = Email,
                ["phone"] = Phone,
                ["username"] = Username,
                ["person_name"] = PersonName,
                ["password"] = Password,
                ["hash"] = Hash,
                ["ip"] = Ip,
                ["domain"] = Domain
            };

        private static readonly ConcurrentDictionary<string, SearchKind> _values =
            new ConcurrentDictionary<string, SearchKind>(_knownValues);

        private SearchKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static SearchKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new SearchKind(value));
        }

        public static implicit operator SearchKind(string value) => Of(value);
        public static implicit operator string(SearchKind searchkind) => searchkind.Value;

        public static SearchKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as SearchKind);

        public bool Equals(SearchKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}