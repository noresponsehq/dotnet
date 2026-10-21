# Keys

## Overview

Your own API keys.

### Available Operations

* [Rotate](#rotate) - Rotate the API key that signs the request.

## Rotate

Mints a new key with the same scopes as the key that signs the request and returns it once. The presented key keeps working for overlap_seconds (default 24 hours, from 60 seconds to 7 days), then answers 401. A key accepts one rotation: rotating again with a key already in its overlap answers 409 key_rotation_in_progress, so rotate with the new key. Rotation is not idempotent: a retry after a lost answer mints another key.

### Example Usage: default

<!-- UsageSnippet language="csharp" operationID="rotateApiKey" method="post" path="/v1/keys/rotate" example="default" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseClient(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

KeyRotationRequest req = new KeyRotationRequest() {};

var res = await sdk.Keys.RotateAsync(req);

// handle response
```
### Example Usage: one_hour

<!-- UsageSnippet language="csharp" operationID="rotateApiKey" method="post" path="/v1/keys/rotate" example="one_hour" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseClient(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

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

var sdk = new NoResponseClient(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

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