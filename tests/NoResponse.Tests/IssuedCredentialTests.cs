namespace NoResponse.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Xunit;

    public class IssuedCredentialTests
    {
        public static IEnumerable<object[]> Vectors()
        {
            var file = JsonConvert.DeserializeObject<JObject>(
                File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "fixtures", "issued-credential-vectors.json")))!;
            foreach (var c in (JArray)file["cases"]!)
            {
                yield return new object[] { c["key"]!.Value<string>()!, c["sha256"]!.Value<string>()! };
            }
        }

        [Theory]
        [MemberData(nameof(Vectors))]
        public void MatchesTheSharedVectors(string key, string sha256)
        {
            Assert.Equal(sha256, IssuedCredential.Sha256(key));
        }

        [Fact]
        public void RefusesAnEmptyKey()
        {
            Assert.Throws<ArgumentException>(() => IssuedCredential.Sha256(""));
        }
    }
}
