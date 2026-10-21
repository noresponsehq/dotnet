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
        /// <exception cref="BadRequestError">The body is not JSON or does not match the request schema (an unknown member, or overlap_seconds outside 60 to 604800). invalid_params names the offending fields without their values. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationError">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedError">The API key is valid but may not use this operation: it lacks the operation's scope, or its tenant is not entitled or is suspended. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundError">No unexpired assessment with this id for the calling tenant, or a malformed assessment_id. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictError">The presented key was already rotated and is still in its overlap window. Rotate with the new key; the new key's own rotation is not affected. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitError">The tenant's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<RotateApiKeyResponse> RotateAsync(
            KeyRotationRequest request,
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
        /// <exception cref="BadRequestError">The body is not JSON or does not match the request schema (an unknown member, or overlap_seconds outside 60 to 604800). invalid_params names the offending fields without their values. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationError">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedError">The API key is valid but may not use this operation: it lacks the operation's scope, or its tenant is not entitled or is suspended. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundError">No unexpired assessment with this id for the calling tenant, or a malformed assessment_id. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictError">The presented key was already rotated and is still in its overlap window. Rotate with the new key; the new key's own rotation is not affected. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitError">The tenant's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
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
                    BadRequestErrorPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<BadRequestErrorPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into BadRequestErrorPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new BadRequestError(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 401)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    AuthenticationErrorPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<AuthenticationErrorPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into AuthenticationErrorPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new AuthenticationError(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 403)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    PermissionDeniedErrorPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<PermissionDeniedErrorPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into PermissionDeniedErrorPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new PermissionDeniedError(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 404)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    NotFoundErrorPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<NotFoundErrorPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into NotFoundErrorPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new NotFoundError(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 409)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    ConflictErrorPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<ConflictErrorPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into ConflictErrorPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new ConflictError(payload, httpResponse, httpResponseBody);
                }

                throw new Models.Errors.APIStatusException("Unknown content type received", httpResponse, await httpResponse.Content.ReadAsStringAsync());
            }
            else if (responseStatusCode == 429)
            {
                if (Utilities.IsContentTypeMatch("application/problem+json", contentType))
                {
                    var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    RateLimitErrorPayload payload;
                    try
                    {
                        payload = ResponseBodyDeserializer.DeserializeNotNull<RateLimitErrorPayload>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into RateLimitErrorPayload.", httpResponse, httpResponseBody, ex);
                    }

                    throw new RateLimitError(payload, httpResponse, httpResponseBody);
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