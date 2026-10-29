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
    public class CustomerExportStatus : IEquatable<CustomerExportStatus>, IOpenEnum<string>
    {
        public static readonly CustomerExportStatus Queued = new CustomerExportStatus("queued");
        public static readonly CustomerExportStatus Running = new CustomerExportStatus("running");
        public static readonly CustomerExportStatus Completed = new CustomerExportStatus("completed");
        public static readonly CustomerExportStatus Failed = new CustomerExportStatus("failed");
        public static readonly CustomerExportStatus Cancelled = new CustomerExportStatus("cancelled");
        public static readonly CustomerExportStatus Expired = new CustomerExportStatus("expired");

        private static readonly Dictionary<string, CustomerExportStatus> _knownValues =
            new Dictionary<string, CustomerExportStatus>() {
                ["queued"] = Queued,
                ["running"] = Running,
                ["completed"] = Completed,
                ["failed"] = Failed,
                ["cancelled"] = Cancelled,
                ["expired"] = Expired
            };

        private static readonly ConcurrentDictionary<string, CustomerExportStatus> _values =
            new ConcurrentDictionary<string, CustomerExportStatus>(_knownValues);

        private CustomerExportStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static CustomerExportStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new CustomerExportStatus(value));
        }

        public static implicit operator CustomerExportStatus(string value) => Of(value);
        public static implicit operator string(CustomerExportStatus customerexportstatus) => customerexportstatus.Value;

        public static CustomerExportStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as CustomerExportStatus);

        public bool Equals(CustomerExportStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}