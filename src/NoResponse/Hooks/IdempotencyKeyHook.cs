#nullable enable
namespace NoResponse.Hooks
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Sends a fresh UUID as the Idempotency-Key when the caller gave none. The hook runs once
    /// per call, before any retry, so every attempt carries the same key.
    /// </summary>
    public sealed class IdempotencyKeyHook : IBeforeRequestHook
    {
        private static readonly string[] Operations = { "createAssessment", "recordAssessmentOutcome", "recordKeyUsage", "recordAccountOutcome", "changeLists", "createPolicyEvaluation" };

        public Task<HttpRequestMessage> BeforeRequestAsync(BeforeRequestContext hookCtx, HttpRequestMessage request)
        {
            if (Array.IndexOf(Operations, hookCtx.OperationID) >= 0 && !request.Headers.Contains("Idempotency-Key"))
            {
                request.Headers.Add("Idempotency-Key", Guid.NewGuid().ToString());
            }

            return Task.FromResult(request);
        }
    }
}
