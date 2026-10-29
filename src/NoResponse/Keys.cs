#nullable enable
namespace NoResponse
{
    using Newtonsoft.Json;
    using NoResponse.Hooks;
    using NoResponse.Models.Errors;
    using NoResponse.Models.Operations;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using NoResponse.Utils.Retries;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Your own API keys.
    /// </summary>
    public interface IKeys
    {
        /// <summary>
        /// Rotate the API key that signs the request.
        /// </summary>
        /// <remarks>
        /// Mints a new key with the same scopes as the key that signs the request and returns it once. The presented key keeps working for overlap_seconds (default 24 hours, from 60 seconds to 7 days), then answers 401. A key accepts one rotation: rotating again with a key already in its overlap answers 409 key_rotation_in_progress, so rotate with the new key. Rotation is not idempotent: a retry after a lost answer mints another key.
        /// </remarks>
        /// <param name="request">A <see cref="KeyRotationRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="RotateApiKeyResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">The required parameter <paramref name="request"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body is not JSON or does not match the request schema (an unknown member, or overlap_seconds outside 60 to 604800). invalid_params names the offending fields without their values. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">The API key is valid but may not use this operation: it lacks the operation's scope, or its workspace is not entitled or is suspended. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No unexpired assessment with this id for the calling workspace, or a malformed assessment_id. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The presented key was already rotated and is still in its overlap window. Rotate with the new key; the new key's own rotation is not affected. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<RotateApiKeyResponse> RotateAsync(
            KeyRotationRequest request,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Create an API key.
        /// </summary>
        /// <remarks>
        /// Create an API key in the signing key environment with authority no greater than the issuer holds. Requires management. Optional name, purpose, kind and expiry describe its lifecycle. Send request_id for safe issuance retries: the first successful response returns the secret once; a repeat returns existing metadata without the secret, and changed parameters conflict. Without request_id a retry can create another key. A service credential is independent of its issuer; personal credentials require an authenticated member and retire when that member loses authority.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="body">A <see cref="KeyRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CreateKeyResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">key_limit_reached: the environment already has 100 keys that authenticate; revoke one first. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<CreateKeyResponse> CreateAsync(
            string workspaceId,
            KeyRequest body,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// List API keys.
        /// </summary>
        /// <remarks>
        /// Lists the keys of the signing key's environment, newest first, at most 200, revoked and expired keys included, without secrets: each key's id, prefix, scopes, status, and who created and revoked it. Requires the management scope. Read-only and safe to retry.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListKeysResponse"/> object when completed.</returns>
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
        public Task<ListKeysResponse> ListAsync(
            string workspaceId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Revoke an API key.
        /// </summary>
        /// <remarks>
        /// Revokes a key of the signing key's environment at once: its next request answers 401. Requires the management scope. A key of the other environment or of another workspace answers 404 not_found. A key cannot revoke itself (409 self_revocation): rotate it with keys.rotate, or revoke it with another management key. Revoking a revoked key changes nothing and answers the key as it was revoked, so a retry is safe.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="keyId">The key's id. A key of another workspace answers 404, as does a key of the other environment to an API key.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="RevokeKeyResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="keyId"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">self_revocation: an API key cannot revoke itself. Rotate it with POST /v1/keys/rotate, or revoke it with another management key or through the console. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<RevokeKeyResponse> RevokeAsync(
            string workspaceId,
            string keyId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );
    }

    /// <summary>
    /// Your own API keys.
    /// </summary>
    public class Keys : IKeys
    {
        /// <summary>
        /// SDK Configuration.
        /// <see cref="SDKConfig"/>
        /// </summary>
        public SDKConfig SDKConfiguration { get; private set; }

        public Keys(SDKConfig config)
        {
            SDKConfiguration = config;
        }

        /// <summary>
        /// Rotate the API key that signs the request.
        /// </summary>
        /// <remarks>
        /// Mints a new key with the same scopes as the key that signs the request and returns it once. The presented key keeps working for overlap_seconds (default 24 hours, from 60 seconds to 7 days), then answers 401. A key accepts one rotation: rotating again with a key already in its overlap answers 409 key_rotation_in_progress, so rotate with the new key. Rotation is not idempotent: a retry after a lost answer mints another key.
        /// </remarks>
        /// <param name="request">A <see cref="KeyRotationRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="RotateApiKeyResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">The required parameter <paramref name="request"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body is not JSON or does not match the request schema (an unknown member, or overlap_seconds outside 60 to 604800). invalid_params names the offending fields without their values. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">The API key is valid but may not use this operation: it lacks the operation's scope, or its workspace is not entitled or is suspended. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No unexpired assessment with this id for the calling workspace, or a malformed assessment_id. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The presented key was already rotated and is still in its overlap window. Rotate with the new key; the new key's own rotation is not affected. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<RotateApiKeyResponse> RotateAsync(
            KeyRotationRequest request,
            CancellationToken? cancellationToken = null
        )
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = baseUrl + "/v1/keys/rotate";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, urlString);
            httpRequest.Headers.Add("user-agent", SDKConfiguration.UserAgent);

            if (!httpRequest.Headers.Contains("Accept"))
            {
                httpRequest.Headers.Add("Accept", "application/json");
            }

            var serializedBody = RequestBodySerializer.Serialize(request, "Request", "json", false, false);
            if (serializedBody != null)
            {
                httpRequest.Content = serializedBody;
            }

            if (SDKConfiguration.SecuritySource != null)
            {
                httpRequest = new SecurityMetadata(SDKConfiguration.SecuritySource).Apply(httpRequest);
            }

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "rotateApiKey", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    KeyRotation obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<KeyRotation>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into KeyRotation.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new RotateApiKeyResponse() {
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
        /// Create an API key.
        /// </summary>
        /// <remarks>
        /// Create an API key in the signing key environment with authority no greater than the issuer holds. Requires management. Optional name, purpose, kind and expiry describe its lifecycle. Send request_id for safe issuance retries: the first successful response returns the secret once; a repeat returns existing metadata without the secret, and changed parameters conflict. Without request_id a retry can create another key. A service credential is independent of its issuer; personal credentials require an authenticated member and retire when that member loses authority.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="body">A <see cref="KeyRequest"/> parameter.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="CreateKeyResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">key_limit_reached: the environment already has 100 keys that authenticate; revoke one first. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<CreateKeyResponse> CreateAsync(
            string workspaceId,
            KeyRequest body,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new CreateKeyRequest() {
                WorkspaceId = workspaceId,
                Body = body,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/keys", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "createKey", null, SDKConfiguration.SecuritySource, cancellationToken);

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
            if (new List<int> { 200, 201 }.Contains(responseStatusCode))
            {
                if (Utilities.IsContentTypeMatch("application/json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    Key obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<Key>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into Key.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new CreateKeyResponse() {
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
        /// List API keys.
        /// </summary>
        /// <remarks>
        /// Lists the keys of the signing key's environment, newest first, at most 200, revoked and expired keys included, without secrets: each key's id, prefix, scopes, status, and who created and revoked it. Requires the management scope. Read-only and safe to retry.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="ListKeysResponse"/> object when completed.</returns>
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
        public async Task<ListKeysResponse> ListAsync(
            string workspaceId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));

            var request = new ListKeysRequest() {
                WorkspaceId = workspaceId,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/keys", request, null);

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "listKeys", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    KeyList obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<KeyList>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into KeyList.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new ListKeysResponse() {
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
        /// Revoke an API key.
        /// </summary>
        /// <remarks>
        /// Revokes a key of the signing key's environment at once: its next request answers 401. Requires the management scope. A key of the other environment or of another workspace answers 404 not_found. A key cannot revoke itself (409 self_revocation): rotate it with keys.rotate, or revoke it with another management key. Revoking a revoked key changes nothing and answers the key as it was revoked, so a retry is safe.
        /// </remarks>
        /// <param name="workspaceId">Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.</param>
        /// <param name="keyId">The key's id. A key of another workspace answers 404, as does a key of the other environment to an API key.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="RevokeKeyResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">One of <paramref name="workspaceId"/> or <paramref name="keyId"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body breaks its schema. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">workspace_forbidden: the signing key lacks the management scope, or a scope it would grant. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No such key or environment for the signing key: another workspace and the other environment answer this too, so no answer shows which workspaces exist. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">self_revocation: an API key cannot revoke itself. Rotate it with POST /v1/keys/rotate, or revoke it with another management key or through the console. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<RevokeKeyResponse> RevokeAsync(
            string workspaceId,
            string keyId,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (workspaceId == null) throw new ArgumentNullException(nameof(workspaceId));
            if (keyId == null) throw new ArgumentNullException(nameof(keyId));

            var request = new RevokeKeyRequest() {
                WorkspaceId = workspaceId,
                KeyId = keyId,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = URLBuilder.Build(baseUrl, "/v1/workspaces/{workspace_id}/keys/{key_id}", request, null);

            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, urlString);
            httpRequest.Headers.Add("user-agent", SDKConfiguration.UserAgent);

            if (!httpRequest.Headers.Contains("Accept"))
            {
                httpRequest.Headers.Add("Accept", "application/json");
            }

            if (SDKConfiguration.SecuritySource != null)
            {
                httpRequest = new SecurityMetadata(SDKConfiguration.SecuritySource).Apply(httpRequest);
            }

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "revokeKey", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    Key obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<Key>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into Key.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new RevokeKeyResponse() {
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