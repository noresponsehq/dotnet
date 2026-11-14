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
    /// Why fewer posts were read than the screening's depth asks for while the account holds more. platform_limit: the platform shows the screening no more of them. page_limit: the pages a screening reads ran out first. rate_limited or unavailable: a later page was held back or failed; the posts before it are kept and the person is partial. Absent when the account was read to its end or to the depth.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class PostsStopped : IEquatable<PostsStopped>, IOpenEnum<string>
    {
        public static readonly PostsStopped PlatformLimit = new PostsStopped("platform_limit");
        public static readonly PostsStopped PageLimit = new PostsStopped("page_limit");
        public static readonly PostsStopped RateLimited = new PostsStopped("rate_limited");
        public static readonly PostsStopped Unavailable = new PostsStopped("unavailable");

        private static readonly Dictionary<string, PostsStopped> _knownValues =
            new Dictionary<string, PostsStopped>() {
                ["platform_limit"] = PlatformLimit,
                ["page_limit"] = PageLimit,
                ["rate_limited"] = RateLimited,
                ["unavailable"] = Unavailable
            };

        private static readonly ConcurrentDictionary<string, PostsStopped> _values =
            new ConcurrentDictionary<string, PostsStopped>(_knownValues);

        private PostsStopped(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static PostsStopped Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new PostsStopped(value));
        }

        public static implicit operator PostsStopped(string value) => Of(value);
        public static implicit operator string(PostsStopped postsstopped) => postsstopped.Value;

        public static PostsStopped[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as PostsStopped);

        public bool Equals(PostsStopped? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}