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

    public class ListCustomerExportsRequest
    {
        /// <summary>
        /// Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=workspace_id")]
        public string WorkspaceId { get; set; } = default!;

        [NoResponseMetadata("queryParam:style=form,explode=true,name=kind")]
        public CustomerExportRequestKind Kind { get; set; } = default!;

        [NoResponseMetadata("queryParam:style=form,explode=true,name=environment")]
        public Models.Shared.Environment Environment { get; set; } = default!;

        [NoResponseMetadata("queryParam:style=form,explode=true,name=limit")]
        public long? Limit { get; set; } = 25;

        [NoResponseMetadata("queryParam:style=form,explode=true,name=cursor")]
        public string? Cursor { get; set; }
    }
}