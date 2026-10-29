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

    public class ListAuditEventsRequest
    {
        /// <summary>
        /// Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=workspace_id")]
        public string WorkspaceId { get; set; } = default!;

        /// <summary>
        /// Events per page.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=limit")]
        public long? Limit { get; set; } = 20;

        /// <summary>
        /// next_cursor from the previous page, unchanged; keep every filter unchanged.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Affected scope. API keys can request only their own environment. Omit to read the scopes the caller is allowed.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=environment")]
        public Models.Shared.Environment? Environment { get; set; }

        /// <summary>
        /// Exact action, such as key.created or account.deleted.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=action")]
        public string? Action { get; set; }

        /// <summary>
        /// Exact external user id, public API key id or console credential id.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=actor")]
        public string? Actor { get; set; }

        /// <summary>
        /// Exact target kind, such as key, member, setting or workspace.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=target_type")]
        public string? TargetType { get; set; }

        /// <summary>
        /// Public target identifier. Audit targets exclude subject identifiers.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=target_id")]
        public string? TargetId { get; set; }

        /// <summary>
        /// Exact outcome.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=outcome")]
        public CustomerExportRequestFilterOutcome? Outcome { get; set; }

        /// <summary>
        /// Inclusive creation timestamp in RFC3339 format.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=since")]
        public string? Since { get; set; }

        /// <summary>
        /// Exclusive creation timestamp in RFC3339 format; must follow since when both are supplied.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=until")]
        public string? Until { get; set; }
    }
}