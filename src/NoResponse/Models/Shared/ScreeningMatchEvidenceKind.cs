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
    /// What matched. email, phone, supplied_handle and source_fact are hard identifiers; company, city and school are soft.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ScreeningMatchEvidenceKind : IEquatable<ScreeningMatchEvidenceKind>, IOpenEnum<string>
    {
        public static readonly ScreeningMatchEvidenceKind Email = new ScreeningMatchEvidenceKind("email");
        public static readonly ScreeningMatchEvidenceKind Phone = new ScreeningMatchEvidenceKind("phone");
        public static readonly ScreeningMatchEvidenceKind SuppliedHandle = new ScreeningMatchEvidenceKind("supplied_handle");
        public static readonly ScreeningMatchEvidenceKind SourceFact = new ScreeningMatchEvidenceKind("source_fact");
        public static readonly ScreeningMatchEvidenceKind Name = new ScreeningMatchEvidenceKind("name");
        public static readonly ScreeningMatchEvidenceKind Company = new ScreeningMatchEvidenceKind("company");
        public static readonly ScreeningMatchEvidenceKind City = new ScreeningMatchEvidenceKind("city");
        public static readonly ScreeningMatchEvidenceKind School = new ScreeningMatchEvidenceKind("school");

        private static readonly Dictionary<string, ScreeningMatchEvidenceKind> _knownValues =
            new Dictionary<string, ScreeningMatchEvidenceKind>() {
                ["email"] = Email,
                ["phone"] = Phone,
                ["supplied_handle"] = SuppliedHandle,
                ["source_fact"] = SourceFact,
                ["name"] = Name,
                ["company"] = Company,
                ["city"] = City,
                ["school"] = School
            };

        private static readonly ConcurrentDictionary<string, ScreeningMatchEvidenceKind> _values =
            new ConcurrentDictionary<string, ScreeningMatchEvidenceKind>(_knownValues);

        private ScreeningMatchEvidenceKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ScreeningMatchEvidenceKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ScreeningMatchEvidenceKind(value));
        }

        public static implicit operator ScreeningMatchEvidenceKind(string value) => Of(value);
        public static implicit operator string(ScreeningMatchEvidenceKind screeningmatchevidencekind) => screeningmatchevidencekind.Value;

        public static ScreeningMatchEvidenceKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ScreeningMatchEvidenceKind);

        public bool Equals(ScreeningMatchEvidenceKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}