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

    public class CommentReviewRequest
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

        [NoResponseMetadata("pathParam:style=simple,explode=false,name=review_id")]
        public string ReviewId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public ReviewCommentRequest Body { get; set; } = new();
    }
}