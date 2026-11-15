#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class ListPeopleRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=group_id")]
        public string GroupId { get; set; } = default!;

        /// <summary>
        /// At most this many people.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=limit")]
        public long? Limit { get; set; } = 100;

        /// <summary>
        /// Read people after this one.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=after")]
        public string? After { get; set; }

        /// <summary>
        /// Only people whose highest finding has this band; none for people with no finding.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=band")]
        public ListPeopleBand? Band { get; set; }

        /// <summary>
        /// Only people with this decision; undecided for none yet.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=decision")]
        public ListPeopleDecision? Decision { get; set; }

        /// <summary>
        /// Only people with this role; unstated for people whose role was not given.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=role")]
        public ListPeopleRole? Role { get; set; }
    }
}