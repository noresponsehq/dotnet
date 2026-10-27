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
    /// block decides block for the accounts the entry matches; allow decides allow. Block wins when both match.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ListEntryAction : IEquatable<ListEntryAction>, IOpenEnum<string>
    {
        public static readonly ListEntryAction Block = new ListEntryAction("block");
        public static readonly ListEntryAction Allow = new ListEntryAction("allow");

        private static readonly Dictionary<string, ListEntryAction> _knownValues =
            new Dictionary<string, ListEntryAction>() {
                ["block"] = Block,
                ["allow"] = Allow
            };

        private static readonly ConcurrentDictionary<string, ListEntryAction> _values =
            new ConcurrentDictionary<string, ListEntryAction>(_knownValues);

        private ListEntryAction(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ListEntryAction Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ListEntryAction(value));
        }

        public static implicit operator ListEntryAction(string value) => Of(value);
        public static implicit operator string(ListEntryAction listentryaction) => listentryaction.Value;

        public static ListEntryAction[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ListEntryAction);

        public bool Equals(ListEntryAction? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}