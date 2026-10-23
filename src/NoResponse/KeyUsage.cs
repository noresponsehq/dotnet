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

    public interface IKeyUsage
    {
        /// <summary>
        /// Report requests made with API keys your platform issued.
        /// </summary>
        /// <remarks>
        /// Records requests made with API keys your platform issued to your accounts, one event per request: the key's SHA-256 (the issued_credential.sha256 the account's assessment carried), the client IP, the time and optionally the user agent, model and token counts. Most callers use the SDK's usage buffer, which batches events and sends them in the background. Sampling is allowed: sample_rate weights an event's request and token counts. Requires the assess scope. Idempotency-Key is optional in the SDK and handled as on POST /v1/assessments: a retry with the same key and body answers the same record with Idempotent-Replayed: true and counts nothing again; a different body answers 409. A batch holds 1 to 500 events and at most 256 KiB. No raw event is kept: usage is aggregated per key, and IPs, user agents and models are kept only as keyed digests, with the IP's country, for 24 hours. When one key is used from 20 or more IP addresses, or 3 or more countries, within 24 hours, every account whose assessment carried that key gets the shared_api_key reason for 30 days.
        /// </remarks>
        /// <param name="body">A <see cref="KeyUsageBatchRequest"/> parameter.</param>
        /// <param name="idempotencyKey">Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the workspace principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="RecordKeyUsageResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">The required parameter <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body is not JSON or does not match the batch schema (no events or more than 500, an unknown member, a key_sha256 that is not 64 lower-case hex, an over-long user_agent, a sample_rate outside (0, 1]), or Idempotency-Key is missing or malformed. invalid_params names the offending fields, not their values. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">The API key is valid but may not use this operation: it lacks the operation's scope, or its workspace is not entitled or is suspended. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No unexpired assessment with this id for the calling workspace, or a malformed assessment_id. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The Idempotency-Key was already used by this workspace, within 24 hours, with a different body. A reuse with the same body is not a conflict, even while the first request is in flight: it replays the latest assessment. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public Task<RecordKeyUsageResponse> RecordAsync(
            KeyUsageBatchRequest body,
            string? idempotencyKey = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        );
    }

    public class KeyUsage : IKeyUsage
    {
        /// <summary>
        /// SDK Configuration.
        /// <see cref="SDKConfig"/>
        /// </summary>
        public SDKConfig SDKConfiguration { get; private set; }

        public KeyUsage(SDKConfig config)
        {
            SDKConfiguration = config;
        }

        /// <summary>
        /// Report requests made with API keys your platform issued.
        /// </summary>
        /// <remarks>
        /// Records requests made with API keys your platform issued to your accounts, one event per request: the key's SHA-256 (the issued_credential.sha256 the account's assessment carried), the client IP, the time and optionally the user agent, model and token counts. Most callers use the SDK's usage buffer, which batches events and sends them in the background. Sampling is allowed: sample_rate weights an event's request and token counts. Requires the assess scope. Idempotency-Key is optional in the SDK and handled as on POST /v1/assessments: a retry with the same key and body answers the same record with Idempotent-Replayed: true and counts nothing again; a different body answers 409. A batch holds 1 to 500 events and at most 256 KiB. No raw event is kept: usage is aggregated per key, and IPs, user agents and models are kept only as keyed digests, with the IP's country, for 24 hours. When one key is used from 20 or more IP addresses, or 3 or more countries, within 24 hours, every account whose assessment carried that key gets the shared_api_key reason for 30 days.
        /// </remarks>
        /// <param name="body">A <see cref="KeyUsageBatchRequest"/> parameter.</param>
        /// <param name="idempotencyKey">Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the workspace principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call.</param>
        /// <param name="retryConfig">The retry configuration to use for this operation.</param>
        /// <param name="cancellationToken">An optional cancellation token to signal when the operation should be aborted.</param>
        /// <returns>An awaitable task that returns a <see cref="RecordKeyUsageResponse"/> object when completed.</returns>
        /// <exception cref="ArgumentNullException">The required parameter <paramref name="body"/> is null.</exception>
        /// <exception cref="OperationCanceledException">The operation was aborted via the provided cancellation token.</exception>
        /// <exception cref="HttpRequestException">The HTTP request failed due to network issues.</exception>
        /// <exception cref="ResponseValidationException">The response body could not be deserialized.</exception>
        /// <exception cref="BadRequestException">The body is not JSON or does not match the batch schema (no events or more than 500, an unknown member, a key_sha256 that is not 64 lower-case hex, an over-long user_agent, a sample_rate outside (0, 1]), or Idempotency-Key is missing or malformed. invalid_params names the offending fields, not their values. Thrown when the API returns a 400 response.</exception>
        /// <exception cref="AuthenticationException">No API key, or one that is malformed, unknown, revoked or expired. Thrown when the API returns a 401 response.</exception>
        /// <exception cref="PermissionDeniedException">The API key is valid but may not use this operation: it lacks the operation's scope, or its workspace is not entitled or is suspended. Thrown when the API returns a 403 response.</exception>
        /// <exception cref="NotFoundException">No unexpired assessment with this id for the calling workspace, or a malformed assessment_id. Thrown when the API returns a 404 response.</exception>
        /// <exception cref="ConflictException">The Idempotency-Key was already used by this workspace, within 24 hours, with a different body. A reuse with the same body is not a conflict, even while the first request is in flight: it replays the latest assessment. Thrown when the API returns a 409 response.</exception>
        /// <exception cref="RateLimitException">The workspace's rate or concurrency limit is exhausted. Retry-After says when to retry; the provider applies its own fallback meanwhile. Thrown when the API returns a 429 response.</exception>
        /// <exception cref="APIStatusException">Default API Exception. Thrown when the API returns a 4XX or 5XX response.</exception>
        public async Task<RecordKeyUsageResponse> RecordAsync(
            KeyUsageBatchRequest body,
            string? idempotencyKey = null,
            RetryConfig? retryConfig = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (body == null) throw new ArgumentNullException(nameof(body));

            var request = new RecordKeyUsageRequest() {
                Body = body,
                IdempotencyKey = idempotencyKey,
            };

            string baseUrl = this.SDKConfiguration.GetTemplatedServerUrl();
            var urlString = baseUrl + "/v1/key_usage/batches";

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

            var hookCtx = new HookContext(SDKConfiguration, baseUrl, "recordKeyUsage", null, SDKConfiguration.SecuritySource, cancellationToken);

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
                    KeyUsageBatch obj;
                    try
                    {
                        obj = ResponseBodyDeserializer.DeserializeNotNull<KeyUsageBatch>(httpResponseBody, NullValueHandling.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new ResponseValidationException("Failed to deserialize response body into KeyUsageBatch.", httpResponse, httpResponseBody, ex);
                    }

                    var result = obj!;
                    var response = new RecordKeyUsageResponse() {
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