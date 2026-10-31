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
    /// Collaborative review cases, comments and linked subjects.
    /// </summary>
    public interface IReviews
    {
        /// <summary>
        /// List retained account reviews.
        /// </summary>
        /// <remarks>
        /// List retained investigation reviews using filters and an authenticated cursor. Use reviews.query to filter by account_id in a JSON body. All filters are bound into the cursor.
        /// </remarks>
        /// <param name="request">A <see cref="ListReviewsRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListReviewsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">The required parameter <paramref name="request"/> is null.</exception>
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
        public Task<ListReviewsResponse> ListAsync(
            ListReviewsRequest request,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Open a review for a retained account.
        /// </summary>
        /// <remarks>
        /// Open a review for one account. An account can have one active review in its environment.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="ReviewCreateRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CreateReviewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<CreateReviewResponse> CreateAsync(
            string workspaceId,
            KeyEnvironment environment,
            ReviewCreateRequest body,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Read an account review.
        /// </summary>
        /// <remarks>
        /// Read a retained review and its current version.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="GetReviewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="reviewId"/> is null.</exception>
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
        public Task<GetReviewResponse> GetAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Assign, prioritize or reopen a guarded review.
        /// </summary>
        /// <remarks>
        /// Change review status, priority or assignment after checking the expected version.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewUpdateRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="UpdateReviewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="reviewId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<UpdateReviewResponse> UpdateAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            ReviewUpdateRequest body,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Resolve a review against an exact account risk version.
        /// </summary>
        /// <remarks>
        /// Resolve a review against the exact current review and account decision versions. A resolution does not report a factual outcome or execute enforcement.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewResolveRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ResolveReviewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="reviewId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<ResolveReviewResponse> ResolveAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            ReviewResolveRequest body,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Read retained review comments.
        /// </summary>
        /// <remarks>
        /// Page through retained review comments.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="limit">Description not available.</param>
        /// <param name="cursor">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListReviewCommentsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="reviewId"/> is null.</exception>
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
        public Task<ListReviewCommentsResponse> ListCommentsAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            long? limit = 50,
            string? cursor = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Append a comment against the current review version.
        /// </summary>
        /// <remarks>
        /// Append a comment after checking the expected review version.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewCommentRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CommentReviewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="reviewId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<CommentReviewResponse> CommentAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            ReviewCommentRequest body,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Read immutable review resolution history.
        /// </summary>
        /// <remarks>
        /// Page through immutable review resolutions.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="limit">Description not available.</param>
        /// <param name="beforeVersion">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListReviewResolutionsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="reviewId"/> is null.</exception>
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
        public Task<ListReviewResolutionsResponse> ListResolutionsAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            long? limit = 50,
            long? beforeVersion = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// List your private and shared review filters.
        /// </summary>
        /// <remarks>
        /// List private and shared saved review filters visible to you.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListReviewViewsResponse"/> object when completed.</returns>
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
        public Task<ListReviewViewsResponse> ListViewsAsync(
            string workspaceId,
            KeyEnvironment environment,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Save a tenant-local review filter.
        /// </summary>
        /// <remarks>
        /// Save a private or shared review filter.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="ReviewViewRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CreateReviewViewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<CreateReviewViewResponse> CreateViewAsync(
            string workspaceId,
            KeyEnvironment environment,
            ReviewViewRequest body,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Replace your saved filter with optimistic concurrency.
        /// </summary>
        /// <remarks>
        /// Update your saved review filter after checking its expected version.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="viewId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewViewRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="UpdateReviewViewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="viewId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<UpdateReviewViewResponse> UpdateViewAsync(
            string workspaceId,
            KeyEnvironment environment,
            string viewId,
            ReviewViewRequest body,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Delete your saved filter with optimistic concurrency.
        /// </summary>
        /// <remarks>
        /// Delete your saved review filter after checking its expected version.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="viewId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewViewDeleteRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="DeleteReviewViewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="viewId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<DeleteReviewViewResponse> DeleteViewAsync(
            string workspaceId,
            KeyEnvironment environment,
            string viewId,
            ReviewViewDeleteRequest body,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Export an account review.
        /// </summary>
        /// <remarks>
        /// Export retained review details, comments, resolutions and linked subjects. Requires current reviews.read and exports.read; API keys need management together with exports, history or audit_manage. The audit event commits before any bytes are returned. Oversized exports are refused without truncation; use the bounded collection APIs.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ExportReviewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="reviewId"/> is null.</exception>
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
        public Task<ExportReviewResponse> ExportAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// List retained subjects linked to a review.
        /// </summary>
        /// <remarks>
        /// List retained subjects linked to this review. A link supplies investigation context and does not prove a relationship or change account risk.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListReviewSubjectsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="reviewId"/> is null.</exception>
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
        public Task<ListReviewSubjectsResponse> ListSubjectsAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Link retained tenant-local evidence to an investigation.
        /// </summary>
        /// <remarks>
        /// Link an account, exact assessment revision or issued-key association within this environment. Supply the expected review version. A review permits 100 links. Erasure removes links to erased subjects. This action records context without creating feedback or enforcement.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewSubjectRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="AddReviewSubjectResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="reviewId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review version changed or the review already holds 100 links. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<AddReviewSubjectResponse> AddSubjectAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            ReviewSubjectRequest body,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Remove a linked subject using the current review version.
        /// </summary>
        /// <remarks>
        /// Remove a subject link with the expected review version. The linked account, assessment or key association remains unchanged.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="subjectId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewViewDeleteRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="DeleteReviewSubjectResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="reviewId"/>, <paramref name="subjectId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review version changed or the review already holds 100 links. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<DeleteReviewSubjectResponse> DeleteSubjectAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            string subjectId,
            ReviewViewDeleteRequest body,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Query retained reviews without identifiers in URLs.
        /// </summary>
        /// <remarks>
        /// Query retained investigation reviews using a JSON body, including an exact account_id filter. All filters are bound into the cursor.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="ReviewQueryRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="QueryReviewsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
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
        public Task<QueryReviewsResponse> QueryAsync(
            string workspaceId,
            KeyEnvironment environment,
            ReviewQueryRequest body,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );
    }

    /// <summary>
    /// Collaborative review cases, comments and linked subjects.
    /// </summary>
    public class Reviews : IReviews
    {
        /// <summary>
        /// SDK Configuration.
        /// <see cref="SDKConfig"/>
        /// </summary>
        public SDKConfig SDKConfiguration { get; private set; }

        public Reviews(SDKConfig config)
        {
            SDKConfiguration = config;
        }

        /// <summary>
        /// List retained account reviews.
        /// </summary>
        /// <remarks>
        /// List retained investigation reviews using filters and an authenticated cursor. Use reviews.query to filter by account_id in a JSON body. All filters are bound into the cursor.
        /// </remarks>
        /// <param name="request">A <see cref="ListReviewsRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListReviewsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">The required parameter <paramref name="request"/> is null.</exception>
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
        public async Task<ListReviewsResponse> ListAsync(
            ListReviewsRequest request,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/reviews/{environment}", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "listReviews", null, SDKConfiguration.SecuritySource, cancellationToken);

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

            Func<Task<ListReviewsResponse?>> nextFunc = async delegate()
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

                var newRequest = new ListReviewsRequest {
                    WorkspaceId = request.WorkspaceId,
                    Environment = request.Environment,
                    Limit = request.Limit,
                    Cursor = nextCursor,
                    Status = request.Status,
                    Priority = request.Priority,
                    Assignee = request.Assignee,
                    Unassigned = request.Unassigned,
                    AssigneePrincipalId = request.AssigneePrincipalId
                };

                return await ListAsync(
                    request: newRequest,
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
                    ReviewList obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ReviewList>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ReviewList.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new ListReviewsResponse() {
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
        /// Open a review for a retained account.
        /// </summary>
        /// <remarks>
        /// Open a review for one account. An account can have one active review in its environment.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="ReviewCreateRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CreateReviewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<CreateReviewResponse> CreateAsync(
            string workspaceId,
            KeyEnvironment environment,
            ReviewCreateRequest body,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new CreateReviewRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/reviews/{environment}", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "createReview", null, SDKConfiguration.SecuritySource, cancellationToken);

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
            if (responseStatusCode == 201)
            {
                if (Utilities.IsContentTypeMatch("application/json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    Review obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<Review>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into Review.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new CreateReviewResponse() {
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
        /// Read an account review.
        /// </summary>
        /// <remarks>
        /// Read a retained review and its current version.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="GetReviewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="reviewId"/> is null.</exception>
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
        public async Task<GetReviewResponse> GetAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));

            var request = new GetReviewRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ReviewId = reviewId,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/reviews/{environment}/{review_id}", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "getReview", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    Review obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<Review>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into Review.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new GetReviewResponse() {
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
        /// Assign, prioritize or reopen a guarded review.
        /// </summary>
        /// <remarks>
        /// Change review status, priority or assignment after checking the expected version.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewUpdateRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="UpdateReviewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="reviewId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<UpdateReviewResponse> UpdateAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            ReviewUpdateRequest body,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new UpdateReviewRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ReviewId = reviewId,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/reviews/{environment}/{review_id}", request, null);

            var httpRequest = new HttpRequestMessage(HttpMethod.Patch, urlString);
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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "updateReview", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    Review obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<Review>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into Review.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new UpdateReviewResponse() {
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
        /// Resolve a review against an exact account risk version.
        /// </summary>
        /// <remarks>
        /// Resolve a review against the exact current review and account decision versions. A resolution does not report a factual outcome or execute enforcement.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewResolveRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ResolveReviewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="reviewId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<ResolveReviewResponse> ResolveAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            ReviewResolveRequest body,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new ResolveReviewRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ReviewId = reviewId,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/reviews/{environment}/{review_id}/resolve", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "resolveReview", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    Review obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<Review>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into Review.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new ResolveReviewResponse() {
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
        /// Read retained review comments.
        /// </summary>
        /// <remarks>
        /// Page through retained review comments.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="limit">Description not available.</param>
        /// <param name="cursor">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListReviewCommentsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="reviewId"/> is null.</exception>
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
        public async Task<ListReviewCommentsResponse> ListCommentsAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            long? limit = 50,
            string? cursor = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));

            var request = new ListReviewCommentsRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ReviewId = reviewId,
                Limit = limit,
                Cursor = cursor,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/reviews/{environment}/{review_id}/comments", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "listReviewComments", null, SDKConfiguration.SecuritySource, cancellationToken);

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

            Func<Task<ListReviewCommentsResponse?>> nextFunc = async delegate()
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

                return await ListCommentsAsync(
                    workspaceId: request.WorkspaceId,
                    environment: request.Environment,
                    reviewId: request.ReviewId,
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
                    ReviewCommentList obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ReviewCommentList>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ReviewCommentList.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new ListReviewCommentsResponse() {
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
        /// Append a comment against the current review version.
        /// </summary>
        /// <remarks>
        /// Append a comment after checking the expected review version.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewCommentRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CommentReviewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="reviewId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<CommentReviewResponse> CommentAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            ReviewCommentRequest body,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new CommentReviewRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ReviewId = reviewId,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/reviews/{environment}/{review_id}/comments", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "commentReview", null, SDKConfiguration.SecuritySource, cancellationToken);

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
            if (responseStatusCode == 201)
            {
                if (Utilities.IsContentTypeMatch("application/json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    ReviewComment obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ReviewComment>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ReviewComment.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new CommentReviewResponse() {
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
        /// Read immutable review resolution history.
        /// </summary>
        /// <remarks>
        /// Page through immutable review resolutions.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="limit">Description not available.</param>
        /// <param name="beforeVersion">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListReviewResolutionsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="reviewId"/> is null.</exception>
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
        public async Task<ListReviewResolutionsResponse> ListResolutionsAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            long? limit = 50,
            long? beforeVersion = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));

            var request = new ListReviewResolutionsRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ReviewId = reviewId,
                Limit = limit,
                BeforeVersion = beforeVersion,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/reviews/{environment}/{review_id}/resolutions", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "listReviewResolutions", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    ReviewResolutionList obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ReviewResolutionList>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ReviewResolutionList.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new ListReviewResolutionsResponse() {
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
        /// List your private and shared review filters.
        /// </summary>
        /// <remarks>
        /// List private and shared saved review filters visible to you.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListReviewViewsResponse"/> object when completed.</returns>
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
        public async Task<ListReviewViewsResponse> ListViewsAsync(
            string workspaceId,
            KeyEnvironment environment,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));

            var request = new ListReviewViewsRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/review_views/{environment}", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "listReviewViews", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    ReviewViewList obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ReviewViewList>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ReviewViewList.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new ListReviewViewsResponse() {
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
        /// Save a tenant-local review filter.
        /// </summary>
        /// <remarks>
        /// Save a private or shared review filter.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="ReviewViewRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CreateReviewViewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<CreateReviewViewResponse> CreateViewAsync(
            string workspaceId,
            KeyEnvironment environment,
            ReviewViewRequest body,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new CreateReviewViewRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/review_views/{environment}", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "createReviewView", null, SDKConfiguration.SecuritySource, cancellationToken);

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
            if (responseStatusCode == 201)
            {
                if (Utilities.IsContentTypeMatch("application/json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    ReviewView obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ReviewView>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ReviewView.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new CreateReviewViewResponse() {
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
        /// Replace your saved filter with optimistic concurrency.
        /// </summary>
        /// <remarks>
        /// Update your saved review filter after checking its expected version.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="viewId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewViewRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="UpdateReviewViewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="viewId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<UpdateReviewViewResponse> UpdateViewAsync(
            string workspaceId,
            KeyEnvironment environment,
            string viewId,
            ReviewViewRequest body,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (viewId == null) throw new ArgumentNullException(nameof(viewId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new UpdateReviewViewRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ViewId = viewId,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/review_views/{environment}/{view_id}", request, null);

            var httpRequest = new HttpRequestMessage(HttpMethod.Patch, urlString);
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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "updateReviewView", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    ReviewView obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ReviewView>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ReviewView.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new UpdateReviewViewResponse() {
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
        /// Delete your saved filter with optimistic concurrency.
        /// </summary>
        /// <remarks>
        /// Delete your saved review filter after checking its expected version.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="viewId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewViewDeleteRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="DeleteReviewViewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="viewId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review or referenced risk version changed, the account already has a review, or a bounded review collection is full. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<DeleteReviewViewResponse> DeleteViewAsync(
            string workspaceId,
            KeyEnvironment environment,
            string viewId,
            ReviewViewDeleteRequest body,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (viewId == null) throw new ArgumentNullException(nameof(viewId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new DeleteReviewViewRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ViewId = viewId,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/review_views/{environment}/{view_id}", request, null);

            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, urlString);
            httpRequest.Headers.Add("user-agent", SDKConfiguration.UserAgent);

            if (!httpRequest.Headers.Contains("Accept"))
            {
                httpRequest.Headers.Add("Accept", "application/problem+json");
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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "deleteReviewView", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                return new DeleteReviewViewResponse() {
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
        /// Export an account review.
        /// </summary>
        /// <remarks>
        /// Export retained review details, comments, resolutions and linked subjects. Requires current reviews.read and exports.read; API keys need management together with exports, history or audit_manage. The audit event commits before any bytes are returned. Oversized exports are refused without truncation; use the bounded collection APIs.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ExportReviewResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="reviewId"/> is null.</exception>
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
        public async Task<ExportReviewResponse> ExportAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));

            var request = new ExportReviewRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ReviewId = reviewId,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/reviews/{environment}/{review_id}/export", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "exportReview", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    Export obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<Export>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into Export.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new ExportReviewResponse() {
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
        /// List retained subjects linked to a review.
        /// </summary>
        /// <remarks>
        /// List retained subjects linked to this review. A link supplies investigation context and does not prove a relationship or change account risk.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListReviewSubjectsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="reviewId"/> is null.</exception>
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
        public async Task<ListReviewSubjectsResponse> ListSubjectsAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));

            var request = new ListReviewSubjectsRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ReviewId = reviewId,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/reviews/{environment}/{review_id}/subjects", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "listReviewSubjects", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    ReviewSubjectList obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ReviewSubjectList>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ReviewSubjectList.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new ListReviewSubjectsResponse() {
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
        /// Link retained tenant-local evidence to an investigation.
        /// </summary>
        /// <remarks>
        /// Link an account, exact assessment revision or issued-key association within this environment. Supply the expected review version. A review permits 100 links. Erasure removes links to erased subjects. This action records context without creating feedback or enforcement.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewSubjectRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="AddReviewSubjectResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="reviewId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review version changed or the review already holds 100 links. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<AddReviewSubjectResponse> AddSubjectAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            ReviewSubjectRequest body,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new AddReviewSubjectRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ReviewId = reviewId,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/reviews/{environment}/{review_id}/subjects", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "addReviewSubject", null, SDKConfiguration.SecuritySource, cancellationToken);

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
            if (responseStatusCode == 201)
            {
                if (Utilities.IsContentTypeMatch("application/json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    ReviewSubjectChange obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ReviewSubjectChange>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ReviewSubjectChange.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new AddReviewSubjectResponse() {
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
        /// Remove a linked subject using the current review version.
        /// </summary>
        /// <remarks>
        /// Remove a subject link with the expected review version. The linked account, assessment or key association remains unchanged.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="reviewId">Description not available.</param>
        /// <param name="subjectId">Description not available.</param>
        /// <param name="body">A <see cref="ReviewViewDeleteRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="DeleteReviewSubjectResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/>, <paramref name="reviewId"/>, <paramref name="subjectId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The review version changed or the review already holds 100 links. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<DeleteReviewSubjectResponse> DeleteSubjectAsync(
            string workspaceId,
            KeyEnvironment environment,
            string reviewId,
            string subjectId,
            ReviewViewDeleteRequest body,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));
            if (subjectId == null) throw new ArgumentNullException(nameof(subjectId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new DeleteReviewSubjectRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                ReviewId = reviewId,
                SubjectId = subjectId,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/reviews/{environment}/{review_id}/subjects/{subject_id}", request, null);

            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, urlString);
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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "deleteReviewSubject", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    Review obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<Review>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into Review.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new DeleteReviewSubjectResponse() {
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
        /// Query retained reviews without identifiers in URLs.
        /// </summary>
        /// <remarks>
        /// Query retained investigation reviews using a JSON body, including an exact account_id filter. All filters are bound into the cursor.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="environment">The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.</param>
        /// <param name="body">A <see cref="ReviewQueryRequest"/> parameter.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="QueryReviewsResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
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
        public async Task<QueryReviewsResponse> QueryAsync(
            string workspaceId,
            KeyEnvironment environment,
            ReviewQueryRequest body,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new QueryReviewsRequest() {
                WorkspaceId = workspaceId,
                Environment = environment,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/reviews/{environment}/query", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "queryReviews", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    ReviewList obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<ReviewList>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ReviewList.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new QueryReviewsResponse() {
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