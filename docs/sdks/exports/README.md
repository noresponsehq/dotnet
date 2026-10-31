# Exports

## Overview

Bounded customer export jobs and encrypted artifacts.

### Available Operations

* [Create](#create) - Request an asynchronous customer export.
* [List](#list) - List exports for one collection.
* [Get](#get) - Read export state and progress.
* [Cancel](#cancel) - Cancel and remove an export artifact.
* [Retry](#retry) - Retry a failed export.
* [DownloadChunk](#downloadchunk) - Download one authorized export chunk.

## Create

Request an encrypted JSON Lines snapshot of retained account or audit records. Requires exports plus history or audit scope. An account export without legacy history needs investigate with evidence or detailed, because it holds submitted identifiers; detailed account data always needs evidence or detailed. Criteria are frozen and hashed. Each environment permits four active jobs, 64 MiB per artifact, and 256 KiB per chunk. Artifacts expire after 24 hours. Send an Idempotency-Key to safely repeat a request. Every operation checks current permissions and source retention.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="createCustomerExport" method="post" path="/v1/workspaces/{workspace_id}/exports" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Exports.CreateAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new CustomerExportRequest() {
        Kind = CustomerExportRequestKind.Account,
        Environment = NoResponse.Models.Shared.Environment.Test,
        AccountId = "acct_example",
        ResponseMode = ResponseMode.Standard,
    }
);

// handle response
```

### Parameters

| Parameter                                                                                                                          | Type                                                                                                                               | Required                                                                                                                           | Description                                                                                                                        | Example                                                                                                                            |
| ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                      | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404. | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                     |
| `Body`                                                                                                                             | [CustomerExportRequest](../../Models/Shared/CustomerExportRequest.md)                                                              | :heavy_check_mark:                                                                                                                 | N/A                                                                                                                                | {<br/>"kind": "account",<br/>"environment": "live",<br/>"account_id": "acct_7f3a9c",<br/>"response_mode": "standard"<br/>}         |
| `IdempotencyKey`                                                                                                                   | *string*                                                                                                                           | :heavy_minus_sign:                                                                                                                 | N/A                                                                                                                                |                                                                                                                                    |

### Response

**[CreateCustomerExportResponse](../../Models/Operations/CreateCustomerExportResponse.md)**

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

List authorised jobs for one kind and environment. Detailed jobs require current detailed-data permission and are filtered before pagination. Keep the kind and environment unchanged when following next_cursor.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listCustomerExports" method="get" path="/v1/workspaces/{workspace_id}/exports" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

ListCustomerExportsResponse? res = await sdk.Exports.ListAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    kind: CustomerExportRequestKind.Audit,
    environment: NoResponse.Models.Shared.Environment.Live,
    limit: 25
);

while(res != null)
{
    // handle items

    res = await res.Next!();
}
```

### Parameters

| Parameter                                                                                                                          | Type                                                                                                                               | Required                                                                                                                           | Description                                                                                                                        | Example                                                                                                                            |
| ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                      | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404. | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                     |
| `Kind`                                                                                                                             | [CustomerExportRequestKind](../../Models/Shared/CustomerExportRequestKind.md)                                                      | :heavy_check_mark:                                                                                                                 | N/A                                                                                                                                |                                                                                                                                    |
| `Environment`                                                                                                                      | [Models.Shared.Environment](../../Models/Shared/Environment.md)                                                                    | :heavy_check_mark:                                                                                                                 | N/A                                                                                                                                |                                                                                                                                    |
| `Limit`                                                                                                                            | *long*                                                                                                                             | :heavy_minus_sign:                                                                                                                 | N/A                                                                                                                                |                                                                                                                                    |
| `Cursor`                                                                                                                           | *string*                                                                                                                           | :heavy_minus_sign:                                                                                                                 | N/A                                                                                                                                |                                                                                                                                    |

### Response

**[ListCustomerExportsResponse](../../Models/Operations/ListCustomerExportsResponse.md)**

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

## Get

Read the status of an authorised export. Records and bytes describe committed progress in the current generation. Only completed, unexpired artifacts can be downloaded. Source erasure, retention changes and workspace removal can invalidate a job.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getCustomerExport" method="get" path="/v1/workspaces/{workspace_id}/exports/{export_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Exports.GetAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    exportId: "<id>"
);

// handle response
```

### Parameters

| Parameter                                                                                                                          | Type                                                                                                                               | Required                                                                                                                           | Description                                                                                                                        | Example                                                                                                                            |
| ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                      | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404. | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                     |
| `ExportId`                                                                                                                         | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | N/A                                                                                                                                |                                                                                                                                    |

### Response

**[GetCustomerExportResponse](../../Models/Operations/GetCustomerExportResponse.md)**

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

## Cancel

Cancel an export and remove its stored chunks. Repeating cancellation is safe. An already admitted download may finish; bytes already returned cannot be recalled. Send an empty JSON object.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="cancelCustomerExport" method="post" path="/v1/workspaces/{workspace_id}/exports/{export_id}/cancel" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Exports.CancelAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    exportId: "<id>",
    body: new CustomerExportActionRequest() {}
);

// handle response
```

### Parameters

| Parameter                                                                                                                          | Type                                                                                                                               | Required                                                                                                                           | Description                                                                                                                        | Example                                                                                                                            |
| ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                      | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404. | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                     |
| `ExportId`                                                                                                                         | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | N/A                                                                                                                                |                                                                                                                                    |
| `Body`                                                                                                                             | [CustomerExportActionRequest](../../Models/Shared/CustomerExportActionRequest.md)                                                  | :heavy_check_mark:                                                                                                                 | N/A                                                                                                                                |                                                                                                                                    |

### Response

**[CancelCustomerExportResponse](../../Models/Operations/CancelCustomerExportResponse.md)**

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

## Retry

Start a new generation of a failed export before its original expiry, up to ten generations. The request criteria stay fixed and the new generation takes a fresh source snapshot. Cancelled, erased and expired jobs cannot be retried. Send an empty JSON object.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="retryCustomerExport" method="post" path="/v1/workspaces/{workspace_id}/exports/{export_id}/retry" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Exports.RetryAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    exportId: "<id>",
    body: new CustomerExportActionRequest() {}
);

// handle response
```

### Parameters

| Parameter                                                                                                                          | Type                                                                                                                               | Required                                                                                                                           | Description                                                                                                                        | Example                                                                                                                            |
| ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                      | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404. | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                     |
| `ExportId`                                                                                                                         | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | N/A                                                                                                                                |                                                                                                                                    |
| `Body`                                                                                                                             | [CustomerExportActionRequest](../../Models/Shared/CustomerExportActionRequest.md)                                                  | :heavy_check_mark:                                                                                                                 | N/A                                                                                                                                |                                                                                                                                    |

### Response

**[RetryCustomerExportResponse](../../Models/Operations/RetryCustomerExportResponse.md)**

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

## DownloadChunk

Download one chunk of a completed, unexpired export after a fresh permission and privacy check. The response contains base64-encoded JSON Lines, a SHA-256, generation and next index. Decode and concatenate chunks in order from one generation. Audit storage failure withholds the bytes.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="downloadCustomerExportChunk" method="get" path="/v1/workspaces/{workspace_id}/exports/{export_id}/chunks/{chunk_index}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Exports.DownloadChunkAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    exportId: "<id>",
    chunkIndex: 768311
);

// handle response
```

### Parameters

| Parameter                                                                                                                          | Type                                                                                                                               | Required                                                                                                                           | Description                                                                                                                        | Example                                                                                                                            |
| ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                      | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404. | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                     |
| `ExportId`                                                                                                                         | *string*                                                                                                                           | :heavy_check_mark:                                                                                                                 | N/A                                                                                                                                |                                                                                                                                    |
| `ChunkIndex`                                                                                                                       | *long*                                                                                                                             | :heavy_check_mark:                                                                                                                 | N/A                                                                                                                                |                                                                                                                                    |

### Response

**[DownloadCustomerExportChunkResponse](../../Models/Operations/DownloadCustomerExportChunkResponse.md)**

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