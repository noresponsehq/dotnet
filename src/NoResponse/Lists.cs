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
    /// List entries, previews and durable changes.
    /// </summary>
    public interface ILists
    {
        /// <summary>
        /// List an environment's list entries.
        /// </summary>
        /// <remarks>
        /// Page through this environment allow and block entries, with canonical values, reasons and optional expiry. Requires settings or management. Follow the returned cursor until exhausted; a page is not the complete list. The cursor is bound to the environment and current filters.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="limit">Description not available.</param>
        /// <param name="cursor">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListListEntriesResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">The required parameter <paramref name="workspaceId"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">A query parameter is unknown, repeated or out of range, the time interval is invalid, the cursor fails authentication or belongs to another workspace or filter, the cursor has expired, or a console credential named no valid NR-Acting-User. invalid_params names offending parameters without their values. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The Idempotency-Key was already used by this workspace, within 24 hours, with a different body. A reuse with the same body is not a conflict, even while the first request is in flight: it replays the latest assessment. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<ListListEntriesResponse> ListAsync(
            string workspaceId,
            KeyEnvironment environment,
            long? limit = null,
            string? cursor = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Add an entry to an environment's list.
        /// </summary>
        /// <remarks>
        /// Add a canonical email or email-domain override with a reason and optional expiry. Requires settings plus policy_publish, or legacy management. Block wins over allow when both match. A small single-entry change retains the documented synchronous match budget; use lists.change for larger changes, whose account propagation is durable and resumable.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="ListEntryRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CreateListEntryResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">list_entry_exists: the value is already on the environment's list; delete its entry first to change its action. list_limit_reached: the environment already holds 10,000 entries. list_entry_too_broad: the entry matches more than 5,000 accounts. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<CreateListEntryResponse> CreateAsync(
            string workspaceId,
            KeyEnvironment environment,
            ListEntryRequest body,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Remove an entry from an environment's list.
        /// </summary>
        /// <remarks>
        /// Remove an override with settings plus policy_publish, or legacy management. The removal takes effect for new decisions and schedules resumable propagation across matching retained accounts. An unknown or already removed entry answers404. Account risk events announce changed decisions as propagation runs.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="listEntryId">The entry's id. An entry of another environment or workspace answers 404.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="DeleteListEntryResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="listEntryId"/> is null.</exception>
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
        public Task<DeleteListEntryResponse> DeleteAsync(
            string workspaceId,
            KeyEnvironment environment,
            string listEntryId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Preview a list override change without side effects.
        /// </summary>
        /// <remarks>
        /// Preview canonical list matches and current override effects with settings or management, plus legacy history, or investigate with evidence or detailed. The result states its bounded coverage and changes no list entries.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="ListPreviewRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="PreviewListResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">A value already exists, a list is full, or an idempotency key was reused with different changes. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<PreviewListResponse> PreviewAsync(
            string workspaceId,
            KeyEnvironment environment,
            ListPreviewRequest body,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Apply an idempotent atomic bulk list change.
        /// </summary>
        /// <remarks>
        /// Atomically add and remove a bounded batch of entries with an idempotency key. Requires settings plus policy_publish, or legacy management. The receipt exposes durable account propagation beyond the synchronous single-entry budget; poll lists.getChange until completion.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="ListBulkRequest"/> parameter.</param>
        /// <param name="idempotencyKey">Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the workspace principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ChangeListsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The list change body or query is invalid, or Idempotency-Key is missing, repeated or malformed. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">A value already exists, a list is full, or an idempotency key was reused with different changes. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<ChangeListsResponse> ChangeAsync(
            string workspaceId,
            KeyEnvironment environment,
            ListBulkRequest body,
            string? idempotencyKey = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Read a bulk list change and propagation progress.
        /// </summary>
        /// <remarks>
        /// Read retained propagation progress for a list change with settings or management.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="changeId">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="GetListChangeResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="changeId"/> is null.</exception>
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
        public Task<GetListChangeResponse> GetChangeAsync(
            string workspaceId,
            KeyEnvironment environment,
            string changeId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );
    }

    /// <summary>
    /// List entries, previews and durable changes.
    /// </summary>
    public class Lists : ILists
    {
        /// <summary>
        /// SDK Configuration.
        /// <see cref="SDKConfig"/>
        /// </summary>
        public SDKConfig SDKConfiguration { get; private set; }

        public Lists(SDKConfig config)
        {
            SDKConfiguration = config;
        }

        /// <summary>
        /// List an environment's list entries.
        /// </summary>
        /// <remarks>
        /// Page through this environment allow and block entries, with canonical values, reasons and optional expiry. Requires settings or management. Follow the returned cursor until exhausted; a page is not the complete list. The cursor is bound to the environment and current filters.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="limit">Description not available.</param>
        /// <param name="cursor">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListListEntriesResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">The required parameter <paramref name="workspaceId"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">A query parameter is unknown, repeated or out of range, the time interval is invalid, the cursor fails authentication or belongs to another workspace or filter, the cursor has expired, or a console credential named no valid NR-Acting-User. invalid_params names offending parameters without their values. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The Idempotency-Key was already used by this workspace, within 24 hours, with a different body. A reuse with the same body is not a conflict, even while the first request is in flight: it replays the latest assessment. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<ListListEntriesResponse> ListAsync(
            string workspaceId,
            KeyEnvironment environment,
            long? limit = null,
            string? cursor = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));

            var request = new ListListEntriesRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                Limit = limit,
                Cursor = cursor,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/lists/{environment}", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "listListEntries", null, SDKConfiguration.SecuritySource, cancellationToken);

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

            Func<Task<ListListEntriesResponse?>> nextFunc = async delegate()
            {
                var body = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());
                var nextCursorToken = body.SelectToken("$.next_cursor");

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
                    cursor: nextCursor,
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
                    ListEntryList obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ListEntryList>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ListEntryList.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new ListListEntriesResponse() {
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
        /// Add an entry to an environment's list.
        /// </summary>
        /// <remarks>
        /// Add a canonical email or email-domain override with a reason and optional expiry. Requires settings plus policy_publish, or legacy management. Block wins over allow when both match. A small single-entry change retains the documented synchronous match budget; use lists.change for larger changes, whose account propagation is durable and resumable.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="ListEntryRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CreateListEntryResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">list_entry_exists: the value is already on the environment's list; delete its entry first to change its action. list_limit_reached: the environment already holds 10,000 entries. list_entry_too_broad: the entry matches more than 5,000 accounts. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<CreateListEntryResponse> CreateAsync(
            string workspaceId,
            KeyEnvironment environment,
            ListEntryRequest body,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new CreateListEntryRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/lists/{environment}", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "createListEntry", null, SDKConfiguration.SecuritySource, cancellationToken);

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
            if (responseStatusCode == 201)
            {
                if (Utilities.IsContentTypeMatch("application/json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    ListEntry obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ListEntry>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ListEntry.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new CreateListEntryResponse() {
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
        /// Remove an entry from an environment's list.
        /// </summary>
        /// <remarks>
        /// Remove an override with settings plus policy_publish, or legacy management. The removal takes effect for new decisions and schedules resumable propagation across matching retained accounts. An unknown or already removed entry answers404. Account risk events announce changed decisions as propagation runs.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="listEntryId">The entry's id. An entry of another environment or workspace answers 404.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="DeleteListEntryResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="listEntryId"/> is null.</exception>
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
        public async Task<DeleteListEntryResponse> DeleteAsync(
            string workspaceId,
            KeyEnvironment environment,
            string listEntryId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (listEntryId == null) throw new ArgumentNullException(nameof(listEntryId));

            var request = new DeleteListEntryRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ListEntryId = listEntryId,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/lists/{environment}/{list_entry_id}", request, null);

            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, urlString);
            httpRequest.Headers.Add("user-agent", SDKConfiguration.UserAgent);

            if (!httpRequest.Headers.Contains("Accept"))
            {
                httpRequest.Headers.Add("Accept", "application/problem+json");
            }

            if (SDKConfiguration.SecuritySource != null)
            {
                httpRequest = new SecurityMetadata(SDKConfiguration.SecuritySource).Apply(httpRequest);
            }

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "deleteListEntry", null, SDKConfiguration.SecuritySource, cancellationToken);

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
            if (responseStatusCode == 204)
            {
                return new DeleteListEntryResponse() {
                    Headers = Utilities.CollectHeaders(httpResponse.Headers)
                };
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
        /// Preview a list override change without side effects.
        /// </summary>
        /// <remarks>
        /// Preview canonical list matches and current override effects with settings or management, plus legacy history, or investigate with evidence or detailed. The result states its bounded coverage and changes no list entries.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="ListPreviewRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="PreviewListResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">A value already exists, a list is full, or an idempotency key was reused with different changes. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<PreviewListResponse> PreviewAsync(
            string workspaceId,
            KeyEnvironment environment,
            ListPreviewRequest body,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new PreviewListRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/lists/{environment}/preview", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "previewList", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    ListImpact obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ListImpact>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ListImpact.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new PreviewListResponse() {
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
        /// Apply an idempotent atomic bulk list change.
        /// </summary>
        /// <remarks>
        /// Atomically add and remove a bounded batch of entries with an idempotency key. Requires settings plus policy_publish, or legacy management. The receipt exposes durable account propagation beyond the synchronous single-entry budget; poll lists.getChange until completion.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="ListBulkRequest"/> parameter.</param>
        /// <param name="idempotencyKey">Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the workspace principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ChangeListsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The list change body or query is invalid, or Idempotency-Key is missing, repeated or malformed. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">A value already exists, a list is full, or an idempotency key was reused with different changes. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<ChangeListsResponse> ChangeAsync(
            string workspaceId,
            KeyEnvironment environment,
            ListBulkRequest body,
            string? idempotencyKey = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new ChangeListsRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                Body = body,
                IdempotencyKey = idempotencyKey,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/lists/{environment}/bulk", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "changeLists", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    ListChange obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ListChange>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ListChange.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new ChangeListsResponse() {
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
        /// Read a bulk list change and propagation progress.
        /// </summary>
        /// <remarks>
        /// Read retained propagation progress for a list change with settings or management.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="changeId">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="GetListChangeResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="changeId"/> is null.</exception>
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
        public async Task<GetListChangeResponse> GetChangeAsync(
            string workspaceId,
            KeyEnvironment environment,
            string changeId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (changeId == null) throw new ArgumentNullException(nameof(changeId));

            var request = new GetListChangeRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ChangeId = changeId,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/list_changes/{environment}/{change_id}", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "getListChange", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    ListChange obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ListChange>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ListChange.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new GetListChangeResponse() {
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