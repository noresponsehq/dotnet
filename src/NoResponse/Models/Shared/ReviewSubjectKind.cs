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
    public class ReviewSubjectKind : IEquatable<ReviewSubjectKind>, IOpenEnum<string>
    {
        public static readonly ReviewSubjectKind Account = new ReviewSubjectKind("account");
        public static readonly ReviewSubjectKind Assessment = new ReviewSubjectKind("assessment");
        public static readonly ReviewSubjectKind IssuedCredential = new ReviewSubjectKind("issued_credential");

        private static readonly Dictionary<string, ReviewSubjectKind> _knownValues =
            new Dictionary<string, ReviewSubjectKind>() {
                ["account"] = Account,
                ["assessment"] = Assessment,
                ["issued_credential"] = IssuedCredential
            };

        private static readonly ConcurrentDictionary<string, ReviewSubjectKind> _values =
            new ConcurrentDictionary<string, ReviewSubjectKind>(_knownValues);

        private ReviewSubjectKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ReviewSubjectKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ReviewSubjectKind(value));
        }

        public static implicit operator ReviewSubjectKind(string value) => Of(value);
        public static implicit operator string(ReviewSubjectKind reviewsubjectkind) => reviewsubjectkind.Value;

        public static ReviewSubjectKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ReviewSubjectKind);

        public bool Equals(ReviewSubjectKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}