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

    public class ExportAccountRequest
    {
        /// <summary>
        /// The account_id the workspace sends with its assessments, as one path segment (percent-encode '|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=account_id")]
        public string AccountId { get; set; } = default!;

        /// <summary>
        /// standard returns the consolidated risk response; detailed adds merged provider-neutral facts when the key has detailed scope. There is no raw mode.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=response_mode")]
        public ResponseMode? ResponseMode { get; set; }
    }
}