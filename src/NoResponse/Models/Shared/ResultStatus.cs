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
    public class ResultStatus : IEquatable<ResultStatus>, IOpenEnum<string>
    {
        public static readonly ResultStatus NoChange = new ResultStatus("no_change");
        public static readonly ResultStatus ProposalRequiresReview = new ResultStatus("proposal_requires_review");

        private static readonly Dictionary<string, ResultStatus> _knownValues =
            new Dictionary<string, ResultStatus>() {
                ["no_change"] = NoChange,
                ["proposal_requires_review"] = ProposalRequiresReview
            };

        private static readonly ConcurrentDictionary<string, ResultStatus> _values =
            new ConcurrentDictionary<string, ResultStatus>(_knownValues);

        private ResultStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ResultStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ResultStatus(value));
        }

        public static implicit operator ResultStatus(string value) => Of(value);
        public static implicit operator string(ResultStatus resultstatus) => resultstatus.Value;

        public static ResultStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ResultStatus);

        public bool Equals(ResultStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}