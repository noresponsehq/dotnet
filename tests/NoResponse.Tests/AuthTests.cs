namespace NoResponse.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using Xunit;

    public class AuthTests
    {
        private static JObject Fixture(string name) =>
            JsonConvert.DeserializeObject<JObject>(
                File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "fixtures", name)),
                new JsonSerializerSettings { DateParseHandling = DateParseHandling.None })!;

        public static IEnumerable<object[]> Vectors()
        {
            foreach (var vector in (JArray)Fixture("auth-context-vectors.json")["cases"]!)
            {
                yield return new object[] { vector["name"]!.Value<string>()!, vector.ToString(Formatting.None) };
            }
        }

        private static AuthContext Run(JObject vector)
        {
            var claims = (JObject)vector["claims"]!;
            var overrides = (JObject)vector["overrides"]!;
            return vector["provider"]!.Value<string>() switch
            {
                "workos" => Auth.FromWorkOS(claims, overrides),
                "clerk" => Auth.FromClerk(claims, overrides),
                "auth0" => Auth.FromAuth0(claims, overrides),
                "supabase" => Auth.FromSupabase(claims, overrides),
                "firebase" => Auth.FromFirebase(claims, overrides),
                "cognito" => Auth.FromCognito(claims, overrides),
                "entra" => Auth.FromEntra(claims, overrides),
                "keycloak" => Auth.FromKeycloak(claims, overrides),
                "ory" => Auth.FromOry(claims, overrides),
                "ory_oauth2" => Auth.FromOryOAuth2(claims, overrides),
                var provider => Auth.FromOIDC(provider!, claims, overrides),
            };
        }

        [Theory]
        [MemberData(nameof(Vectors))]
        public void SharedVector(string name, string json)
        {
            var vector = JsonConvert.DeserializeObject<JObject>(json, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None })!;
            Assert.NotEmpty(name);
            if (vector["error"] != null)
            {
                Assert.Throws<ArgumentException>(() => Run(vector));
                return;
            }

            var got = JsonConvert.DeserializeObject<JObject>(Utilities.SerializeJSON(Run(vector)), new JsonSerializerSettings { DateParseHandling = DateParseHandling.None })!;
            Assert.True(JToken.DeepEquals(vector["expect"], got), name + "\nwant " + vector["expect"] + "\ngot  " + got);
        }

        [Fact]
        public void EmbeddedClaimMapIsTheContractMap()
        {
            var table = Fixture("auth-context-claim-map.json");
            table.Remove("description");
            table.Remove("rules");
            foreach (var provider in table["providers"]!.Children<JProperty>())
            {
                ((JObject)provider.Value).Remove("notes");
            }

            Assert.True(JToken.DeepEquals(table, Auth.ClaimMap));
        }
    }
}
