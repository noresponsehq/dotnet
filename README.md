# NoResponse for .NET

The NoResponse client for .NET 8 and later. It assesses a signup or gateway event, polls a pending assessment, pages through an account's history, records what you later learned, rotates your API key and verifies webhooks. The client is generated from the NoResponse OpenAPI document; webhook verification and the wait helper are hand-written.

Keep the API key on your server.

## Install

There is no NuGet package yet. Clone the tag you want beside your solution and reference the project. Keep the clone outside your project folder, or the SDK's source files compile twice.

```bash
# from the folder that holds your project folder
git clone --branch v2.1.11 https://github.com/noresponsehq/dotnet.git noresponse-dotnet
dotnet add MyApp reference noresponse-dotnet/src/NoResponse/NoResponse.csproj
```

In a repository of your own, add it as a submodule and check out the tag before you commit:

```bash
git submodule add https://github.com/noresponsehq/dotnet.git third_party/noresponse-dotnet
git -C third_party/noresponse-dotnet checkout v2.1.11
git add third_party/noresponse-dotnet
```

Use the latest tag from https://github.com/noresponsehq/dotnet/tags.

## Quick start

```csharp
using NoResponse;
using NoResponse.Models.Shared;

var client = new NoResponseClient(); // reads NORESPONSE_API_KEY

var response = await client.Assessments.CreateAsync(new AssessmentRequest
{
    AccountId = "usr_8f2k1",
    Identifiers = new AssessmentRequestIdentifiers { Email = "jordan.lee@example.com" },
});

var assessment = response.Result;
if (Status.Pending.Equals(assessment.Status))
{
    // No recommendation yet. Apply your fallback now, then wait or take the webhook.
    assessment = await client.Assessments.WaitAsync(assessment.AssessmentId);
}

Console.WriteLine(assessment.Recommendation); // allow, step_up or review
```

Every method returns an envelope: `Result` holds the object and `Headers` the response headers (`X-Request-ID`, `Retry-After`, `Idempotent-Replayed`).

## Configure the client

```csharp
var client = new NoResponseClient(
    apiKey: key,                     // or apiKeySource: () => key, to read a rotating secret on each request
    serverUrl: "https://api.noresponse.org", // the default
    client: new MyHttpClient(),      // an INoResponseHttpClient, for timeouts, proxies and logging
    retryConfig: new RetryConfig(RetryConfig.RetryStrategy.NONE));
```

With no `apiKey` and no `apiKeySource`, the client reads the key from `NORESPONSE_API_KEY`, and throws `ArgumentException` when that is unset or empty. `NoResponseClient.Builder().WithApiKey(key).Build()` does the same. The default `NoResponseHttpClient` uses a shared `HttpClient`; derive from it or implement `INoResponseHttpClient` to set a timeout. Every method also takes a `CancellationToken`.

## Assessments

```csharp
var created = await client.Assessments.CreateAsync(
    new AssessmentRequest
    {
        AccountId = "usr_8f2k1",
        Identifiers = new AssessmentRequestIdentifiers { Email = "jordan.lee@example.com" },
        ResponseMode = ResponseMode.Detailed, // detailed needs the detailed scope
    },
    idempotencyKey: "signup-usr_8f2k1-0001");

var latest = await client.Assessments.GetAsync(created.Result.AssessmentId);
var settled = await client.Assessments.WaitAsync(created.Result.AssessmentId, timeout: TimeSpan.FromSeconds(30));
```

`WaitAsync` polls `GetAsync` until the status is `Complete`, sleeping the server's `Retry-After` (at least one second) between polls. When the timeout (default 30 s) passes it returns the latest answer if that is `Partial`, with its status still `Partial`, and throws `WaitTimeoutException`, which carries the last pending assessment, if the assessment is still `Pending`.

### Query an account's history

```csharp
var page = await client.Assessments.QueryAsync(new AssessmentQuery { AccountId = "usr_8f2k1", Limit = 20 });
while (page != null)
{
    foreach (var item in page.Result.Items) { /* newest first */ }
    page = await page.Next!();
}
```

### Record an outcome

```csharp
await client.Assessments.RecordOutcomeAsync(
    created.Result.AssessmentId,
    new AssessmentOutcomeRequest { Outcome = Outcome.Chargeback, ExternalRef = "case-42" });
```

### Idempotency

`CreateAsync` and `RecordOutcomeAsync` send a fresh UUID as the `Idempotency-Key` when you pass none, and the same key on every retry of that call, so a retried request is recorded once. Pass your own key to make a retry from your side replay the same assessment: the API answers the latest version with `Idempotent-Replayed: true`. The same key with a different body answers 409 (`ConflictException`).

## Keys

```csharp
var rotation = await client.Keys.RotateAsync(new KeyRotationRequest { OverlapSeconds = 3600 });
// rotation.Result.Key is the new key, shown once. Rotate with the new key next time.
```

## Errors

Every API error derives from `NoResponseException` and carries `StatusCode`, `Headers`, `Body` and, for problem documents, a typed `Payload` with `Type`, `Title`, `Detail`, `Code`, `RequestId` and `InvalidParams`.

| Exception | When |
|---|---|
| `BadRequestException` | 400: the body or a header is malformed; see `Payload.InvalidParams` |
| `AuthenticationException` | 401: missing, malformed, revoked or expired key |
| `PermissionDeniedException` | 403: the key lacks the operation's scope |
| `NotFoundException` | 404: unknown, expired or foreign assessment |
| `ConflictException` | 409: idempotency key reused with a different body, or a key already rotating |
| `RateLimitException` | 429: see the `Retry-After` header |
| `APIStatusException` | any other 4xx or 5xx, including 503 |
| `ResponseValidationException` | a 2xx the client could not read |

```csharp
try
{
    await client.Assessments.CreateAsync(request);
}
catch (RateLimitException e)
{
    Schedule(e.Headers.GetValues("Retry-After"));
}
catch (NoResponseException e)
{
    Log(e.StatusCode, e is BadRequestException b ? b.Payload.RequestId : null);
}
catch (HttpRequestException)
{
    // DNS, TLS or connection failure: apply your fallback.
}
```

## Retries

Connection errors, 408, 429 and every 5xx are retried with exponential backoff (500 ms to 8 s, 30 s in all). 409 and other 4xx are not retried. Override for the client with `retryConfig:` or for one call with `retryConfig:` on the method:

```csharp
await client.Assessments.CreateAsync(request, retryConfig: new RetryConfig(RetryConfig.RetryStrategy.NONE));
```

## Issued keys

When your signup gate issues an API key to the new account, send only its hash, as `issued_credential.sha256`; the key itself never leaves your backend.

```csharp
var sha256 = IssuedCredential.Sha256(issuedKey); // NoResponse.IssuedCredential
```

## Webhooks

Register an endpoint, store its signing secret, then send it a test event to check your receiver. These calls need a key with the `webhooks` scope.

```csharp
var created = await client.WebhookEndpoints.CreateAsync(
    new WebhookEndpointRequest { Url = "https://app.example.com/webhooks/noresponse" });
var signingSecret = created.Result.SigningSecret; // KeyId and Secret, shown once
var endpointId = created.Result.Endpoint.EndpointId;

var check = await client.WebhookEndpoints.SendTestEventAsync(
    endpointId, new WebhookTestRequest { Event = WebhookTestRequestEvent.EndpointVerification }); // check.Result.Verified
await client.WebhookEndpoints.SendTestEventAsync(
    endpointId, new WebhookTestRequest { Event = WebhookTestRequestEvent.AssessmentUpdated }); // webhookEvent.Test is true
```

`ListAsync()`, `RotateSigningKeyAsync(endpointId, body)` (the old key verifies for 7 more days) and `DeleteAsync(endpointId)` manage it afterwards.

Verify the exact bytes you received, then act on the typed event. `secrets` maps each `whk_` key id we give you to its secret; hold both during a rotation.

```csharp
using NoResponse.Webhooks;

app.MapPost("/webhooks/noresponse", async (HttpRequest request) =>
{
    using var buffer = new MemoryStream();
    await request.Body.CopyToAsync(buffer);
    try
    {
        var secrets = new Dictionary<string, string> { [keyId] = secret };
        var webhookEvent = Webhook.ConstructEvent(buffer.ToArray(), request.Headers, secrets);
        if (webhookEvent.Type == "endpoint.verification")
        {
            return Results.Json(Webhook.VerificationResponse(webhookEvent));
        }
        if (webhookEvent.Test) { return Results.NoContent(); } // SendTestEventAsync's fixed example assessment: apply nothing
        if (webhookEvent.IsExpired) { /* forget the assessment: GET now answers 404 */ }
        else if (webhookEvent.Data is Assessment assessment) { /* keep the highest AssessmentVersion */ }
        return Results.NoContent();
    }
    catch (WebhookVerificationException)
    {
        return Results.BadRequest();
    }
});
```

`ConstructEvent` refuses a missing, empty or repeated header, a delivery older than 300 seconds (`tolerance:` changes it), an unknown key id or a bad signature, and `WebhookVerificationException.Code` says which. Deliveries are at least once: deduplicate on `webhookEvent.Id`.


### Late results

An assessment can rise to `review` after the signup was allowed. `LateResults.HandleAsync` verifies the delivery with `ConstructEvent` first, applies your policy (`LateResults.DefaultPolicy` revokes sessions on `review` and does nothing otherwise) and acts through Clerk, WorkOS or Auth0. WorkOS has no block, so its ban deletes the user permanently.

```csharp
using NoResponse.LateResults;

var provider = LateResults.Clerk(Environment.GetEnvironmentVariable("CLERK_SECRET_KEY")!);
var result = await LateResults.HandleAsync(buffer.ToArray(), request.Headers, secrets, provider);
// result.Action is None, RevokeSessions or Ban; result.UserId is set when it acted.
```

If your app has its own login, `LateResults.LogOnly()` logs what it would revoke or ban (to standard output, or pass an `Action<string>`) and calls nothing. Use it to try the handler, then implement `ILateResultProvider` yourself: `Name`, `RevokeSessionsAsync(userId)` and `BanAsync(userId)`.

## Changes since 2.0.0

Names that changed:

| 2.0.0 | Now |
|---|---|
| `NoResponseSDK`, `INoResponseSDK` | `NoResponseClient`, `INoResponseClient` |
| `DefaultHttpClient`, `IDefaultHttpClient` | `NoResponseHttpClient`, `INoResponseHttpClient` |
| `BadRequestError`, `AuthenticationError`, `PermissionDeniedError`, `NotFoundError`, `ConflictError`, `RateLimitError` | `BadRequestException`, `AuthenticationException`, `PermissionDeniedException`, `NotFoundException`, `ConflictException`, `RateLimitException` |
| `BadRequestErrorPayload` and the other `...ErrorPayload` types | `BadRequestExceptionPayload` and the other `...ExceptionPayload` types |
| `client.WaitAsync(id)` | `client.Assessments.WaitAsync(id)` |
| `CreateAsync(idempotencyKey, body)` | `CreateAsync(body, idempotencyKey = null)` |
| `RecordOutcomeAsync(assessmentId, idempotencyKey, body)` | `RecordOutcomeAsync(assessmentId, body, idempotencyKey = null)` |

The `[Obsolete]` copies of the payload fields on each exception are gone: read `Payload`. The error code enums (`BadRequestErrorCode` and the rest) keep their names.

Issues: https://github.com/noresponsehq/dotnet/issues. Quote `RequestId` when you write to us.
