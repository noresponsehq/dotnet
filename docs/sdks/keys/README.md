# Keys

## Overview

Your own API keys.

### Available Operations

* [Rotate](#rotate) - Rotate the API key that signs the request.
* [Create](#create) - Create an API key.
* [List](#list) - List API keys.
* [Revoke](#revoke) - Revoke an API key.

## Rotate

Mints a new key with the same scopes as the key that signs the request and returns it once. The presented key keeps working for overlap_seconds (default 24 hours, from 60 seconds to 7 days), then answers 401. A key accepts one rotation: rotating again with a key already in its overlap answers 409 key_rotation_in_progress, so rotate with the new key. Rotation is not idempotent: a retry after a lost answer mints another key.

### Example Usage: default

<!-- UsageSnippet language="csharp" operationID="rotateApiKey" method="post" path="/v1/keys/rotate" example="default" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

KeyRotationRequest req = new KeyRotationRequest() {};

var res = await sdk.Keys.RotateAsync(req);

// handle response
```
### Example Usage: one_hour

<!-- UsageSnippet language="csharp" operationID="rotateApiKey" method="post" path="/v1/keys/rotate" example="one_hour" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

KeyRotationRequest req = new KeyRotationRequest() {
    OverlapSeconds = 3600,
};

var res = await sdk.Keys.RotateAsync(req);

// handle response
```
### Example Usage: rotated

<!-- UsageSnippet language="csharp" operationID="rotateApiKey" method="post" path="/v1/keys/rotate" example="rotated" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

KeyRotationRequest req = new KeyRotationRequest() {};

var res = await sdk.Keys.RotateAsync(req);

// handle response
```

### Parameters

| Parameter                                                       | Type                                                            | Required                                                        | Description                                                     |
| --------------------------------------------------------------- | --------------------------------------------------------------- | --------------------------------------------------------------- | --------------------------------------------------------------- |
| `request`                                                       | [KeyRotationRequest](../../Models/Shared/KeyRotationRequest.md) | :heavy_check_mark:                                              | The request object to use for the request.                      |

### Response

**[RotateApiKeyResponse](../../Models/Operations/RotateApiKeyResponse.md)**

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

## Create

Creates an API key in the environment of the key that signs the request, with scopes that key holds itself, and returns its secret once in key. Requires the management scope; the new key may hold it too. A scope the signing key lacks answers 403 workspace_forbidden, and the other environment 404 not_found. An environment holds at most 100 keys that authenticate (409 key_limit_reached). created_by is the signing key's prefix. Not idempotent: a retry after a lost answer creates a second key, which you can list and revoke.

### Example Usage: key

<!-- UsageSnippet language="csharp" operationID="createKey" method="post" path="/v1/workspaces/{workspace_id}/keys" example="key" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Keys.CreateAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new KeyRequest() {
        Environment = KeyEnvironment.Live,
        Scopes = new List<KeyRequestScope>() {
            KeyRequestScope.Delete,
        },
    }
);

// handle response
```
### Example Usage: live

<!-- UsageSnippet language="csharp" operationID="createKey" method="post" path="/v1/workspaces/{workspace_id}/keys" example="live" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Keys.CreateAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new KeyRequest() {
        Environment = KeyEnvironment.Live,
        Scopes = new List<KeyRequestScope>() {
            KeyRequestScope.Assess,
        },
    }
);

// handle response
```
### Example Usage: test

<!-- UsageSnippet language="csharp" operationID="createKey" method="post" path="/v1/workspaces/{workspace_id}/keys" example="test" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Keys.CreateAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new KeyRequest() {
        Environment = KeyEnvironment.Test,
        Scopes = new List<KeyRequestScope>() {
            KeyRequestScope.Assess,
            KeyRequestScope.History,
            KeyRequestScope.Webhooks,
        },
    }
);

// handle response
```

### Parameters

| Parameter                                                                                                                          | Type                                                                                                                               | Required                                                                                                                           | Description                                                                                                                        | Example                                                                                                                            |
| ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                      | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404. | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                     |
| `Body`                                                                                                                             | [KeyRequest](../../Models/Shared/KeyRequest.md)                                                                                    | :heavy_check_mark:                                                                                                                 | N/A                                                                                                                                |                                                                                                                                    |

### Response

**[CreateKeyResponse](../../Models/Operations/CreateKeyResponse.md)**

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

Lists the keys of the signing key's environment, newest first, at most 200, revoked and expired keys included, without secrets: each key's id, prefix, scopes, status, and who created and revoked it. Requires the management scope. Read-only and safe to retry.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listKeys" method="get" path="/v1/workspaces/{workspace_id}/keys" example="list" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Keys.ListAsync(workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```

### Parameters

| Parameter                                                                                                                          | Type                                                                                                                               | Required                                                                                                                           | Description                                                                                                                        | Example                                                                                                                            |
| ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                      | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404. | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                     |

### Response

**[ListKeysResponse](../../Models/Operations/ListKeysResponse.md)**

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

## Revoke

Revokes a key of the signing key's environment at once: its next request answers 401. Requires the management scope. A key of the other environment or of another workspace answers 404 not_found. A key cannot revoke itself (409 self_revocation): rotate it with keys.rotate, or revoke it with another management key. Revoking a revoked key changes nothing and answers the key as it was revoked, so a retry is safe.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="revokeKey" method="delete" path="/v1/workspaces/{workspace_id}/keys/{key_id}" example="key" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Keys.RevokeAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    keyId: "01JA2ZE4K5M6N7P8Q9R0S1T2V3"
);

// handle response
```

### Parameters

| Parameter                                                                                                                          | Type                                                                                                                               | Required                                                                                                                           | Description                                                                                                                        | Example                                                                                                                            |
| ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                      | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404. | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                     |
| `KeyId`                                                                                                                            | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | The key's id. A key of another workspace answers 404, as does a key of the other environment to an API key.                        | 01JA2ZE4K5M6N7P8Q9R0S1T2V3                                                                                                         |

### Response

**[RevokeKeyResponse](../../Models/Operations/RevokeKeyResponse.md)**

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