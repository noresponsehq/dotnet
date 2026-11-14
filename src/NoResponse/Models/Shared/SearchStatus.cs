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
    /// A password or hash search is counting, then counted: rows says how many breach rows hold the value, and nothing runs until a person runs it (POST .../searches/{search_id}/run). Any other search is queued, then running. completed: every source it asked answered. partial: some did not. failed: none answered or none could be asked, which says nothing about whether the value is held anywhere.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class SearchStatus : IEquatable<SearchStatus>, IOpenEnum<string>
    {
        public static readonly SearchStatus Counting = new SearchStatus("counting");
        public static readonly SearchStatus Counted = new SearchStatus("counted");
        public static readonly SearchStatus Queued = new SearchStatus("queued");
        public static readonly SearchStatus Running = new SearchStatus("running");
        public static readonly SearchStatus Completed = new SearchStatus("completed");
        public static readonly SearchStatus Partial = new SearchStatus("partial");
        public static readonly SearchStatus Failed = new SearchStatus("failed");

        private static readonly Dictionary<string, SearchStatus> _knownValues =
            new Dictionary<string, SearchStatus>() {
                ["counting"] = Counting,
                ["counted"] = Counted,
                ["queued"] = Queued,
                ["running"] = Running,
                ["completed"] = Completed,
                ["partial"] = Partial,
                ["failed"] = Failed
            };

        private static readonly ConcurrentDictionary<string, SearchStatus> _values =
            new ConcurrentDictionary<string, SearchStatus>(_knownValues);

        private SearchStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static SearchStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new SearchStatus(value));
        }

        public static implicit operator SearchStatus(string value) => Of(value);
        public static implicit operator string(SearchStatus searchstatus) => searchstatus.Value;

        public static SearchStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as SearchStatus);

        public bool Equals(SearchStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}