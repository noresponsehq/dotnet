# Integrations

## Overview

Hosted endpoints that answer your sign-up and sign-in hooks.

### Available Operations

* [Create](#create) - Register a hosted provider integration.
* [List](#list) - List the workspace's integrations.
* [Delete](#delete) - Delete an integration.
* [Rotate](#rotate) - Replace an integration's secret.

## Create

Registers a WorkOS, Auth0 or Supabase integration and returns its hosted endpoint, url, so a sign-up or sign-in is scored with no server of your own. For WorkOS, send the Actions signing secret from the WorkOS dashboard and point the user registration and authentication Actions at url. For Supabase, send the hook secret Supabase generates (v1,whsec_...) and set url as the Before User Created hook's HTTPS endpoint. For Auth0, the answer's secret is the shared secret of the NoResponse Auth0 Action, shown once: store it as an Action secret. hook_points defaults to every hook point the platform has (Supabase has only pre_registration); an event that is not listed is allowed without an assessment. The endpoint waits for the full assessment for at most 2.5 seconds (WorkOS, Supabase) or 4.5 seconds (Auth0), denies only when risk.block is true and allows on any failure. Requires the integrations scope; at most 10 per account. Not idempotent: a retry after a lost answer registers another integration.

### Example Usage: auth0

<!-- UsageSnippet language="csharp" operationID="createIntegration" method="post" path="/v1/integrations" example="auth0" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

IntegrationRequest req = new IntegrationRequest() {
    Platform = IntegrationPlatform.Auth0,
};

var res = await sdk.Integrations.CreateAsync(req);

// handle response
```
### Example Usage: supabase

<!-- UsageSnippet language="csharp" operationID="createIntegration" method="post" path="/v1/integrations" example="supabase" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

IntegrationRequest req = new IntegrationRequest() {
    Platform = IntegrationPlatform.Supabase,
    SigningSecret = "v1,whsec_dGVzdC1zdXBhYmFzZS1ob29rLXNlY3JldC1ub3QtcmVhbA==",
};

var res = await sdk.Integrations.CreateAsync(req);

// handle response
```
### Example Usage: workos

<!-- UsageSnippet language="csharp" operationID="createIntegration" method="post" path="/v1/integrations" example="workos" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

IntegrationRequest req = new IntegrationRequest() {
    Platform = IntegrationPlatform.Workos,
    HookPoints = new List<IntegrationHookPoint>() {
        IntegrationHookPoint.PreRegistration,
        IntegrationHookPoint.PostLogin,
    },
    SigningSecret = "wos_action_secret_k3Yx8pQ2mN7vR4tW",
};

var res = await sdk.Integrations.CreateAsync(req);

// handle response
```

### Parameters

| Parameter                                                       | Type                                                            | Required                                                        | Description                                                     |
| --------------------------------------------------------------- | --------------------------------------------------------------- | --------------------------------------------------------------- | --------------------------------------------------------------- |
| `request`                                                       | [IntegrationRequest](../../Models/Shared/IntegrationRequest.md) | :heavy_check_mark:                                              | The request object to use for the request.                      |

### Response

**[CreateIntegrationResponse](../../Models/Operations/CreateIntegrationResponse.md)**

### Errors

| Error Type                                         | Status Code                                        | Content Type                                       |
| -------------------------------------------------- | -------------------------------------------------- | -------------------------------------------------- |
| NoResponse.Models.Errors.BadRequestException       | 400                                                | application/problem+json                           |
| NoResponse.Models.Errors.AuthenticationException   | 401                                                | application/problem+json                           |
| NoResponse.Models.Errors.PermissionDeniedException | 403                                                | application/problem+json                           |
| NoResponse.Models.Errors.NotFoundException         | 404                                                | application/problem+json                           |
| NoResponse.Models.Errors.ConflictException         | 409                                                | application/problem+json                           |
| NoResponse.Models.Errors.RateLimitException        | 429                                                | application/problem+json                           |
| NoResponse.Models.Errors.APIStatusException        | 4XX, 5XX                                           | \*/\*                                              |

## List

Every integration, oldest first. Listings carry no secrets. Requires the integrations scope.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listIntegrations" method="get" path="/v1/integrations" example="list" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Integrations.ListAsync();

// handle response
```

### Response

**[ListIntegrationsResponse](../../Models/Operations/ListIntegrationsResponse.md)**

### Errors

| Error Type                                         | Status Code                                        | Content Type                                       |
| -------------------------------------------------- | -------------------------------------------------- | -------------------------------------------------- |
| NoResponse.Models.Errors.BadRequestException       | 400                                                | application/problem+json                           |
| NoResponse.Models.Errors.AuthenticationException   | 401                                                | application/problem+json                           |
| NoResponse.Models.Errors.PermissionDeniedException | 403                                                | application/problem+json                           |
| NoResponse.Models.Errors.NotFoundException         | 404                                                | application/problem+json                           |
| NoResponse.Models.Errors.ConflictException         | 409                                                | application/problem+json                           |
| NoResponse.Models.Errors.RateLimitException        | 429                                                | application/problem+json                           |
| NoResponse.Models.Errors.APIStatusException        | 4XX, 5XX                                           | \*/\*                                              |

## Delete

Removes the integration and its secrets; its endpoint answers 404 from then on, so remove the Action from the provider first. Requires the integrations scope. An unknown or already deleted integration answers 404.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="deleteIntegration" method="delete" path="/v1/integrations/{integration_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Integrations.DeleteAsync(integrationId: "int_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```

### Parameters

| Parameter                                                                                                                             | Type                                                                                                                                  | Required                                                                                                                              | Description                                                                                                                           | Example                                                                                                                               |
| ------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------- |
| `IntegrationId`                                                                                                                       | *string*                                                                                                                              | :heavy_check_mark:                                                                                                                    | Path segment, matched case-sensitively. A value that does not match the integration_id shape, or another workspace's id, answers 404. | int_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                        |

### Response

**[DeleteIntegrationResponse](../../Models/Operations/DeleteIntegrationResponse.md)**

### Errors

| Error Type                                         | Status Code                                        | Content Type                                       |
| -------------------------------------------------- | -------------------------------------------------- | -------------------------------------------------- |
| NoResponse.Models.Errors.BadRequestException       | 400                                                | application/problem+json                           |
| NoResponse.Models.Errors.AuthenticationException   | 401                                                | application/problem+json                           |
| NoResponse.Models.Errors.PermissionDeniedException | 403                                                | application/problem+json                           |
| NoResponse.Models.Errors.NotFoundException         | 404                                                | application/problem+json                           |
| NoResponse.Models.Errors.ConflictException         | 409                                                | application/problem+json                           |
| NoResponse.Models.Errors.RateLimitException        | 429                                                | application/problem+json                           |
| NoResponse.Models.Errors.APIStatusException        | 4XX, 5XX                                           | \*/\*                                              |

## Rotate

For Auth0, send {} and the answer's secret is the new shared secret, shown once. For WorkOS, regenerate the Actions secret in the WorkOS dashboard and send it as signing_secret; for Supabase, generate a new hook secret in the Supabase dashboard and send it as signing_secret. The replaced secret is still accepted for 24 hours (previous_secret_expires_at). Requires the integrations scope. Not idempotent: a retry after a lost answer rotates again.

### Example Usage: auth0

<!-- UsageSnippet language="csharp" operationID="rotateIntegrationSecret" method="post" path="/v1/integrations/{integration_id}/rotate" example="auth0" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Integrations.RotateAsync(
    integrationId: "int_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new IntegrationRotationRequest() {}
);

// handle response
```
### Example Usage: workos

<!-- UsageSnippet language="csharp" operationID="rotateIntegrationSecret" method="post" path="/v1/integrations/{integration_id}/rotate" example="workos" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Integrations.RotateAsync(
    integrationId: "int_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new IntegrationRotationRequest() {
        SigningSecret = "wos_action_secret_Z9cD1eF5gH0jL4sP",
    }
);

// handle response
```

### Parameters

| Parameter                                                                                                                             | Type                                                                                                                                  | Required                                                                                                                              | Description                                                                                                                           | Example                                                                                                                               |
| ------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------- |
| `IntegrationId`                                                                                                                       | *string*                                                                                                                              | :heavy_check_mark:                                                                                                                    | Path segment, matched case-sensitively. A value that does not match the integration_id shape, or another workspace's id, answers 404. | int_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                        |
| `Body`                                                                                                                                | [IntegrationRotationRequest](../../Models/Shared/IntegrationRotationRequest.md)                                                       | :heavy_check_mark:                                                                                                                    | N/A                                                                                                                                   |                                                                                                                                       |

### Response

**[RotateIntegrationSecretResponse](../../Models/Operations/RotateIntegrationSecretResponse.md)**

### Errors

| Error Type                                         | Status Code                                        | Content Type                                       |
| -------------------------------------------------- | -------------------------------------------------- | -------------------------------------------------- |
| NoResponse.Models.Errors.BadRequestException       | 400                                                | application/problem+json                           |
| NoResponse.Models.Errors.AuthenticationException   | 401                                                | application/problem+json                           |
| NoResponse.Models.Errors.PermissionDeniedException | 403                                                | application/problem+json                           |
| NoResponse.Models.Errors.NotFoundException         | 404                                                | application/problem+json                           |
| NoResponse.Models.Errors.ConflictException         | 409                                                | application/problem+json                           |
| NoResponse.Models.Errors.RateLimitException        | 429                                                | application/problem+json                           |
| NoResponse.Models.Errors.APIStatusException        | 4XX, 5XX                                           | \*/\*                                              |