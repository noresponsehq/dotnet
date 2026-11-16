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
    /// queued: the profile is being written. written: summary, statements and leads hold it. failed: it could not be written; error_code says why and no content is returned.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class WrittenProfileStatus : IEquatable<WrittenProfileStatus>, IOpenEnum<string>
    {
        public static readonly WrittenProfileStatus Queued = new WrittenProfileStatus("queued");
        public static readonly WrittenProfileStatus Written = new WrittenProfileStatus("written");
        public static readonly WrittenProfileStatus Failed = new WrittenProfileStatus("failed");

        private static readonly Dictionary<string, WrittenProfileStatus> _knownValues =
            new Dictionary<string, WrittenProfileStatus>() {
                ["queued"] = Queued,
                ["written"] = Written,
                ["failed"] = Failed
            };

        private static readonly ConcurrentDictionary<string, WrittenProfileStatus> _values =
            new ConcurrentDictionary<string, WrittenProfileStatus>(_knownValues);

        private WrittenProfileStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static WrittenProfileStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new WrittenProfileStatus(value));
        }

        public static implicit operator WrittenProfileStatus(string value) => Of(value);
        public static implicit operator string(WrittenProfileStatus writtenprofilestatus) => writtenprofilestatus.Value;

        public static WrittenProfileStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as WrittenProfileStatus);

        public bool Equals(WrittenProfileStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}