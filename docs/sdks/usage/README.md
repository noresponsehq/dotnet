# Usage

## Overview

Recorded usage for your environment.

### Available Operations

* [Get](#get) - Read a workspace's usage.

## Get

Read daily usage units for the environments you can access. These counts are unpriced.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getUsage" method="get" path="/v1/workspaces/{workspace_id}/usage" example="usage" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Usage.GetAsync(workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```

### Parameters

| Parameter                                                                                                                          | Type                                                                                                                               | Required                                                                                                                           | Description                                                                                                                        | Example                                                                                                                            |
| ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                      | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404. | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                     |
| `From`                                                                                                                             | *string*                                                                                                                           | :heavy_minus_sign:                                                                                                                 | The first UTC day, as YYYY-MM-DD.                                                                                                  |                                                                                                                                    |
| `To`                                                                                                                               | *string*                                                                                                                           | :heavy_minus_sign:                                                                                                                 | The last UTC day, included, as YYYY-MM-DD; no later than today.                                                                    |                                                                                                                                    |

### Response

**[GetUsageResponse](../../Models/Operations/GetUsageResponse.md)**

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