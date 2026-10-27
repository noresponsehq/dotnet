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
    /// email matches one address, whatever the case of its local part. email_domain matches every address of the registrable domain and of its subdomains.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ListEntryKind : IEquatable<ListEntryKind>, IOpenEnum<string>
    {
        public static readonly ListEntryKind Email = new ListEntryKind("email");
        public static readonly ListEntryKind EmailDomain = new ListEntryKind("email_domain");

        private static readonly Dictionary<string, ListEntryKind> _knownValues =
            new Dictionary<string, ListEntryKind>() {
                ["email"] = Email,
                ["email_domain"] = EmailDomain
            };

        private static readonly ConcurrentDictionary<string, ListEntryKind> _values =
            new ConcurrentDictionary<string, ListEntryKind>(_knownValues);

        private ListEntryKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ListEntryKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ListEntryKind(value));
        }

        public static implicit operator ListEntryKind(string value) => Of(value);
        public static implicit operator string(ListEntryKind listentrykind) => listentrykind.Value;

        public static ListEntryKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ListEntryKind);

        public bool Equals(ListEntryKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}