#nullable enable
namespace NoResponse
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>Hash an API key your signup gate issued. Hand-written beside the generated client.</summary>
    public static class IssuedCredential
    {
        /// <summary>
        /// SHA-256 of an issued API key, as 64 lower-case hex characters. The key never leaves your
        /// backend: send only this hash, as issued_credential.sha256 on the assessment request. The key
        /// is hashed as its UTF-8 bytes exactly as given, with no trimming or normalisation.
        /// </summary>
        /// <exception cref="ArgumentException">The key is null or empty.</exception>
        public static string Sha256(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Issued key must not be empty.", nameof(key));
            }
            return Convert.ToHexString(SHA256.HashData(new UTF8Encoding(false).GetBytes(key))).ToLowerInvariant();
        }
    }
}
