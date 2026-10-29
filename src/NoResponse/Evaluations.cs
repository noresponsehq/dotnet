#nullable enable
namespace NoResponse
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NoResponse.Hooks;
    using NoResponse.Models.Errors;
    using NoResponse.Models.Operations;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using NoResponse.Utils.Retries;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Frozen policy evaluation evidence and reviewed publication.
    /// </summary>
    public interface IEvaluations
    {
        /// <summary>
        /// List retained policy evaluation jobs.
        /// </summary>
        /// <remarks>
        /// List retained policy evaluation jobs for this environment, newest first. At most five jobs are retained for their 24-hour evidence lifetime. Use next_before as before on the next page.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="limit">Description not available.</param>
        /// <param name="before">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListPolicyEvaluationsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">The required parameter <paramref name="workspaceId"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The Idempotency-Key was already used by this workspace, within 24 hours, with a different body. A reuse with the same body is not a conflict, even while the first request is in flight: it replays the latest assessment. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<ListPolicyEvaluationsResponse> ListAsync(
            string workspaceId,
            KeyEnvironment environment,
            long? limit = 20,
            string? before = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Freeze an evaluation cohort and queue a bounded job.
        /// </summary>
        /// <remarks>
        /// Create a bounded evaluation with an Idempotency-Key, expected policy version, fixed candidates, cohort, seed and cost assumptions. Evidence is frozen before evaluation and recorded by hashes. Limits are 1000 accounts, 50000 request attempts and 64 MiB. Capture completeness is currently unknown, so results are no_change and cannot be published. The evaluation fetches no new source evidence and changes no live policy.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="PolicyEvaluationRequest"/> parameter.</param>
        /// <param name="idempotencyKey">Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the workspace principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CreatePolicyEvaluationResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The list change body or query is invalid, or Idempotency-Key is missing, repeated or malformed. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The policy, idempotency binding, evaluation state, proposal or provenance does not match; or the environment retains five jobs. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<CreatePolicyEvaluationResponse> CreateAsync(
            string workspaceId,
            KeyEnvironment environment,
            PolicyEvaluationRequest body,
            string? idempotencyKey = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Read evaluation state and provenance.
        /// </summary>
        /// <remarks>
        /// Read a retained evaluation job and its capture and provenance metadata. Cancellation, source erasure, retention reduction and expiry can make a result unavailable.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="evaluationId">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="GetPolicyEvaluationResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="evaluationId"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The Idempotency-Key was already used by this workspace, within 24 hours, with a different body. A reuse with the same body is not a conflict, even while the first request is in flight: it replays the latest assessment. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<GetPolicyEvaluationResponse> GetAsync(
            string workspaceId,
            KeyEnvironment environment,
            string evaluationId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Read the bounded completed evaluator report.
        /// </summary>
        /// <remarks>
        /// Read the completed evaluation report, including coverage, uncertainty and candidate results. Unknown capture completeness produces no_change. A completed result is not an automatic policy change.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="evaluationId">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="GetPolicyEvaluationResultResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="evaluationId"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The policy, idempotency binding, evaluation state, proposal or provenance does not match; or the environment retains five jobs. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<GetPolicyEvaluationResultResponse> GetResultAsync(
            string workspaceId,
            KeyEnvironment environment,
            string evaluationId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Cancel processing and destroy retained evidence and report.
        /// </summary>
        /// <remarks>
        /// Cancel a queued or running evaluation and invalidate its stored evidence. The operation does not change policy. Send an empty JSON object.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="evaluationId">Description not available.</param>
        /// <param name="body">A <see cref="PolicyEvaluationCancelRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CancelPolicyEvaluationResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="evaluationId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The Idempotency-Key was already used by this workspace, within 24 hours, with a different body. A reuse with the same body is not a conflict, even while the first request is in flight: it replays the latest assessment. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<CancelPolicyEvaluationResponse> CancelAsync(
            string workspaceId,
            KeyEnvironment environment,
            string evaluationId,
            PolicyEvaluationCancelRequest body,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Publish an explicitly reviewed proposal with exact provenance.
        /// </summary>
        /// <remarks>
        /// Publish an explicitly reviewed candidate with its exact manifest, input and result hashes and unchanged baseline policy version. Requires settings plus policy_publish, or legacy management. Publication creates a fresh policy version and immutable provenance. no_change, expired or invalidated results are refused.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="evaluationId">Description not available.</param>
        /// <param name="body">A <see cref="PolicyEvaluationPublishRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="PublishPolicyEvaluationResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="evaluationId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The policy, idempotency binding, evaluation state, proposal or provenance does not match; or the environment retains five jobs. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<PublishPolicyEvaluationResponse> PublishAsync(
            string workspaceId,
            KeyEnvironment environment,
            string evaluationId,
            PolicyEvaluationPublishRequest body,
            CancellationToken? cancellationToken = null
        );
    }

    /// <summary>
    /// Frozen policy evaluation evidence and reviewed publication.
    /// </summary>
    public class Evaluations : IEvaluations
    {
        /// <summary>
        /// SDK Configuration.
        /// <see cref="SDKConfig"/>
        /// </summary>
        public SDKConfig SDKConfiguration { get; private set; }

        public Evaluations(SDKConfig config)
        {
            SDKConfiguration = config;
        }

        /// <summary>
        /// List retained policy evaluation jobs.
        /// </summary>
        /// <remarks>
        /// List retained policy evaluation jobs for this environment, newest first. At most five jobs are retained for their 24-hour evidence lifetime. Use next_before as before on the next page.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="limit">Description not available.</param>
        /// <param name="before">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListPolicyEvaluationsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">The required parameter <paramref name="workspaceId"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The Idempotency-Key was already used by this workspace, within 24 hours, with a different body. A reuse with the same body is not a conflict, even while the first request is in flight: it replays the latest assessment. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<ListPolicyEvaluationsResponse> ListAsync(
            string workspaceId,
            KeyEnvironment environment,
            long? limit = 20,
            string? before = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));

            var request = new ListPolicyEvaluationsRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                Limit = limit,
                Before = before,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/evaluations/{environment}", request, null);

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, urlString);
            httpRequest.Headers.Add("user-agent", SDKConfiguration.UserAgent);

            if (!httpRequest.Headers.Contains("Accept"))
            {
                httpRequest.Headers.Add("Accept", "application/json");
            }

            if (SDKConfiguration.SecuritySource != null)
            {
                httpRequest = new SecurityMetadata(SDKConfiguration.SecuritySource).Apply(httpRequest);
            }

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "listPolicyEvaluations", null, SDKConfiguration.SecuritySource, cancellationToken);

            httpRequest = await this.SDKConfiguration.Hooks.BeforeRequestAsync(new BeforeRequestContext(hookCtx), httpRequest);
            if (retryConfig == null)
            {
                if (this.SDKConfiguration.RetryConfig != null)
                {
                    retryConfig = this.SDKConfiguration.RetryConfig;
                }
                else
                {
                    var backoff = new BackoffStrategy(
                        initialIntervalMs: 500L,
                        maxIntervalMs: 8000L,
                        maxElapsedTimeMs: 30000L,
                        exponent: 2
                    );
                    retryConfig = new RetryConfig(
                        strategy: RetryConfig.RetryStrategy.BACKOFF,
                        backoff: backoff,
                        retryConnectionErrors: true
                    );
                }
            }

            List<string> statusCodes = new List<string> {
                "408",
                "429",
                "5XX",
            };

            Func<Task<HttpResponseMessage>> retrySend = async () =>
            {
                var _httpRequest = await SDKConfiguration.Client.CloneAsync(httpRequest);
                return await SDKConfiguration.Client.SendAsync(_httpRequest, cancellationToken);
            };
            var retries = new NoResponse.Utils.Retries.Retries(retrySend, retryConfig, statusCodes, cancellationToken);

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await retries.Run();
                int _statusCode = (int)httpResponse.StatusCode;

                if (_statusCode >= 400 && _statusCode < 500 || _statusCode >= 500 && _statusCode < 600)
                {
                    var _httpResponse = await this.SDKConfiguration.Hooks.AfterErrorAsync(new AfterErrorContext(hookCtx), httpResponse, null);
                    if (_httpResponse != null)
                    {
                        httpResponse = _httpResponse;
                    }
                }
            }
            catch (Exception _hookError)
            {
                var _httpResponse = await this.SDKConfiguration.Hooks.AfterErrorAsync(new AfterErrorContext(hookCtx), null, _hookError);
                if (_httpResponse != null)
                {
                    httpResponse = _httpResponse;
                }
                else
                {
                    throw;
                }
            }

            httpResponse = await this.SDKConfiguration.Hooks.AfterSuccessAsync(new AfterSuccessContext(hookCtx), httpResponse);

            Func<Task<ListPolicyEvaluationsResponse?>> nextFunc = async delegate()
            {
                var body = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());
                var nextCursorToken = body.SelectToken("$.next_before");

                if (nextCursorToken == null)
                {
                    return null;
                }

                var nextCursor = nextCursorToken.Value<string>();
                if (string.IsNullOrWhiteSpace(nextCursor))
                {
                    return null;
                }

                return await ListAsync(
                    workspaceId: request.WorkspaceId,
                    environment: request.Environment,
                    limit: request.Limit,
                    before: nextCursor,
                    retryConfig: retryConfig
                );
            };

            var contentType = httpResponse.Content.Headers.ContentType?.MediaType;
            int responseStatusCode = (int)httpResponse.StatusCode;
            if (responseStatusCode == 200)
            {
                if (Utilities.IsContentTypeMatch("application/json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    PolicyEvaluationList obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<PolicyEvaluationList>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into PolicyEvaluationList.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new ListPolicyEvaluationsResponse() {
                        Result = result,
                        Next = nextFunc,
                        Headers = Utilities.CollectHeaders(httpResponse.Headers)
                    };
                    return response;
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 400)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    BadRequestExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<BadRequestExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into BadRequestExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new BadRequestException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 401)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    AuthenticationExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<AuthenticationExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into AuthenticationExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new AuthenticationException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 403)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    PermissionDeniedExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<PermissionDeniedExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into PermissionDeniedExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new PermissionDeniedException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 404)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    NotFoundExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<NotFoundExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into NotFoundExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new NotFoundException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 409)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    ConflictExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<ConflictExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ConflictExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new ConflictException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 429)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    RateLimitExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<RateLimitExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into RateLimitExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new RateLimitException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode >= 400 && responseStatusCode < 500)
            {
                throw new Models.Errors.APIStatusException("API error occurred", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode >= 500 && responseStatusCode < 600)
            {
                throw new Models.Errors.APIStatusException("API error occurred", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }

            throw new Models.Errors.APIStatusException("Unknown status code received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Freeze an evaluation cohort and queue a bounded job.
        /// </summary>
        /// <remarks>
        /// Create a bounded evaluation with an Idempotency-Key, expected policy version, fixed candidates, cohort, seed and cost assumptions. Evidence is frozen before evaluation and recorded by hashes. Limits are 1000 accounts, 50000 request attempts and 64 MiB. Capture completeness is currently unknown, so results are no_change and cannot be published. The evaluation fetches no new source evidence and changes no live policy.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="PolicyEvaluationRequest"/> parameter.</param>
        /// <param name="idempotencyKey">Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the workspace principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CreatePolicyEvaluationResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The list change body or query is invalid, or Idempotency-Key is missing, repeated or malformed. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The policy, idempotency binding, evaluation state, proposal or provenance does not match; or the environment retains five jobs. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<CreatePolicyEvaluationResponse> CreateAsync(
            string workspaceId,
            KeyEnvironment environment,
            PolicyEvaluationRequest body,
            string? idempotencyKey = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new CreatePolicyEvaluationRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                Body = body,
                IdempotencyKey = idempotencyKey,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/evaluations/{environment}", request, null);

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, urlString);
            httpRequest.Headers.Add("user-agent", SDKConfiguration.UserAgent);
            HeaderSerializer.PopulateHeaders(ref httpRequest, request);

            if (!httpRequest.Headers.Contains("Accept"))
            {
                httpRequest.Headers.Add("Accept", "application/json");
            }

            var serializedBody = RequestBodySerializer.Serialize(request, "Body", "json", false, false);
            if (serializedBody != null)
            {
                httpRequest.Content = serializedBody;
            }

            if (SDKConfiguration.SecuritySource != null)
            {
                httpRequest = new SecurityMetadata(SDKConfiguration.SecuritySource).Apply(httpRequest);
            }

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "createPolicyEvaluation", null, SDKConfiguration.SecuritySource, cancellationToken);

            httpRequest = await this.SDKConfiguration.Hooks.BeforeRequestAsync(new BeforeRequestContext(hookCtx), httpRequest);
            if (retryConfig == null)
            {
                if (this.SDKConfiguration.RetryConfig != null)
                {
                    retryConfig = this.SDKConfiguration.RetryConfig;
                }
                else
                {
                    var backoff = new BackoffStrategy(
                        initialIntervalMs: 500L,
                        maxIntervalMs: 8000L,
                        maxElapsedTimeMs: 30000L,
                        exponent: 2
                    );
                    retryConfig = new RetryConfig(
                        strategy: RetryConfig.RetryStrategy.BACKOFF,
                        backoff: backoff,
                        retryConnectionErrors: true
                    );
                }
            }

            List<string> statusCodes = new List<string> {
                "408",
                "429",
                "5XX",
            };

            Func<Task<HttpResponseMessage>> retrySend = async () =>
            {
                var _httpRequest = await SDKConfiguration.Client.CloneAsync(httpRequest);
                return await SDKConfiguration.Client.SendAsync(_httpRequest, cancellationToken);
            };
            var retries = new NoResponse.Utils.Retries.Retries(retrySend, retryConfig, statusCodes, cancellationToken);

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await retries.Run();
                int _statusCode = (int)httpResponse.StatusCode;

                if (_statusCode >= 400 && _statusCode < 500 || _statusCode >= 500 && _statusCode < 600)
                {
                    var _httpResponse = await this.SDKConfiguration.Hooks.AfterErrorAsync(new AfterErrorContext(hookCtx), httpResponse, null);
                    if (_httpResponse != null)
                    {
                        httpResponse = _httpResponse;
                    }
                }
            }
            catch (Exception _hookError)
            {
                var _httpResponse = await this.SDKConfiguration.Hooks.AfterErrorAsync(new AfterErrorContext(hookCtx), null, _hookError);
                if (_httpResponse != null)
                {
                    httpResponse = _httpResponse;
                }
                else
                {
                    throw;
                }
            }

            httpResponse = await this.SDKConfiguration.Hooks.AfterSuccessAsync(new AfterSuccessContext(hookCtx), httpResponse);

            var contentType = httpResponse.Content.Headers.ContentType?.MediaType;
            int responseStatusCode = (int)httpResponse.StatusCode;
            if (responseStatusCode == 202)
            {
                if (Utilities.IsContentTypeMatch("application/json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    PolicyEvaluation obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<PolicyEvaluation>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into PolicyEvaluation.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new CreatePolicyEvaluationResponse() {
                        Result = result,
                        Headers = Utilities.CollectHeaders(httpResponse.Headers)
                    };
                    return response;
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 400)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    BadRequestExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<BadRequestExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into BadRequestExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new BadRequestException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 401)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    AuthenticationExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<AuthenticationExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into AuthenticationExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new AuthenticationException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 403)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    PermissionDeniedExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<PermissionDeniedExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into PermissionDeniedExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new PermissionDeniedException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 404)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    NotFoundExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<NotFoundExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into NotFoundExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new NotFoundException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 409)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    ConflictExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<ConflictExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ConflictExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new ConflictException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 429)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    RateLimitExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<RateLimitExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into RateLimitExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new RateLimitException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode >= 400 && responseStatusCode < 500)
            {
                throw new Models.Errors.APIStatusException("API error occurred", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode >= 500 && responseStatusCode < 600)
            {
                throw new Models.Errors.APIStatusException("API error occurred", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }

            throw new Models.Errors.APIStatusException("Unknown status code received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Read evaluation state and provenance.
        /// </summary>
        /// <remarks>
        /// Read a retained evaluation job and its capture and provenance metadata. Cancellation, source erasure, retention reduction and expiry can make a result unavailable.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="evaluationId">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="GetPolicyEvaluationResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="evaluationId"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The Idempotency-Key was already used by this workspace, within 24 hours, with a different body. A reuse with the same body is not a conflict, even while the first request is in flight: it replays the latest assessment. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<GetPolicyEvaluationResponse> GetAsync(
            string workspaceId,
            KeyEnvironment environment,
            string evaluationId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (evaluationId == null) throw new ArgumentNullException(nameof(evaluationId));

            var request = new GetPolicyEvaluationRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                EvaluationId = evaluationId,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/evaluations/{environment}/{evaluation_id}", request, null);

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, urlString);
            httpRequest.Headers.Add("user-agent", SDKConfiguration.UserAgent);

            if (!httpRequest.Headers.Contains("Accept"))
            {
                httpRequest.Headers.Add("Accept", "application/json");
            }

            if (SDKConfiguration.SecuritySource != null)
            {
                httpRequest = new SecurityMetadata(SDKConfiguration.SecuritySource).Apply(httpRequest);
            }

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "getPolicyEvaluation", null, SDKConfiguration.SecuritySource, cancellationToken);

            httpRequest = await this.SDKConfiguration.Hooks.BeforeRequestAsync(new BeforeRequestContext(hookCtx), httpRequest);
            if (retryConfig == null)
            {
                if (this.SDKConfiguration.RetryConfig != null)
                {
                    retryConfig = this.SDKConfiguration.RetryConfig;
                }
                else
                {
                    var backoff = new BackoffStrategy(
                        initialIntervalMs: 500L,
                        maxIntervalMs: 8000L,
                        maxElapsedTimeMs: 30000L,
                        exponent: 2
                    );
                    retryConfig = new RetryConfig(
                        strategy: RetryConfig.RetryStrategy.BACKOFF,
                        backoff: backoff,
                        retryConnectionErrors: true
                    );
                }
            }

            List<string> statusCodes = new List<string> {
                "408",
                "429",
                "5XX",
            };

            Func<Task<HttpResponseMessage>> retrySend = async () =>
            {
                var _httpRequest = await SDKConfiguration.Client.CloneAsync(httpRequest);
                return await SDKConfiguration.Client.SendAsync(_httpRequest, cancellationToken);
            };
            var retries = new NoResponse.Utils.Retries.Retries(retrySend, retryConfig, statusCodes, cancellationToken);

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await retries.Run();
                int _statusCode = (int)httpResponse.StatusCode;

                if (_statusCode >= 400 && _statusCode < 500 || _statusCode >= 500 && _statusCode < 600)
                {
                    var _httpResponse = await this.SDKConfiguration.Hooks.AfterErrorAsync(new AfterErrorContext(hookCtx), httpResponse, null);
                    if (_httpResponse != null)
                    {
                        httpResponse = _httpResponse;
                    }
                }
            }
            catch (Exception _hookError)
            {
                var _httpResponse = await this.SDKConfiguration.Hooks.AfterErrorAsync(new AfterErrorContext(hookCtx), null, _hookError);
                if (_httpResponse != null)
                {
                    httpResponse = _httpResponse;
                }
                else
                {
                    throw;
                }
            }

            httpResponse = await this.SDKConfiguration.Hooks.AfterSuccessAsync(new AfterSuccessContext(hookCtx), httpResponse);

            var contentType = httpResponse.Content.Headers.ContentType?.MediaType;
            int responseStatusCode = (int)httpResponse.StatusCode;
            if (responseStatusCode == 200)
            {
                if (Utilities.IsContentTypeMatch("application/json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    PolicyEvaluation obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<PolicyEvaluation>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into PolicyEvaluation.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new GetPolicyEvaluationResponse() {
                        Result = result,
                        Headers = Utilities.CollectHeaders(httpResponse.Headers)
                    };
                    return response;
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 400)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    BadRequestExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<BadRequestExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into BadRequestExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new BadRequestException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 401)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    AuthenticationExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<AuthenticationExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into AuthenticationExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new AuthenticationException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 403)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    PermissionDeniedExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<PermissionDeniedExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into PermissionDeniedExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new PermissionDeniedException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 404)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    NotFoundExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<NotFoundExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into NotFoundExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new NotFoundException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 409)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    ConflictExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<ConflictExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ConflictExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new ConflictException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 429)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    RateLimitExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<RateLimitExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into RateLimitExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new RateLimitException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode >= 400 && responseStatusCode < 500)
            {
                throw new Models.Errors.APIStatusException("API error occurred", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode >= 500 && responseStatusCode < 600)
            {
                throw new Models.Errors.APIStatusException("API error occurred", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }

            throw new Models.Errors.APIStatusException("Unknown status code received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Read the bounded completed evaluator report.
        /// </summary>
        /// <remarks>
        /// Read the completed evaluation report, including coverage, uncertainty and candidate results. Unknown capture completeness produces no_change. A completed result is not an automatic policy change.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="evaluationId">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="GetPolicyEvaluationResultResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="evaluationId"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The policy, idempotency binding, evaluation state, proposal or provenance does not match; or the environment retains five jobs. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<GetPolicyEvaluationResultResponse> GetResultAsync(
            string workspaceId,
            KeyEnvironment environment,
            string evaluationId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (evaluationId == null) throw new ArgumentNullException(nameof(evaluationId));

            var request = new GetPolicyEvaluationResultRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                EvaluationId = evaluationId,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/evaluations/{environment}/{evaluation_id}/result", request, null);

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, urlString);
            httpRequest.Headers.Add("user-agent", SDKConfiguration.UserAgent);

            if (!httpRequest.Headers.Contains("Accept"))
            {
                httpRequest.Headers.Add("Accept", "application/json");
            }

            if (SDKConfiguration.SecuritySource != null)
            {
                httpRequest = new SecurityMetadata(SDKConfiguration.SecuritySource).Apply(httpRequest);
            }

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "getPolicyEvaluationResult", null, SDKConfiguration.SecuritySource, cancellationToken);

            httpRequest = await this.SDKConfiguration.Hooks.BeforeRequestAsync(new BeforeRequestContext(hookCtx), httpRequest);
            if (retryConfig == null)
            {
                if (this.SDKConfiguration.RetryConfig != null)
                {
                    retryConfig = this.SDKConfiguration.RetryConfig;
                }
                else
                {
                    var backoff = new BackoffStrategy(
                        initialIntervalMs: 500L,
                        maxIntervalMs: 8000L,
                        maxElapsedTimeMs: 30000L,
                        exponent: 2
                    );
                    retryConfig = new RetryConfig(
                        strategy: RetryConfig.RetryStrategy.BACKOFF,
                        backoff: backoff,
                        retryConnectionErrors: true
                    );
                }
            }

            List<string> statusCodes = new List<string> {
                "408",
                "429",
                "5XX",
            };

            Func<Task<HttpResponseMessage>> retrySend = async () =>
            {
                var _httpRequest = await SDKConfiguration.Client.CloneAsync(httpRequest);
                return await SDKConfiguration.Client.SendAsync(_httpRequest, cancellationToken);
            };
            var retries = new NoResponse.Utils.Retries.Retries(retrySend, retryConfig, statusCodes, cancellationToken);

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await retries.Run();
                int _statusCode = (int)httpResponse.StatusCode;

                if (_statusCode >= 400 && _statusCode < 500 || _statusCode >= 500 && _statusCode < 600)
                {
                    var _httpResponse = await this.SDKConfiguration.Hooks.AfterErrorAsync(new AfterErrorContext(hookCtx), httpResponse, null);
                    if (_httpResponse != null)
                    {
                        httpResponse = _httpResponse;
                    }
                }
            }
            catch (Exception _hookError)
            {
                var _httpResponse = await this.SDKConfiguration.Hooks.AfterErrorAsync(new AfterErrorContext(hookCtx), null, _hookError);
                if (_httpResponse != null)
                {
                    httpResponse = _httpResponse;
                }
                else
                {
                    throw;
                }
            }

            httpResponse = await this.SDKConfiguration.Hooks.AfterSuccessAsync(new AfterSuccessContext(hookCtx), httpResponse);

            var contentType = httpResponse.Content.Headers.ContentType?.MediaType;
            int responseStatusCode = (int)httpResponse.StatusCode;
            if (responseStatusCode == 200)
            {
                if (Utilities.IsContentTypeMatch("application/json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    PolicyEvaluationResult obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<PolicyEvaluationResult>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into PolicyEvaluationResult.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new GetPolicyEvaluationResultResponse() {
                        Result = result,
                        Headers = Utilities.CollectHeaders(httpResponse.Headers)
                    };
                    return response;
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 400)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    BadRequestExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<BadRequestExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into BadRequestExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new BadRequestException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 401)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    AuthenticationExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<AuthenticationExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into AuthenticationExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new AuthenticationException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 403)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    PermissionDeniedExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<PermissionDeniedExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into PermissionDeniedExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new PermissionDeniedException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 404)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    NotFoundExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<NotFoundExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into NotFoundExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new NotFoundException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 409)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    ConflictExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<ConflictExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ConflictExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new ConflictException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 429)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    RateLimitExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<RateLimitExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into RateLimitExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new RateLimitException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode >= 400 && responseStatusCode < 500)
            {
                throw new Models.Errors.APIStatusException("API error occurred", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode >= 500 && responseStatusCode < 600)
            {
                throw new Models.Errors.APIStatusException("API error occurred", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }

            throw new Models.Errors.APIStatusException("Unknown status code received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Cancel processing and destroy retained evidence and report.
        /// </summary>
        /// <remarks>
        /// Cancel a queued or running evaluation and invalidate its stored evidence. The operation does not change policy. Send an empty JSON object.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="evaluationId">Description not available.</param>
        /// <param name="body">A <see cref="PolicyEvaluationCancelRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CancelPolicyEvaluationResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="evaluationId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The Idempotency-Key was already used by this workspace, within 24 hours, with a different body. A reuse with the same body is not a conflict, even while the first request is in flight: it replays the latest assessment. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<CancelPolicyEvaluationResponse> CancelAsync(
            string workspaceId,
            KeyEnvironment environment,
            string evaluationId,
            PolicyEvaluationCancelRequest body,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (evaluationId == null) throw new ArgumentNullException(nameof(evaluationId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new CancelPolicyEvaluationRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                EvaluationId = evaluationId,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/evaluations/{environment}/{evaluation_id}/cancel", request, null);

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, urlString);
            httpRequest.Headers.Add("user-agent", SDKConfiguration.UserAgent);

            if (!httpRequest.Headers.Contains("Accept"))
            {
                httpRequest.Headers.Add("Accept", "application/json");
            }

            var serializedBody = RequestBodySerializer.Serialize(request, "Body", "json", false, false);
            if (serializedBody != null)
            {
                httpRequest.Content = serializedBody;
            }

            if (SDKConfiguration.SecuritySource != null)
            {
                httpRequest = new SecurityMetadata(SDKConfiguration.SecuritySource).Apply(httpRequest);
            }

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "cancelPolicyEvaluation", null, SDKConfiguration.SecuritySource, cancellationToken);

            httpRequest = await this.SDKConfiguration.Hooks.BeforeRequestAsync(new BeforeRequestContext(hookCtx), httpRequest);
            if (retryConfig == null)
            {
                if (this.SDKConfiguration.RetryConfig != null)
                {
                    retryConfig = this.SDKConfiguration.RetryConfig;
                }
                else
                {
                    var backoff = new BackoffStrategy(
                        initialIntervalMs: 500L,
                        maxIntervalMs: 8000L,
                        maxElapsedTimeMs: 30000L,
                        exponent: 2
                    );
                    retryConfig = new RetryConfig(
                        strategy: RetryConfig.RetryStrategy.BACKOFF,
                        backoff: backoff,
                        retryConnectionErrors: true
                    );
                }
            }

            List<string> statusCodes = new List<string> {
                "408",
                "429",
                "5XX",
            };

            Func<Task<HttpResponseMessage>> retrySend = async () =>
            {
                var _httpRequest = await SDKConfiguration.Client.CloneAsync(httpRequest);
                return await SDKConfiguration.Client.SendAsync(_httpRequest, cancellationToken);
            };
            var retries = new NoResponse.Utils.Retries.Retries(retrySend, retryConfig, statusCodes, cancellationToken);

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await retries.Run();
                int _statusCode = (int)httpResponse.StatusCode;

                if (_statusCode >= 400 && _statusCode < 500 || _statusCode >= 500 && _statusCode < 600)
                {
                    var _httpResponse = await this.SDKConfiguration.Hooks.AfterErrorAsync(new AfterErrorContext(hookCtx), httpResponse, null);
                    if (_httpResponse != null)
                    {
                        httpResponse = _httpResponse;
                    }
                }
            }
            catch (Exception _hookError)
            {
                var _httpResponse = await this.SDKConfiguration.Hooks.AfterErrorAsync(new AfterErrorContext(hookCtx), null, _hookError);
                if (_httpResponse != null)
                {
                    httpResponse = _httpResponse;
                }
                else
                {
                    throw;
                }
            }

            httpResponse = await this.SDKConfiguration.Hooks.AfterSuccessAsync(new AfterSuccessContext(hookCtx), httpResponse);

            var contentType = httpResponse.Content.Headers.ContentType?.MediaType;
            int responseStatusCode = (int)httpResponse.StatusCode;
            if (responseStatusCode == 200)
            {
                if (Utilities.IsContentTypeMatch("application/json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    PolicyEvaluation obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<PolicyEvaluation>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into PolicyEvaluation.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new CancelPolicyEvaluationResponse() {
                        Result = result,
                        Headers = Utilities.CollectHeaders(httpResponse.Headers)
                    };
                    return response;
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 400)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    BadRequestExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<BadRequestExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into BadRequestExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new BadRequestException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 401)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    AuthenticationExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<AuthenticationExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into AuthenticationExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new AuthenticationException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 403)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    PermissionDeniedExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<PermissionDeniedExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into PermissionDeniedExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new PermissionDeniedException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 404)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    NotFoundExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<NotFoundExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into NotFoundExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new NotFoundException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 409)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    ConflictExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<ConflictExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ConflictExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new ConflictException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 429)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    RateLimitExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<RateLimitExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into RateLimitExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new RateLimitException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode >= 400 && responseStatusCode < 500)
            {
                throw new Models.Errors.APIStatusException("API error occurred", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode >= 500 && responseStatusCode < 600)
            {
                throw new Models.Errors.APIStatusException("API error occurred", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }

            throw new Models.Errors.APIStatusException("Unknown status code received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Publish an explicitly reviewed proposal with exact provenance.
        /// </summary>
        /// <remarks>
        /// Publish an explicitly reviewed candidate with its exact manifest, input and result hashes and unchanged baseline policy version. Requires settings plus policy_publish, or legacy management. Publication creates a fresh policy version and immutable provenance. no_change, expired or invalidated results are refused.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="evaluationId">Description not available.</param>
        /// <param name="body">A <see cref="PolicyEvaluationPublishRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="PublishPolicyEvaluationResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="evaluationId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The policy, idempotency binding, evaluation state, proposal or provenance does not match; or the environment retains five jobs. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<PublishPolicyEvaluationResponse> PublishAsync(
            string workspaceId,
            KeyEnvironment environment,
            string evaluationId,
            PolicyEvaluationPublishRequest body,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (evaluationId == null) throw new ArgumentNullException(nameof(evaluationId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new PublishPolicyEvaluationRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                EvaluationId = evaluationId,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/evaluations/{environment}/{evaluation_id}/publish", request, null);

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, urlString);
            httpRequest.Headers.Add("user-agent", SDKConfiguration.UserAgent);

            if (!httpRequest.Headers.Contains("Accept"))
            {
                httpRequest.Headers.Add("Accept", "application/json");
            }

            var serializedBody = RequestBodySerializer.Serialize(request, "Body", "json", false, false);
            if (serializedBody != null)
            {
                httpRequest.Content = serializedBody;
            }

            if (SDKConfiguration.SecuritySource != null)
            {
                httpRequest = new SecurityMetadata(SDKConfiguration.SecuritySource).Apply(httpRequest);
            }

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "publishPolicyEvaluation", null, SDKConfiguration.SecuritySource, cancellationToken);

            httpRequest = await this.SDKConfiguration.Hooks.BeforeRequestAsync(new BeforeRequestContext(hookCtx), httpRequest);

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await SDKConfiguration.Client.SendAsync(httpRequest, cancellationToken);
                int _statusCode = (int)httpResponse.StatusCode;

                if (_statusCode >= 400 && _statusCode < 500 || _statusCode >= 500 && _statusCode < 600)
                {
                    var _httpResponse = await this.SDKConfiguration.Hooks.AfterErrorAsync(new AfterErrorContext(hookCtx), httpResponse, null);
                    if (_httpResponse != null)
                    {
                        httpResponse = _httpResponse;
                    }
                }
            }
            catch (Exception _hookError)
            {
                var _httpResponse = await this.SDKConfiguration.Hooks.AfterErrorAsync(new AfterErrorContext(hookCtx), null, _hookError);
                if (_httpResponse != null)
                {
                    httpResponse = _httpResponse;
                }
                else
                {
                    throw;
                }
            }

            httpResponse = await this.SDKConfiguration.Hooks.AfterSuccessAsync(new AfterSuccessContext(hookCtx), httpResponse);

            var contentType = httpResponse.Content.Headers.ContentType?.MediaType;
            int responseStatusCode = (int)httpResponse.StatusCode;
            if (responseStatusCode == 200)
            {
                if (Utilities.IsContentTypeMatch("application/json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    Models.Shared.Settings obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<Models.Shared.Settings>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into Models.Shared.Settings.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new PublishPolicyEvaluationResponse() {
                        Result = result,
                        Headers = Utilities.CollectHeaders(httpResponse.Headers)
                    };
                    return response;
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 400)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    BadRequestExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<BadRequestExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into BadRequestExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new BadRequestException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 401)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    AuthenticationExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<AuthenticationExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into AuthenticationExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new AuthenticationException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 403)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    PermissionDeniedExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<PermissionDeniedExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into PermissionDeniedExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new PermissionDeniedException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 404)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    NotFoundExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<NotFoundExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into NotFoundExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new NotFoundException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 409)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    ConflictExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<ConflictExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ConflictExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new ConflictException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 429)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    RateLimitExceptionPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<RateLimitExceptionPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into RateLimitExceptionPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new RateLimitException(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode >= 400 && responseStatusCode < 500)
            {
                throw new Models.Errors.APIStatusException("API error occurred", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode >= 500 && responseStatusCode < 600)
            {
                throw new Models.Errors.APIStatusException("API error occurred", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }

            throw new Models.Errors.APIStatusException("Unknown status code received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
        }
    }
}