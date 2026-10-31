# Lists

## Overview

List entries, previews and durable changes.

### Available Operations

* [List](#list) - List an environment's list entries.
* [Create](#create) - Add an entry to an environment's list.
* [Delete](#delete) - Remove an entry from an environment's list.
* [Preview](#preview) - Preview a list override change without side effects.
* [Change](#change) - Apply an idempotent atomic bulk list change.
* [GetChange](#getchange) - Read a bulk list change and propagation progress.

## List

Page through this environment allow and block entries, with canonical values, reasons and optional expiry. Requires settings or management. Follow the returned cursor until exhausted; a page is not the complete list. The cursor is bound to the environment and current filters.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listListEntries" method="get" path="/v1/workspaces/{workspace_id}/lists/{environment}" example="default" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

ListListEntriesResponse? res = await sdk.Lists.ListAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Test
);

while(res != null)
{
    // handle items

    res = await res.Next!();
}
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `Limit`                                                                                                                                                                | *long*                                                                                                                                                                 | :heavy_minus_sign:                                                                                                                                                     | N/A                                                                                                                                                                    |                                                                                                                                                                        |
| `Cursor`                                                                                                                                                               | *string*                                                                                                                                                               | :heavy_minus_sign:                                                                                                                                                     | N/A                                                                                                                                                                    |                                                                                                                                                                        |

### Response

**[ListListEntriesResponse](../../Models/Operations/ListListEntriesResponse.md)**

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

Add a canonical email or email-domain override with a reason and optional expiry. Requires settings plus policy_publish, or legacy management. Block wins over allow when both match. A small single-entry change retains the documented synchronous match budget; use lists.change for larger changes, whose account propagation is durable and resumable.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="createListEntry" method="post" path="/v1/workspaces/{workspace_id}/lists/{environment}" example="default" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Lists.CreateAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Live,
    body: new ListEntryRequest() {
        Kind = ListEntryKind.EmailDomain,
        Value = "throwaway.example",
        Action = ListEntryAction.Block,
        Note = "Signup burst from one domain",
    }
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `Body`                                                                                                                                                                 | [ListEntryRequest](../../Models/Shared/ListEntryRequest.md)                                                                                                            | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    |                                                                                                                                                                        |

### Response

**[CreateListEntryResponse](../../Models/Operations/CreateListEntryResponse.md)**

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

Remove an override with settings plus policy_publish, or legacy management. The removal takes effect for new decisions and schedules resumable propagation across matching retained accounts. An unknown or already removed entry answers404. Account risk events announce changed decisions as propagation runs.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="deleteListEntry" method="delete" path="/v1/workspaces/{workspace_id}/lists/{environment}/{list_entry_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Lists.DeleteAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Live,
    listEntryId: "lst_01JA2ZE4K5M6N7P8Q9R0S1T2V3"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `ListEntryId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | The entry's id. An entry of another environment or workspace answers 404.                                                                                              | lst_01JA2ZE4K5M6N7P8Q9R0S1T2V3                                                                                                                                         |

### Response

**[DeleteListEntryResponse](../../Models/Operations/DeleteListEntryResponse.md)**

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

## Preview

Preview canonical list matches and current override effects with settings or management, plus legacy history, or investigate with evidence or detailed. The result states its bounded coverage and changes no list entries.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="previewList" method="post" path="/v1/workspaces/{workspace_id}/lists/{environment}/preview" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Lists.PreviewAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Test,
    body: new ListPreviewRequest() {
        Add = new ListEntryRequest() {
            Kind = ListEntryKind.EmailDomain,
            Value = "example.com",
            Action = ListEntryAction.Block,
        },
    }
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `Body`                                                                                                                                                                 | [ListPreviewRequest](../../Models/Shared/ListPreviewRequest.md)                                                                                                        | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | {<br/>"add": {<br/>"kind": "email_domain",<br/>"value": "example.com",<br/>"action": "block"<br/>}<br/>}                                                               |

### Response

**[PreviewListResponse](../../Models/Operations/PreviewListResponse.md)**

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

## Change

Atomically add and remove a bounded batch of entries with an idempotency key. Requires settings plus policy_publish, or legacy management. The receipt exposes durable account propagation beyond the synchronous single-entry budget; poll lists.getChange until completion.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="changeLists" method="post" path="/v1/workspaces/{workspace_id}/lists/{environment}/bulk" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Lists.ChangeAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Test,
    body: new ListBulkRequest() {
        Add = new List<ListEntryRequest>() {
            new ListEntryRequest() {
                Kind = ListEntryKind.Email,
                Value = "throwaway.example",
                Action = ListEntryAction.Block,
                Note = "Signup burst from one domain",
                ExpiresAt = "2026-01-01T00:00:00Z",
            },
        },
        Remove = new List<string>() {
            "lst_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
        },
    },
    idempotencyKey: "account-acct_7f3a9c"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                                                                                | Type                                                                                                                                                                                                                                                                     | Required                                                                                                                                                                                                                                                                 | Description                                                                                                                                                                                                                                                              | Example                                                                                                                                                                                                                                                                  |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `WorkspaceId`                                                                                                                                                                                                                                                            | *string*                                                                                                                                                                                                                                                                 | :heavy_check_mark:                                                                                                                                                                                                                                                       | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                                                                                                                       | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                                                                                                                           |
| `Environment`                                                                                                                                                                                                                                                            | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                                                                                                                  | :heavy_check_mark:                                                                                                                                                                                                                                                       | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404.                                                                                                   |                                                                                                                                                                                                                                                                          |
| `Body`                                                                                                                                                                                                                                                                   | [ListBulkRequest](../../Models/Shared/ListBulkRequest.md)                                                                                                                                                                                                                | :heavy_check_mark:                                                                                                                                                                                                                                                       | N/A                                                                                                                                                                                                                                                                      |                                                                                                                                                                                                                                                                          |
| `IdempotencyKey`                                                                                                                                                                                                                                                         | *string*                                                                                                                                                                                                                                                                 | :heavy_minus_sign:                                                                                                                                                                                                                                                       | Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the workspace principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call. | account-acct_7f3a9c                                                                                                                                                                                                                                                      |

### Response

**[ChangeListsResponse](../../Models/Operations/ChangeListsResponse.md)**

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

## GetChange

Read retained propagation progress for a list change with settings or management.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getListChange" method="get" path="/v1/workspaces/{workspace_id}/list_changes/{environment}/{change_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Lists.GetChangeAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Live,
    changeId: "<id>"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `ChangeId`                                                                                                                                                             | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    |                                                                                                                                                                        |

### Response

**[GetListChangeResponse](../../Models/Operations/GetListChangeResponse.md)**

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