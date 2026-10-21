#nullable enable
namespace NoResponse.Models.Shared
{
    using NoResponse.Utils;

    public class Security
    {
        [NoResponseMetadata("security:scheme=true,type=http,subType=bearer,name=Authorization")]
        public string ApiKey { get; set; } = default!;
    }
}