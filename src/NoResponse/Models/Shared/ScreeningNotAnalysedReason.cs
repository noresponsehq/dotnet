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
    /// Why an item read was not analysed. video: a video, whose caption alone is read. image_unavailable: the image could not be fetched from the platform. image_unsupported: not a JPEG, PNG, GIF or WebP image, or too large to send. image_limit: past the most images one person's analysis reads. refused: the model declined to read it. unfinished: the model cut its batch short or answered outside the schema, or the request was rejected. unverified: a finding cited it with words it does not contain or an image it did not describe. unspecified: the analyser gave no reason.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ScreeningNotAnalysedReason : IEquatable<ScreeningNotAnalysedReason>, IOpenEnum<string>
    {
        public static readonly ScreeningNotAnalysedReason Video = new ScreeningNotAnalysedReason("video");
        public static readonly ScreeningNotAnalysedReason ImageUnavailable = new ScreeningNotAnalysedReason("image_unavailable");
        public static readonly ScreeningNotAnalysedReason ImageUnsupported = new ScreeningNotAnalysedReason("image_unsupported");
        public static readonly ScreeningNotAnalysedReason ImageLimit = new ScreeningNotAnalysedReason("image_limit");
        public static readonly ScreeningNotAnalysedReason Refused = new ScreeningNotAnalysedReason("refused");
        public static readonly ScreeningNotAnalysedReason Unfinished = new ScreeningNotAnalysedReason("unfinished");
        public static readonly ScreeningNotAnalysedReason Unverified = new ScreeningNotAnalysedReason("unverified");
        public static readonly ScreeningNotAnalysedReason Unspecified = new ScreeningNotAnalysedReason("unspecified");

        private static readonly Dictionary<string, ScreeningNotAnalysedReason> _knownValues =
            new Dictionary<string, ScreeningNotAnalysedReason>() {
                ["video"] = Video,
                ["image_unavailable"] = ImageUnavailable,
                ["image_unsupported"] = ImageUnsupported,
                ["image_limit"] = ImageLimit,
                ["refused"] = Refused,
                ["unfinished"] = Unfinished,
                ["unverified"] = Unverified,
                ["unspecified"] = Unspecified
            };

        private static readonly ConcurrentDictionary<string, ScreeningNotAnalysedReason> _values =
            new ConcurrentDictionary<string, ScreeningNotAnalysedReason>(_knownValues);

        private ScreeningNotAnalysedReason(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ScreeningNotAnalysedReason Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ScreeningNotAnalysedReason(value));
        }

        public static implicit operator ScreeningNotAnalysedReason(string value) => Of(value);
        public static implicit operator string(ScreeningNotAnalysedReason screeningnotanalysedreason) => screeningnotanalysedreason.Value;

        public static ScreeningNotAnalysedReason[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ScreeningNotAnalysedReason);

        public bool Equals(ScreeningNotAnalysedReason? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}