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
    public class InvestigationStatus : IEquatable<InvestigationStatus>, IOpenEnum<string>
    {
        public static readonly InvestigationStatus Queued = new InvestigationStatus("queued");
        public static readonly InvestigationStatus Running = new InvestigationStatus("running");
        public static readonly InvestigationStatus Completed = new InvestigationStatus("completed");
        public static readonly InvestigationStatus Partial = new InvestigationStatus("partial");
        public static readonly InvestigationStatus Failed = new InvestigationStatus("failed");
        public static readonly InvestigationStatus Cancelled = new InvestigationStatus("cancelled");
        public static readonly InvestigationStatus Unknown = new InvestigationStatus("unknown");

        private static readonly Dictionary<string, InvestigationStatus> _knownValues =
            new Dictionary<string, InvestigationStatus>() {
                ["queued"] = Queued,
                ["running"] = Running,
                ["completed"] = Completed,
                ["partial"] = Partial,
                ["failed"] = Failed,
                ["cancelled"] = Cancelled,
                ["unknown"] = Unknown
            };

        private static readonly ConcurrentDictionary<string, InvestigationStatus> _values =
            new ConcurrentDictionary<string, InvestigationStatus>(_knownValues);

        private InvestigationStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static InvestigationStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new InvestigationStatus(value));
        }

        public static implicit operator InvestigationStatus(string value) => Of(value);
        public static implicit operator string(InvestigationStatus investigationstatus) => investigationstatus.Value;

        public static InvestigationStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as InvestigationStatus);

        public bool Equals(InvestigationStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}