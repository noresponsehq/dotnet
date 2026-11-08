#nullable enable
namespace NoResponse.Models.Operations
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class ListAttendeesRequest
    {
        /// <summary>
        /// Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=workspace_id")]
        public string WorkspaceId { get; set; } = default!;

        /// <summary>
        /// The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=environment")]
        public KeyEnvironment Environment { get; set; } = default!;

        [NoResponseMetadata("pathParam:style=simple,explode=false,name=event_id")]
        public string EventId { get; set; } = default!;

        /// <summary>
        /// At most this many attendees.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=limit")]
        public long? Limit { get; set; } = 100;

        /// <summary>
        /// Read attendees after this one.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=after")]
        public string? After { get; set; }

        /// <summary>
        /// Only attendees whose highest finding has this band; none for attendees with no finding.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=band")]
        public ListAttendeesBand? Band { get; set; }

        /// <summary>
        /// Only attendees with this decision; undecided for none yet.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=decision")]
        public ListAttendeesDecision? Decision { get; set; }
    }
}