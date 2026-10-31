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

    public class ListReviewsRequest
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

        [NoResponseMetadata("queryParam:style=form,explode=true,name=limit")]
        public long? Limit { get; set; } = 50;

        [NoResponseMetadata("queryParam:style=form,explode=true,name=cursor")]
        public string? Cursor { get; set; }

        [NoResponseMetadata("queryParam:style=form,explode=true,name=status")]
        public ReviewStatus? Status { get; set; }

        [NoResponseMetadata("queryParam:style=form,explode=true,name=priority")]
        public Priority? Priority { get; set; }

        [NoResponseMetadata("queryParam:style=form,explode=true,name=assignee")]
        public string? Assignee { get; set; }

        [NoResponseMetadata("queryParam:style=form,explode=true,name=unassigned")]
        public bool? Unassigned { get; set; }

        /// <summary>
        /// Filter by immutable member ID. Mutually exclusive with assignee and unassigned=true.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=assignee_principal_id")]
        public string? AssigneePrincipalId { get; set; }
    }
}