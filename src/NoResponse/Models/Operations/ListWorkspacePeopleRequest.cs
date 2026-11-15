#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class ListWorkspacePeopleRequest
    {
        [NoResponseMetadata("queryParam:style=form,explode=true,name=limit")]
        public long? Limit { get; set; } = 100;

        /// <summary>
        /// Pass next_cursor from the prior page.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=after")]
        public string? After { get; set; }
    }
}