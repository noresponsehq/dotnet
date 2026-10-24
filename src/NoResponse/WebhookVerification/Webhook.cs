#nullable enable
namespace NoResponse.WebhookVerification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NoResponse.Utils;

    /// <summary>
    /// Thrown by <see cref="Webhook"/> for a delivery that must not be processed: answer it with a 400 and do not process it.
    /// </summary>
    public sealed class WebhookVerificationException : Exception
    {
        public const string MissingHeader = "missing_header";
        public const string DuplicateHeader = "duplicate_header";
        public const string InvalidTimestamp = "invalid_timestamp";
        public const string UnknownKey = "unknown_key";
        public const string InvalidSignature = "invalid_signature";
        public const string InvalidPayload = "invalid_payload";

        public WebhookVerificationException(string code, string message) : base(message)
        {
            Code = code;
        }

        /// <summary>Why the delivery was refused: one of the constants on this class.</summary>
        public string Code { get; }
    }

    /// <summary>Verifies webhook deliveries and parses them into <see cref="NoResponse.Models.Shared.WebhookEvent"/>.</summary>
    public static class Webhook
    {
        /// <summary>How far the NR-Webhook-Timestamp header may be from the receiver's clock.</summary>
        public static readonly TimeSpan DefaultTolerance = TimeSpan.FromSeconds(300);

        private const string SignaturePrefix = "v1=";

        private static readonly string[] HeaderNames = { "NR-Webhook-Id", "NR-Webhook-Timestamp", "NR-Webhook-Key-Id", "NR-Webhook-Signature" };

        /// <summary>
        /// Verifies a webhook delivery and parses its body.
        /// <paramref name="payload"/> is the raw request body exactly as received: re-encoding parsed JSON breaks the signature.
        /// <paramref name="headers"/> may be ASP.NET Core's Request.Headers, HttpHeaders or any map of names to values; a header repeated in any letter case, or sent as a comma-joined list, is refused.
        /// <paramref name="secrets"/> maps each key id, sent in NR-Webhook-Key-Id, to its secret, so a receiver can hold two during a rotation.
        /// </summary>
        /// <exception cref="WebhookVerificationException">The delivery must be refused.</exception>
        public static NoResponse.Models.Shared.WebhookEvent ConstructEvent<TValues>(byte[] payload, IEnumerable<KeyValuePair<string, TValues>> headers, IReadOnlyDictionary<string, string> secrets, TimeSpan? tolerance = null, DateTimeOffset? now = null)
            where TValues : IEnumerable<string ?>
        {
            var values = HeaderValues(headers.Select(h => new KeyValuePair<string, IEnumerable<string?>>(h.Key, h.Value)));
            var id = values["NR-Webhook-Id"];
            var timestamp = values["NR-Webhook-Timestamp"];
            var keyId = values["NR-Webhook-Key-Id"];
            var signature = values["NR-Webhook-Signature"];

            if (timestamp.Length > 12 || !timestamp.All(c => c >= '0' && c <= '9'))
            {
                throw new WebhookVerificationException(WebhookVerificationException.InvalidTimestamp, "NR-Webhook-Timestamp is not a Unix time in seconds");
            }
            var seconds = long.Parse(timestamp, System.Globalization.CultureInfo.InvariantCulture);
            if (seconds > DateTimeOffset.MaxValue.ToUnixTimeSeconds() || ((now ?? DateTimeOffset.UtcNow) - DateTimeOffset.FromUnixTimeSeconds(seconds)).Duration() > (tolerance ?? DefaultTolerance))
            {
                throw new WebhookVerificationException(WebhookVerificationException.InvalidTimestamp, "NR-Webhook-Timestamp is outside the tolerance");
            }

            if (!secrets.TryGetValue(keyId, out var secret))
            {
                throw new WebhookVerificationException(WebhookVerificationException.UnknownKey, "NR-Webhook-Key-Id names a key this receiver does not hold");
            }
            if (string.IsNullOrEmpty(secret))
            {
                throw new WebhookVerificationException(WebhookVerificationException.InvalidSignature, "the webhook secret is empty");
            }

            var received = signature.StartsWith(SignaturePrefix, StringComparison.Ordinal) ? Decode(signature.Substring(SignaturePrefix.Length)) : null;
            if (received == null)
            {
                throw new WebhookVerificationException(WebhookVerificationException.InvalidSignature, "NR-Webhook-Signature is malformed");
            }

            byte[] expected;
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                var before = Encoding.UTF8.GetBytes("{timestamp}.".Replace("{timestamp}", timestamp).Replace("{id}", id));
                hmac.TransformBlock(before, 0, before.Length, null, 0);
                hmac.TransformFinalBlock(payload, 0, payload.Length);
                expected = hmac.Hash!;
            }
            if (!FixedTimeEquals(expected, received))
            {
                throw new WebhookVerificationException(WebhookVerificationException.InvalidSignature, "NR-Webhook-Signature does not match");
            }

            var json = Encoding.UTF8.GetString(payload);
            if (!BodyIdMatches(json, id))
            {
                throw new WebhookVerificationException(WebhookVerificationException.InvalidPayload, "the body's id does not equal NR-Webhook-Id");
            }
            try
            {
                return ResponseBodyDeserializer.DeserializeNotNull<NoResponse.Models.Shared.WebhookEvent>(json);
            }
            catch (Exception)
            {
                throw new WebhookVerificationException(WebhookVerificationException.InvalidPayload, "the body is not a known webhook event");
            }
        }

        /// <summary>Verifies a webhook delivery and parses its body, given one value per header name.</summary>
        public static NoResponse.Models.Shared.WebhookEvent ConstructEvent(byte[] payload, IEnumerable<KeyValuePair<string, string>> headers, IReadOnlyDictionary<string, string> secrets, TimeSpan? tolerance = null, DateTimeOffset? now = null)
        {
            return ConstructEvent(payload, headers.Select(h => new KeyValuePair<string, string?[]>(h.Key, new string?[] { h.Value })), secrets, tolerance, now);
        }

        /// <summary>Verifies a webhook delivery and parses its body; the payload is encoded as UTF-8, so prefer the raw body bytes when the framework provides them.</summary>
        public static NoResponse.Models.Shared.WebhookEvent ConstructEvent<TValues>(string payload, IEnumerable<KeyValuePair<string, TValues>> headers, IReadOnlyDictionary<string, string> secrets, TimeSpan? tolerance = null, DateTimeOffset? now = null)
            where TValues : IEnumerable<string ?>
        {
            return ConstructEvent(Encoding.UTF8.GetBytes(payload), headers, secrets, tolerance, now);
        }

        /// <summary>Verifies a webhook delivery and parses its body; the payload is encoded as UTF-8.</summary>
        public static NoResponse.Models.Shared.WebhookEvent ConstructEvent(string payload, IEnumerable<KeyValuePair<string, string>> headers, IReadOnlyDictionary<string, string> secrets, TimeSpan? tolerance = null, DateTimeOffset? now = null)
        {
            return ConstructEvent(Encoding.UTF8.GetBytes(payload), headers, secrets, tolerance, now);
        }

        private static Dictionary<string, string> HeaderValues(IEnumerable<KeyValuePair<string, IEnumerable<string?>>> headers)
        {
            var all = headers.ToList();
            var values = new Dictionary<string, string>();
            foreach (var name in HeaderNames)
            {
                var found = all.Where(h => string.Equals(h.Key, name, StringComparison.OrdinalIgnoreCase))
                                .SelectMany(h => h.Value ?? Enumerable.Empty<string?>())
                                .ToList();
                if (found.Count > 1 || (found.Count == 1 && (found[0]?.IndexOf(',') ?? -1) >= 0))
                {
                    throw new WebhookVerificationException(WebhookVerificationException.DuplicateHeader, name + " is repeated");
                }
                var value = found.FirstOrDefault();
                if (string.IsNullOrEmpty(value))
                {
                    throw new WebhookVerificationException(WebhookVerificationException.MissingHeader, name + " is missing");
                }
                values[name] = value!;
            }
            return values;
        }

        private static byte[]? Decode(string text)
        {
            if (text.Length % 2 != 0)
            {
                return null;
            }
            var bytes = new byte[text.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                int high = HexValue(text[2 * i]), low = HexValue(text[2 * i + 1]);
                if (high < 0 || low < 0)
                {
                    return null;
                }
                bytes[i] = (byte)((high << 4) | low);
            }
            return bytes;
        }

        private static int HexValue(char c) =>
            c >= '0' && c <= '9' ? c - '0' : c >= 'a' && c <= 'f' ? c - 'a' + 10
                                         : c >= 'A' && c <= 'F'   ? c - 'A' + 10
                                                                  : -1;

        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            return CryptographicOperations.FixedTimeEquals(left, right);
        }

        private static bool BodyIdMatches(string json, string id)
        {
            try
            {
                using var reader = new JsonTextReader(new System.IO.StringReader(json)) { DateParseHandling = DateParseHandling.None };
                return JToken.ReadFrom(reader) is JObject body && body.TryGetValue("id", out var value) && value.Type == JTokenType.String && (string?)value == id;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
