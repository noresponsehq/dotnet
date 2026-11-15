namespace NoResponse.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using Xunit;

    /// <summary>A person's report returns the contract's worked records as typed models.</summary>
    public class RecordsTests
    {
        private static readonly string Body = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "fixtures", "valid", "person-report-records.json"));

        private sealed class Transport : NoResponseHttpClient
        {
            public string? Path;

            public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken? cancellationToken = null)
            {
                Path = request.RequestUri!.AbsolutePath;
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(Body, Encoding.UTF8, "application/json"), RequestMessage = request });
            }
        }

        [Fact]
        public async Task APersonsReportReturnsTypedRecords()
        {
            var person = JObject.Parse(Body)["person"]!;
            string group = (string)person["group_id"]!, id = (string)person["id"]!;

            var transport = new Transport();
            var client = new NoResponseClient(apiKey: "nrk_test_records_fixture", serverUrl: "https://api.example.test", client: transport);
            var response = await client.Groups.GetPersonAsync(group, id);
            Assert.Equal($"/v1/groups/{group}/people/{id}", transport.Path);

            var lookup = response.Result.Lookup!;
            var account = lookup.Records![0];
            var row = lookup.Records[1];

            Assert.Equal("platform", account.Source.Kind);
            Assert.Equal("github.com", account.Source.Site);
            var login = account.Fields.Single(field => field.Field == "login");
            Assert.Equal(RecordKind.Username, login.Kind);
            Assert.Equal(RecordRole.Handle, login.Role);
            Assert.Equal("jordan-ellis", login.Value);
            Assert.Equal(Rating.Distinctive, login.Distinctive!.Rating);
            Assert.Equal("Jordan Ellis", account.Fields.Single(field => field.Field == "name").Value);
            Assert.Null(account.Fields.Single(field => field.Field == "hireable").Role);
            Assert.Equal(0.74, account.Likelihood);
            Assert.Equal(new[] { "name", "twitter_username" }, account.Agreed);
            Assert.True(account.Joined);
            Assert.Equal(Verdict.Joined, account.Verdict);

            Assert.Equal("site_breach", row.Source.Kind);
            Assert.Equal("FORUM_EXAMPLE_NET_2019", row.Source.Table);
            Assert.Equal("2019-03", row.Source.Date);
            var password = row.Fields.Single(field => field.Field == "password");
            Assert.Equal(RecordKind.Password, password.Kind);
            Assert.Equal("Leeds1990!", password.Value);
            Assert.Equal("bcrypt", row.Fields.Single(field => field.Field == "password_hash").Detail);
            var lastIp = row.Fields.Single(field => field.Field == "lastip");
            Assert.Equal(RecordRole.LastIp, lastIp.Role);
            Assert.Equal("203.0.113.5", lastIp.Value);
            var birthday = row.Fields.Single(field => field.Field == "birthday");
            Assert.Null(birthday.Value);
            Assert.Equal("07/03/1990", birthday.Original);
            Assert.Equal(0.99, row.Likelihood);
            Assert.Null(row.Verdict);

            var remaining = lookup.Remaining![0];
            Assert.Equal("credential_list", remaining.Source.Kind);
            Assert.Equal("username", remaining.By);
            Assert.Equal(37, remaining.Rows);
        }
    }
}
