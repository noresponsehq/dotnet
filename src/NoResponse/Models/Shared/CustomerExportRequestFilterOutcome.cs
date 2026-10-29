#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    [JsonConverter(typeof(OpenEnumConverter))]
    public class CustomerExportRequestFilterOutcome : IEquatable<CustomerExportRequestFilterOutcome>, IOpenEnum<string>
    {
        public static readonly CustomerExportRequestFilterOutcome Succeeded = new CustomerExportRequestFilterOutcome("succeeded");
        public static readonly CustomerExportRequestFilterOutcome Denied = new CustomerExportRequestFilterOutcome("denied");

        private static readonly Dictionary<string, CustomerExportRequestFilterOutcome> _knownValues =
            new Dictionary<string, CustomerExportRequestFilterOutcome>() {
                ["succeeded"] = Succeeded,
                ["denied"] = Denied
            };

        private static readonly ConcurrentDictionary<string, CustomerExportRequestFilterOutcome> _values =
            new ConcurrentDictionary<string, CustomerExportRequestFilterOutcome>(_knownValues);

        private CustomerExportRequestFilterOutcome(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static CustomerExportRequestFilterOutcome Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new CustomerExportRequestFilterOutcome(value));
        }

        public static implicit operator CustomerExportRequestFilterOutcome(string value) => Of(value);
        public static implicit operator string(CustomerExportRequestFilterOutcome customerexportrequestfilteroutcome) => customerexportrequestfilteroutcome.Value;

        public static CustomerExportRequestFilterOutcome[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as CustomerExportRequestFilterOutcome);

        public bool Equals(CustomerExportRequestFilterOutcome? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}