# NoResponse

Developer-friendly & type-safe Csharp SDK specifically catered to leverage *NoResponse* API.


<br /><br />
> [!IMPORTANT]
> This SDK is not yet ready for production use. Delete this section before > publishing to a package manager.

<!-- Start Summary [summary] -->
## Summary

NoResponse API: Server-side signup and gateway risk assessment: a recommendation, polling and signed webhooks.

Assess a signup or gateway event, poll a pending assessment, page through an account's history, record what you later learned, and rotate your API key. Every response carries X-Request-ID; every error is an RFC 9457 problem document. Keep the API key on your server.
<!-- End Summary [summary] -->

<!-- Start Table of Contents [toc] -->
## Table of Contents
<!-- $toc-max-depth=2 -->
* [NoResponse](#noresponse)
  * [SDK Installation](#sdk-installation)
  * [SDK Example Usage](#sdk-example-usage)
  * [Authentication](#authentication)
  * [Available Resources and Operations](#available-resources-and-operations)
  * [Pagination](#pagination)
  * [Retries](#retries)
  * [Error Handling](#error-handling)
  * [Server Selection](#server-selection)
  * [Custom HTTP Client](#custom-http-client)
* [Development](#development)
  * [Maturity](#maturity)
  * [Contributions](#contributions)

<!-- End Table of Contents [toc] -->

<!-- Start SDK Installation [installation] -->
## SDK Installation

To add a reference to a local instance of the SDK in a .NET project:
```bash
dotnet add reference src/NoResponse/NoResponse.csproj
```
<!-- End SDK Installation [installation] -->

<!-- Start SDK Example Usage [usage] -->
## SDK Example Usage

### Example

```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseSDK(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

var res = await sdk.Assessments.CreateAsync(
    idempotencyKey: "<value>",
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
        },
    }
);

// handle response
```
<!-- End SDK Example Usage [usage] -->

<!-- Start Authentication [security] -->
## Authentication

### Per-Client Security Schemes

This SDK supports the following security scheme globally:

| Name     | Type | Scheme      |
| -------- | ---- | ----------- |
| `ApiKey` | http | HTTP Bearer |

To authenticate with the API the `ApiKey` parameter must be set when initializing the SDK client instance. For example:
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseSDK(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

var res = await sdk.Assessments.CreateAsync(
    idempotencyKey: "<value>",
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
        },
    }
);

// handle response
```
<!-- End Authentication [security] -->

<!-- Start Available Resources and Operations [operations] -->
## Available Resources and Operations

<details open>
<summary>Available methods</summary>

### [Assessments](docs/sdks/assessments/README.md)

* [Create](docs/sdks/assessments/README.md#create) - Assess identifiers for a signup or gateway decision.
* [Get](docs/sdks/assessments/README.md#get) - Poll the authoritative assessment of one assessment.
* [RecordOutcome](docs/sdks/assessments/README.md#recordoutcome) - Report what was later learned about an assessed account.
* [Query](docs/sdks/assessments/README.md#query) - Page through one account's unexpired assessments.

### [Keys](docs/sdks/keys/README.md)

* [Rotate](docs/sdks/keys/README.md#rotate) - Rotate the API key that signs the request.

</details>
<!-- End Available Resources and Operations [operations] -->

<!-- Start Pagination [pagination] -->
## Pagination

Some of the endpoints in this SDK support pagination. To use pagination, you make your SDK calls as usual, but the
returned response object will have a `Next` method that can be called to pull down the next group of results. If the
return value of `Next` is `null`, then there are no more pages to be fetched.

Here's an example of one such pagination call:
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;

var sdk = new NoResponseSDK(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

AssessmentQuery req = new AssessmentQuery() {
    AccountId = "acct_7f3a9c",
};

QueryAccountAssessmentsResponse? res = await sdk.Assessments.QueryAsync(req);

while (res != null)
{
    // handle items

    res = await res.Next!();
}
```
<!-- End Pagination [pagination] -->

<!-- Start Retries [retries] -->
## Retries

Some of the endpoints in this SDK support retries. If you use the SDK without any configuration, it will fall back to the default retry strategy provided by the API. However, the default retry strategy can be overridden on a per-operation basis, or across the entire SDK.

To change the default retry strategy for a single API call, simply pass a `RetryConfig` to the call:
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseSDK(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

var res = await sdk.Assessments.CreateAsync(
    retryConfig: new RetryConfig(
        strategy: RetryConfig.RetryStrategy.BACKOFF,
        backoff: new BackoffStrategy(
            initialIntervalMs: 1L,
            maxIntervalMs: 50L,
            maxElapsedTimeMs: 100L,
            exponent: 1.1
        ),
        retryConnectionErrors: false
    ),
    idempotencyKey: "<value>",
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
        },
    }
);

// handle response
```

If you'd like to override the default retry strategy for all operations that support retries, you can use the `RetryConfig` optional parameter when intitializing the SDK:
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseSDK(
    retryConfig: new RetryConfig(
        strategy: RetryConfig.RetryStrategy.BACKOFF,
        backoff: new BackoffStrategy(
            initialIntervalMs: 1L,
            maxIntervalMs: 50L,
            maxElapsedTimeMs: 100L,
            exponent: 1.1
        ),
        retryConnectionErrors: false
    ),
    apiKey: "<YOUR_BEARER_TOKEN_HERE>"
);

var res = await sdk.Assessments.CreateAsync(
    idempotencyKey: "<value>",
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
        },
    }
);

// handle response
```
<!-- End Retries [retries] -->

<!-- Start Error Handling [errors] -->
## Error Handling

[`NoResponseException`](./src/NoResponse/Models/Errors/NoResponseException.cs) is the base exception class for all HTTP error responses. It has the following properties:

| Property      | Type                  | Description           |
|---------------|-----------------------|-----------------------|
| `Message`     | *string*              | Error message         |
| `StatusCode`  | *int*                 | HTTP status code      |
| `Headers`     | *HttpResponseHeaders* | HTTP headers          |
| `ContentType` | *string?*             | HTTP content type     |
| `RawResponse` | *HttpResponseMessage* | HTTP response object  |
| `Body`        | *string*              | HTTP response body    |

Some exceptions in this SDK include an additional `Payload` field, which will contain deserialized custom error data when present. Possible exceptions are listed in the [Error Classes](#error-classes) section.

### Example

```csharp
using NoResponse;
using NoResponse.Models.Errors;
using NoResponse.Models.Shared;

var sdk = new NoResponseSDK(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

try
{
    var res = await sdk.Assessments.CreateAsync(
        idempotencyKey: "<value>",
        body: new AssessmentRequest() {
            AccountId = "acct_7f3a9c",
            Identifiers = new AssessmentRequestIdentifiers() {
                Email = "new.user@example.com",
            },
        }
    );

    // handle response
}
catch (NoResponseException ex) // all SDK exceptions inherit from NoResponseException
{
    // ex.ToString() provides a detailed error message
    System.Console.WriteLine(ex);

    // Base exception fields
    HttpResponseMessage rawResponse = ex.RawResponse;
    HttpResponseHeaders headers = ex.Headers;
    int statusCode = ex.StatusCode;
    string? contentType = ex.ContentType;
    var responseBody = ex.Body;

    if (ex is BadRequestError) // different exceptions may be thrown depending on the method
    {
        // Check error data fields
        BadRequestErrorPayload payload = ex.Payload;
        string Type = payload.Type;
        string Title = payload.Title;
        // ...
    }

    // An underlying cause may be provided
    if (ex.InnerException != null)
    {
        Exception cause = ex.InnerException;
    }
}
catch (OperationCanceledException ex)
{
    // CancellationToken was cancelled
}
catch (System.Net.Http.HttpRequestException ex)
{
    // Check ex.InnerException for Network connectivity errors
}
```

### Error Classes

**Primary exceptions:**
* [`NoResponseException`](./src/NoResponse/Models/Errors/NoResponseException.cs): The base class for HTTP error responses.
  * [`BadRequestError`](./src/NoResponse/Models/Errors/BadRequestError.cs): HTTP 400 problem document. Status code `400`.
  * [`AuthenticationError`](./src/NoResponse/Models/Errors/AuthenticationError.cs): HTTP 401 problem document. Status code `401`.
  * [`PermissionDeniedError`](./src/NoResponse/Models/Errors/PermissionDeniedError.cs): HTTP 403 problem document. Status code `403`.
  * [`NotFoundError`](./src/NoResponse/Models/Errors/NotFoundError.cs): HTTP 404 problem document. Status code `404`.
  * [`ConflictError`](./src/NoResponse/Models/Errors/ConflictError.cs): HTTP 409 problem document. Status code `409`.
  * [`RateLimitError`](./src/NoResponse/Models/Errors/RateLimitError.cs): HTTP 429 problem document. Status code `429`.

<details><summary>Less common exceptions (2)</summary>

* [`System.Net.Http.HttpRequestException`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httprequestexception): Network connectivity error. For more details about the underlying cause, inspect the `ex.InnerException`.

* Inheriting from [`NoResponseException`](./src/NoResponse/Models/Errors/NoResponseException.cs):
  * [`ResponseValidationError`](./src/NoResponse/Models/Errors/ResponseValidationError.cs): Thrown when the response data could not be deserialized into the expected type.
</details>
<!-- End Error Handling [errors] -->

<!-- Start Server Selection [server] -->
## Server Selection

### Override Server URL Per-Client

The default server can be overridden globally by passing a URL to the `serverUrl: string` optional parameter when initializing the SDK client instance. For example:
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseSDK(
    serverUrl: "https://api.noresponse.org",
    apiKey: "<YOUR_BEARER_TOKEN_HERE>"
);

var res = await sdk.Assessments.CreateAsync(
    idempotencyKey: "<value>",
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
        },
    }
);

// handle response
```
<!-- End Server Selection [server] -->

<!-- Start Custom HTTP Client [http-client] -->
## Custom HTTP Client

The C# SDK makes API calls using an `INoResponseHttpClient` that wraps the native
[HttpClient](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient). This
client provides the ability to attach hooks around the request lifecycle that can be used to modify the request or handle
errors and response.

The `INoResponseHttpClient` interface allows you to either use the default `NoResponseHttpClient` that comes with the SDK,
or provide your own custom implementation with customized configuration such as custom message handlers, timeouts,
connection pooling, and other HTTP client settings.

The following example shows how to create a custom HTTP client with request modification and error handling:

```csharp
using NoResponse;
using NoResponse.Utils;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

// Create a custom HTTP client
public class CustomHttpClient : INoResponseHttpClient
{
    private readonly INoResponseHttpClient _defaultClient;

    public CustomHttpClient()
    {
        _defaultClient = new NoResponseHttpClient();
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken? cancellationToken = null)
    {
        // Add custom header and timeout
        request.Headers.Add("x-custom-header", "custom value");
        request.Headers.Add("x-request-timeout", "30");
        
        try
        {
            var response = await _defaultClient.SendAsync(request, cancellationToken);
            // Log successful response
            Console.WriteLine($"Request successful: {response.StatusCode}");
            return response;
        }
        catch (Exception error)
        {
            // Log error
            Console.WriteLine($"Request failed: {error.Message}");
            throw;
        }
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
        _defaultClient?.Dispose();
    }
}

// Use the custom HTTP client with the SDK
var customHttpClient = new CustomHttpClient();
var sdk = new NoResponse(client: customHttpClient);
```

<details>
<summary>You can also provide a completely custom HTTP client with your own configuration:</summary>

```csharp
using NoResponse.Utils;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

// Custom HTTP client with custom configuration
public class AdvancedHttpClient : INoResponseHttpClient
{
    private readonly HttpClient _httpClient;

    public AdvancedHttpClient()
    {
        var handler = new HttpClientHandler()
        {
            MaxConnectionsPerServer = 10,
            // ServerCertificateCustomValidationCallback = customCertValidation, // Custom SSL validation if needed
        };

        _httpClient = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromSeconds(30)
        };
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken? cancellationToken = null)
    {
        return await _httpClient.SendAsync(request, cancellationToken ?? CancellationToken.None);
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}

var sdk = NoResponse.Builder()
    .WithClient(new AdvancedHttpClient())
    .Build();
```
</details>

<details>
<summary>For simple debugging, you can enable request/response logging by implementing a custom client:</summary>

```csharp
public class LoggingHttpClient : INoResponseHttpClient
{
    private readonly INoResponseHttpClient _innerClient;

    public LoggingHttpClient(INoResponseHttpClient innerClient = null)
    {
        _innerClient = innerClient ?? new NoResponseHttpClient();
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken? cancellationToken = null)
    {
        // Log request
        Console.WriteLine($"Sending {request.Method} request to {request.RequestUri}");
        
        var response = await _innerClient.SendAsync(request, cancellationToken);
        
        // Log response
        Console.WriteLine($"Received {response.StatusCode} response");
        
        return response;
    }

    public void Dispose() => _innerClient?.Dispose();
}

var sdk = new NoResponse(client: new LoggingHttpClient());
```
</details>

The SDK also provides built-in hook support through the `SDKConfiguration.Hooks` system, which automatically handles
`BeforeRequestAsync`, `AfterSuccessAsync`, and `AfterErrorAsync` hooks for advanced request lifecycle management.
<!-- End Custom HTTP Client [http-client] -->

<!-- Placeholder for Future NoResponse SDK Sections -->

# Development

## Maturity

This SDK is in beta, and there may be breaking changes between versions without a major version update. Therefore, we recommend pinning usage
to a specific package version. This way, you can install the same version each time without breaking changes unless you are intentionally
looking for the latest version.

## Contributions

While we value open-source contributions to this SDK, this library is generated programmatically. Any manual changes added to internal files will be overwritten on the next generation. 
We look forward to hearing your feedback. Feel free to open a PR or an issue with a proof of concept and we'll do our best to include it in a future release. 
