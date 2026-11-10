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
    public class MediaType : IEquatable<MediaType>, IOpenEnum<string>
    {
        public static readonly MediaType Image = new MediaType("image");
        public static readonly MediaType Video = new MediaType("video");

        private static readonly Dictionary<string, MediaType> _knownValues =
            new Dictionary<string, MediaType>() {
                ["image"] = Image,
                ["video"] = Video
            };

        private static readonly ConcurrentDictionary<string, MediaType> _values =
            new ConcurrentDictionary<string, MediaType>(_knownValues);

        private MediaType(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static MediaType Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new MediaType(value));
        }

        public static implicit operator MediaType(string value) => Of(value);
        public static implicit operator string(MediaType mediatype) => mediatype.Value;

        public static MediaType[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as MediaType);

        public bool Equals(MediaType? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}