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
    /// standard returns the consolidated risk response; detailed adds merged provider-neutral facts when the key has detailed scope. There is no raw mode.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ResponseMode : IEquatable<ResponseMode>, IOpenEnum<string>
    {
        public static readonly ResponseMode Standard = new ResponseMode("standard");
        public static readonly ResponseMode Detailed = new ResponseMode("detailed");

        private static readonly Dictionary<string, ResponseMode> _knownValues =
            new Dictionary<string, ResponseMode>() {
                ["standard"] = Standard,
                ["detailed"] = Detailed
            };

        private static readonly ConcurrentDictionary<string, ResponseMode> _values =
            new ConcurrentDictionary<string, ResponseMode>(_knownValues);

        private ResponseMode(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ResponseMode Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ResponseMode(value));
        }

        public static implicit operator ResponseMode(string value) => Of(value);
        public static implicit operator string(ResponseMode responsemode) => responsemode.Value;

        public static ResponseMode[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ResponseMode);

        public bool Equals(ResponseMode? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}