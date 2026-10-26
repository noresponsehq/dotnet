<a href="https://noresponse.org"><img src="https://raw.githubusercontent.com/noresponsehq/.github/main/profile/sdk-header.png" alt="NoResponse · Identity Intelligence" width="100%"></a>

# NoResponse for .NET

Assess sign-ups, logins and purchases with the NoResponse Identity Intelligence API.

.NET 8+ · C#.

[Documentation](https://docs.noresponse.org/sdks/csharp) · [API reference](https://docs.noresponse.org/api-reference) · [Releases](https://github.com/noresponsehq/dotnet/releases)

## Install

There is no NuGet package yet. Clone the tag you want beside your solution and reference the project. Keep the clone outside your project folder, or the SDK's source files compile twice.

```bash
# from the folder that holds your project folder
git clone --branch v8.0.2 https://github.com/noresponsehq/dotnet.git noresponse-dotnet
dotnet add MyApp reference noresponse-dotnet/src/NoResponse/NoResponse.csproj
```

<details>
<summary>Install as a Git submodule</summary>

In a repository of your own, add it as a submodule and check out the tag before you commit:

```bash
git submodule add https://github.com/noresponsehq/dotnet.git third_party/noresponse-dotnet
git -C third_party/noresponse-dotnet checkout v8.0.2
git add third_party/noresponse-dotnet
```

Use the [latest release tag](https://github.com/noresponsehq/dotnet/tags).

</details>

## Quick start

```csharp
using NoResponse;
using NoResponse.Models.Shared;

var client = new NoResponseClient(); // reads NORESPONSE_API_KEY

var response = await client.Assessments.CreateAsync(new AssessmentRequest
{
    AccountId = "usr_8f2k1",
    Flow = Flow.AccountCreation,
    Identifiers = new AssessmentRequestIdentifiers { Email = "jordan.lee@example.com" },
});

var assessment = response.Result;
if (Status.Pending.Equals(assessment.Status))
{
    // No decision yet. Apply your fallback now, then wait or take the webhook.
    assessment = await client.Assessments.WaitAsync(assessment.Id);
}

Console.WriteLine(assessment.Account?.Risk.Decision); // allow, step_up, review or block
Console.WriteLine(assessment.Detail.Risk?.Score);     // this assessment's own facts are under Detail
```

Every method returns an envelope: `Result` holds the object and `Headers` the response headers (`X-Request-ID`, `Retry-After`, `Idempotent-Replayed`).

## Go further

| Task | Guide |
| --- | --- |
| Configure this client and explore its methods | [.NET SDK guide](https://docs.noresponse.org/sdks/csharp) |
| Act when an account's decision changes | [Late results](https://docs.noresponse.org/guides/late-results) |
| Explore request and response fields | [API reference](https://docs.noresponse.org/api-reference) |

<details>
<summary><strong>SDK reference</strong> · configuration, methods, errors and webhooks</summary>

[Configure the client](#configure-the-client) · [Assessments](#assessments) · [Keys](#keys) · [Errors](#errors) · [Retries](#retries) · [Issued keys](#issued-keys) · [Webhooks](#webhooks) · [Changes since 2.0.0](#changes-since-200)

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
        Flow = Flow.AccountCreation,
        Identifiers = new AssessmentRequestIdentifiers { Email = "jordan.lee@example.com" },
        ResponseMode = ResponseMode.Detailed, // detailed needs the detailed scope
    },
    idempotencyKey: "account-usr_8f2k1-0001");

var latest = await client.Assessments.GetAsync(created.Result.Id);
var settled = await client.Assessments.WaitAsync(created.Result.Id, timeout: TimeSpan.FromSeconds(30));
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
    created.Result.Id,
    new AssessmentOutcomeRequest { Outcome = Outcome.Chargeback, ExternalRef = "case-42" });
```

### Idempotency

`CreateAsync` and `RecordOutcomeAsync` send a fresh UUID as the `Idempotency-Key` when you pass none, and the same key on every retry of that call, so a retried request is recorded once. Pass your own key to make a retry from your side replay the same assessment: the API answers the latest version with `Idempotent-Replayed: true`. The same key with a different body answers 409 (`ConflictException`).

`Assessments.DeleteAsync(assessmentId)` deletes one assessment (`history` scope); the answer's `Account.CurrentAssessmentId` names the assessment the account's risk now comes from, null when none is left, and a repeat answers 404.

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

When your platform issues an API key to the new account, send only its hash, as `issued_credential.sha256`; the key itself never leaves your backend.

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
    endpointId, new WebhookTestRequest { Event = WebhookTestRequestEvent.AssessmentUpdated }); // its AssessmentUpdatedEvent.Test is true
```

`ListAsync()`, `RotateSigningKeyAsync(endpointId, body)` (the old key verifies for 7 more days) and `DeleteAsync(endpointId)` manage it afterwards.

Verify the exact bytes you received, then act on the typed event. `secrets` maps each `whk_` key id we give you to its secret; hold both during a rotation.

```csharp
using NoResponse.Models.Shared;
using NoResponse.WebhookVerification;

app.MapPost("/webhooks/noresponse", async (HttpRequest request) =>
{
    using var buffer = new MemoryStream();
    await request.Body.CopyToAsync(buffer);
    try
    {
        var secrets = new Dictionary<string, string> { [keyId] = secret };
        var webhookEvent = Webhook.ConstructEvent(buffer.ToArray(), request.Headers, secrets);
        if (webhookEvent.EndpointVerificationEvent is { } verification)
        {
            return Results.Json(new { challenge = verification.Data.Challenge });
        }
        if (webhookEvent.AssessmentUpdatedEvent is { Test: true }) { return Results.NoContent(); } // SendTestEventAsync's fixed example assessment: apply nothing
        if (webhookEvent.AccountDeletedEvent is { } erased) { /* account.deleted: you erased erased.Data.AccountId with Accounts.DeleteAsync */ }
        else if (webhookEvent.WorkspaceDeletedEvent is { } gone) { /* workspace.deleted: the workspace is deleted; the last event for gone.Data.Environment */ }
        else if (webhookEvent.WebhookEndpointDisabledEvent is { } disabled) { /* disabled.Data.EndpointId failed for 72 hours: fix it, verify it again */ }
        else if (webhookEvent.AccountRiskChangedEvent is { } changed) { /* account.risk_changed, the one event to act on: this verifier alone does not enforce it: use LateResults.HandleRiskChangeAsync (below) as this endpoint's receiver */ }
        else if (webhookEvent.AssessmentUpdatedEvent?.Data.AssessmentExpired is { } expired) { /* forget expired.AssessmentId: GET now answers 404 */ }
        else if (webhookEvent.AssessmentUpdatedEvent?.Data.Assessment is { } assessment) { /* progress only: apply nothing */ }
        return Results.NoContent();
    }
    catch (WebhookVerificationException)
    {
        return Results.BadRequest();
    }
});
```

`Webhook.ConstructEvent` is generated from the API document. It refuses a missing, empty or repeated header (in any letter case), a delivery more than 300 seconds from now (`tolerance:` changes it), an unknown key id, a bad signature or a body that is not a known event, and `WebhookVerificationException.Code` says which: `"missing_header"`, `"duplicate_header"`, `"invalid_timestamp"`, `"unknown_key"`, `"invalid_signature"` or `"invalid_payload"`, also available as constants on the exception class. It returns a `WebhookEvent` union: `Type.Value` is the wire type, and exactly one of `AssessmentUpdatedEvent`, `EndpointVerificationEvent`, `AccountDeletedEvent`, `WorkspaceDeletedEvent`, `WebhookEndpointDisabledEvent` and `AccountRiskChangedEvent` is set, each with `Id`, `CreatedAt`, `Test` and `Data`. Deliveries are at least once: deduplicate on the event's `Id`.


### Late results

`account.risk_changed` only wakes your handler. `LateResults.HandleRiskChangeAsync` verifies the delivery with `ConstructEvent`, reads the account's current risk with `Accounts.GetAsync` (the event's own decision is ignored), asks your `GetAccountState` where the account is in your flow, and acts on the decision of that state's flow: an `onboarding` or `provisional` account with an `account_creation` decision of `review` or `block` fails onboarding or is refused activation; an `active` account is signed out on a `login` decision of `review`, and restricted and signed out on `block`. A `restricted` account is unrestricted when its `login` decision is `allow` again, only if you set `Flows["login"] = new FlowPolicy(Reinstate: true)`. Each action, or `none`, is recorded with `Accounts.RecordEnforcementAsync` at the risk version read. Nothing is ever deleted: Clerk bans (reversed by unban), Auth0 sets `blocked`, and WorkOS, which has no block on a user, gets `noresponse_state` in the user's metadata and its sessions revoked.

```csharp
using NoResponse.LateResults;

var provider = LateResults.IdentityProvider("clerk", new IdentityProviderOptions { Token = Environment.GetEnvironmentVariable("CLERK_SECRET_KEY")! });
var result = await LateResults.HandleRiskChangeAsync(client, buffer.ToArray(), request.Headers, secrets, new EnforcementOptions
{
    Provider = provider,
    GetAccountState = account => users.StateOfAsync(account.Id), // AccountStateAnswer, or null for an account you do not hold
});
// Store result.RiskVersion and return it as EnforcedRiskVersion, so a replayed or older event changes nothing.
```

A refused provider call is recorded as failed and thrown as `ProviderActionException` (`Provider`, `Status`, `Code`): answer 5xx so the delivery is retried. `LateResults.EnforceAccountAsync` enforces an account from `Accounts.ListChangedAsync` with `unenforced: true`, to catch up on missed deliveries. For your own user store, `LateResults.CustomProvider` takes one function per action.

A held flow (workspace settings `admission: held`) answers `held` until its investigation completes or `hold_seconds` pass, and the handler records `none` for it. When the final decision arrives as `account.risk_changed`, an `onboarding` or `provisional` account fails onboarding or is refused activation on `review` or `block`. On `allow` the handler records `activate` (no provider call; a custom provider runs its own `activate`), so admit the account when the result's enforcements hold it. An account at `allow` that needs no change, such as an `active` one after a held login, is recorded as `none`. Either way the account leaves the unenforced list. See https://docs.noresponse.org/guides/late-results.

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

</details>

## Support

[Open an issue](https://github.com/noresponsehq/dotnet/issues) and include the request ID from the response or error.
Keep API keys and personal data out of issues.

[Apache-2.0](LICENSE).
