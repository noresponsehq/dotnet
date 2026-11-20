using Newtonsoft.Json.Linq;
using NoResponse.Models.Shared;
using NoResponse.Utils;
using Xunit;

public class GroupDatesTests
{
    [Fact]
    public void DatesDistinguishOmissionNullAndValues()
    {
        var update = new GroupUpdate();
        Assert.Null(JObject.Parse(Utilities.SerializeJSON(update))["starts_at"]);
        update.StartsAt = OptionalNullable<string?>.Set(null);
        update.EndsAt = OptionalNullable<string?>.Set(null);
        var cleared = JObject.Parse(Utilities.SerializeJSON(update));
        Assert.Equal(JTokenType.Null, cleared["starts_at"]!.Type);
        Assert.Equal(JTokenType.Null, cleared["ends_at"]!.Type);
        update.StartsAt = "2026-12-03T09:00:00Z";
        Assert.Contains("2026-12-03T09:00:00Z", Utilities.SerializeJSON(update));
        var group = Newtonsoft.Json.JsonConvert.DeserializeObject<Group>("{\"id\":\"grp_example\",\"environment\":\"test\",\"name\":\"Harbour building\",\"type\":\"event\",\"attribute\":\"\",\"starts_at\":null,\"ends_at\":null,\"principals\":[],\"topics\":[],\"people\":0,\"version\":1,\"created_by\":\"test\",\"created_at\":\"2026-11-15T10:00:00Z\",\"updated_at\":\"2026-11-15T10:00:00Z\"}");
        Assert.Null(group!.StartsAt);
        Assert.Null(group.EndsAt);
        var other = Newtonsoft.Json.JsonConvert.DeserializeObject<OtherGroup>("{\"group_id\":\"grp_building\",\"group_name\":\"Harbour building\",\"group_starts_at\":null,\"person_id\":\"per_example\",\"matched_on\":[\"person_id\"],\"match_state\":\"unscreened\",\"decision\":null}");
        Assert.Null(other!.GroupStartsAt);
    }
}
