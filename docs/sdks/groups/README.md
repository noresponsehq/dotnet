# Groups

## Overview

Groups, lists of people, screenings and reviewer decisions.

### Available Operations

* [List](#list) - List an environment's groups.
* [Create](#create) - Create a group to screen.
* [Get](#get) - Read a group.
* [Update](#update) - Change a group.
* [Delete](#delete) - Erase a group.
* [ListPeople](#listpeople) - List a group's people.
* [AddPeople](#addpeople) - Add or update a batch of people.
* [GetPerson](#getperson) - Read a person's report.
* [ReviewPerson](#reviewperson) - Record a decision on a person.
* [JudgeRecord](#judgerecord) - Join one of a person's records to them, or reject it.
* [ListScreenings](#listscreenings) - List a group's screenings.
* [StartScreening](#startscreening) - Start a screening.
* [GetScreening](#getscreening) - Read a screening.
* [CancelScreening](#cancelscreening) - Cancel a screening.

## List

List this environment's groups, newest first. Pass next_before back as before for the next page. Requires the groups scope.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listGroups" method="get" path="/v1/workspaces/{workspace_id}/groups/{environment}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.ListAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Test,
    limit: 20
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `Limit`                                                                                                                                                                | *long*                                                                                                                                                                 | :heavy_minus_sign:                                                                                                                                                     | At most this many items.                                                                                                                                               |                                                                                                                                                                        |
| `Before`                                                                                                                                                               | *string*                                                                                                                                                               | :heavy_minus_sign:                                                                                                                                                     | Read groups older than this one.                                                                                                                                       |                                                                                                                                                                        |

### Response

**[ListGroupsResponse](../../Models/Operations/ListGroupsResponse.md)**

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

Create a group to screen: its name, start, venue, host, the principals it protects and the sensitive topics posts are read against. An environment holds at most 1000 groups.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="createGroup" method="post" path="/v1/workspaces/{workspace_id}/groups/{environment}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.CreateAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Live,
    body: new GroupRequest() {
        Name = "Annual shareholder meeting",
        StartsAt = "2026-01-01T00:00:00Z",
        EndsAt = "2026-01-02T00:00:00Z",
        Venue = "Example Convention Centre",
        Host = "Example Holdings",
        Principals = new List<Principal>() {
            new Principal() {
                Name = "Alex Principal",
                Aliases = new List<string>() {
                    "Alex P",
                },
            },
        },
        Topics = new List<string>() {
            "executive pay",
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
| `Body`                                                                                                                                                                 | [GroupRequest](../../Models/Shared/GroupRequest.md)                                                                                                                    | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    |                                                                                                                                                                        |

### Response

**[CreateGroupResponse](../../Models/Operations/CreateGroupResponse.md)**

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

Read a group and how many people it holds.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getGroup" method="get" path="/v1/workspaces/{workspace_id}/groups/{environment}/{group_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.GetAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Live,
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `GroupId`                                                                                                                                                              | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |

### Response

**[GetGroupResponse](../../Models/Operations/GetGroupResponse.md)**

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

## Update

Change the fields you name. Send expected_version to apply the change only to that version.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="updateGroup" method="patch" path="/v1/workspaces/{workspace_id}/groups/{environment}/{group_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.UpdateAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Test,
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new GroupUpdate() {
        Name = "Annual shareholder meeting",
        StartsAt = "2026-01-01T00:00:00Z",
        EndsAt = "2026-01-02T00:00:00Z",
        Venue = "Example Convention Centre",
        Host = "Example Holdings",
        Principals = new List<Principal>() {
            new Principal() {
                Name = "Alex Principal",
                Aliases = new List<string>() {
                    "Alex P",
                },
            },
        },
        Topics = new List<string>() {
            "executive pay",
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
| `GroupId`                                                                                                                                                              | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Body`                                                                                                                                                                 | [GroupUpdate](../../Models/Shared/GroupUpdate.md)                                                                                                                      | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    |                                                                                                                                                                        |

### Response

**[UpdateGroupResponse](../../Models/Operations/UpdateGroupResponse.md)**

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

Erase a group with its people, their decisions and its screenings, and stop any screening still running.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="deleteGroup" method="delete" path="/v1/workspaces/{workspace_id}/groups/{environment}/{group_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.DeleteAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Test,
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `GroupId`                                                                                                                                                              | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |

### Response

**[DeleteGroupResponse](../../Models/Operations/DeleteGroupResponse.md)**

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

## ListPeople

Read a group's roster in upload order, with each person's match state, highest finding band and decision. Filter by band or decision; pass next_after back as after.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listPeople" method="get" path="/v1/workspaces/{workspace_id}/groups/{environment}/{group_id}/people" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

ListPeopleRequest req = new ListPeopleRequest() {
    WorkspaceId = "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    Environment = KeyEnvironment.Test,
    GroupId = "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
};

var res = await sdk.Groups.ListPeopleAsync(req);

// handle response
```

### Parameters

| Parameter                                                         | Type                                                              | Required                                                          | Description                                                       |
| ----------------------------------------------------------------- | ----------------------------------------------------------------- | ----------------------------------------------------------------- | ----------------------------------------------------------------- |
| `request`                                                         | [ListPeopleRequest](../../Models/Operations/ListPeopleRequest.md) | :heavy_check_mark:                                                | The request object to use for the request.                        |

### Response

**[ListPeopleResponse](../../Models/Operations/ListPeopleResponse.md)**

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

## AddPeople

Add or update 1 to 500 people with an Idempotency-Key. A row with the same external_id, else email, else phone as an existing person updates it. Identities are stored encrypted.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="addPeople" method="post" path="/v1/workspaces/{workspace_id}/groups/{environment}/{group_id}/people" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.AddPeopleAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Live,
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new PersonBatchRequest() {
        People = new List<PersonIdentity>() {},
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
| `GroupId`                                                                                                                                                                                                                                                                | *string*                                                                                                                                                                                                                                                                 | :heavy_check_mark:                                                                                                                                                                                                                                                       | N/A                                                                                                                                                                                                                                                                      | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                                                                                                                           |
| `Body`                                                                                                                                                                                                                                                                   | [PersonBatchRequest](../../Models/Shared/PersonBatchRequest.md)                                                                                                                                                                                                          | :heavy_check_mark:                                                                                                                                                                                                                                                       | N/A                                                                                                                                                                                                                                                                      |                                                                                                                                                                                                                                                                          |
| `IdempotencyKey`                                                                                                                                                                                                                                                         | *string*                                                                                                                                                                                                                                                                 | :heavy_minus_sign:                                                                                                                                                                                                                                                       | Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the workspace principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call. | account-acct_7f3a9c                                                                                                                                                                                                                                                      |

### Response

**[AddPeopleResponse](../../Models/Operations/AddPeopleResponse.md)**

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

## GetPerson

Read a person's report: matched profiles, findings with their evidence, every recorded decision, and the other groups where the same email or phone appears.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getPerson" method="get" path="/v1/workspaces/{workspace_id}/groups/{environment}/{group_id}/people/{person_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.GetPersonAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Test,
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    personId: "per_01JA2Z8K6Q4V7W9XBCDEFGHJKM"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `GroupId`                                                                                                                                                              | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `PersonId`                                                                                                                                                             | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | per_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |

### Response

**[GetPersonResponse](../../Models/Operations/GetPersonResponse.md)**

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

## ReviewPerson

Record a cleared, watch or escalated decision with a note. The latest decision is the person's; the next screening refreshes everyone on watch or escalated.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="reviewPerson" method="post" path="/v1/workspaces/{workspace_id}/groups/{environment}/{group_id}/people/{person_id}/review" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.ReviewPersonAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Test,
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    personId: "per_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new PersonReviewRequest() {
        Decision = PersonDecision.Escalated,
        Note = "Known to the venue team.",
    }
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `GroupId`                                                                                                                                                              | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `PersonId`                                                                                                                                                             | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | per_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Body`                                                                                                                                                                 | [PersonReviewRequest](../../Models/Shared/PersonReviewRequest.md)                                                                                                      | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    |                                                                                                                                                                        |

### Response

**[ReviewPersonResponse](../../Models/Operations/ReviewPersonResponse.md)**

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

## JudgeRecord

Join one of a person's lookup records to them whatever its likelihood, or reject it so it stays a lead. The verdict sticks while lookups find the same record.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="judgeRecord" method="post" path="/v1/workspaces/{workspace_id}/groups/{environment}/{group_id}/people/{person_id}/records" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.JudgeRecordAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Test,
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    personId: "per_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new RecordVerdictRequest() {
        RecordId = "rec_3f9a1c22d4e5b6a73f9a1c22d4e5b6a7",
        Verdict = Verdict.Rejected,
    }
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `GroupId`                                                                                                                                                              | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `PersonId`                                                                                                                                                             | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | per_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Body`                                                                                                                                                                 | [RecordVerdictRequest](../../Models/Shared/RecordVerdictRequest.md)                                                                                                    | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    |                                                                                                                                                                        |

### Response

**[JudgeRecordResponse](../../Models/Operations/JudgeRecordResponse.md)**

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

## ListScreenings

List a group's screenings, newest first.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listScreenings" method="get" path="/v1/workspaces/{workspace_id}/groups/{environment}/{group_id}/screenings" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.ListScreeningsAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Live,
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    limit: 20
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `GroupId`                                                                                                                                                              | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Limit`                                                                                                                                                                | *long*                                                                                                                                                                 | :heavy_minus_sign:                                                                                                                                                     | At most this many items.                                                                                                                                               |                                                                                                                                                                        |
| `Before`                                                                                                                                                               | *string*                                                                                                                                                               | :heavy_minus_sign:                                                                                                                                                     | Read screenings older than this one.                                                                                                                                   |                                                                                                                                                                        |

### Response

**[ListScreeningsResponse](../../Models/Operations/ListScreeningsResponse.md)**

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

## StartScreening

Start a screening of new people and everyone on watch or escalated, with an Idempotency-Key. It finishes completed, partial or failed minutes to hours later; poll it. The answer carries estimated_spend; a screening whose estimate exceeds what the daily screening cap has left answers 409 screening_cap_exceeded. The test environment screens with the synthetic connector, which makes up deterministic profiles, posts and findings.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="startScreening" method="post" path="/v1/workspaces/{workspace_id}/groups/{environment}/{group_id}/screenings" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.StartScreeningAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Live,
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new ScreeningRequest() {
        Depth = ScreeningDepth.Standard,
        Platforms = new List<HandlePlatform>() {},
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
| `GroupId`                                                                                                                                                                                                                                                                | *string*                                                                                                                                                                                                                                                                 | :heavy_check_mark:                                                                                                                                                                                                                                                       | N/A                                                                                                                                                                                                                                                                      | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                                                                                                                           |
| `Body`                                                                                                                                                                                                                                                                   | [ScreeningRequest](../../Models/Shared/ScreeningRequest.md)                                                                                                                                                                                                              | :heavy_check_mark:                                                                                                                                                                                                                                                       | N/A                                                                                                                                                                                                                                                                      |                                                                                                                                                                                                                                                                          |
| `IdempotencyKey`                                                                                                                                                                                                                                                         | *string*                                                                                                                                                                                                                                                                 | :heavy_minus_sign:                                                                                                                                                                                                                                                       | Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the workspace principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call. | account-acct_7f3a9c                                                                                                                                                                                                                                                      |

### Response

**[StartScreeningResponse](../../Models/Operations/StartScreeningResponse.md)**

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

## GetScreening

Read a screening's status and progress.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getScreening" method="get" path="/v1/workspaces/{workspace_id}/groups/{environment}/{group_id}/screenings/{screening_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.GetScreeningAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Live,
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    screeningId: "scn_01JA2Z8K6Q4V7W9XBCDEFGHJKM"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `GroupId`                                                                                                                                                              | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `ScreeningId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | scn_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |

### Response

**[GetScreeningResponse](../../Models/Operations/GetScreeningResponse.md)**

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

## CancelScreening

Stop a queued or running screening. People already screened keep their results.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="cancelScreening" method="post" path="/v1/workspaces/{workspace_id}/groups/{environment}/{group_id}/screenings/{screening_id}/cancel" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.CancelScreeningAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Live,
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    screeningId: "scn_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new ScreeningCancelRequest() {}
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `GroupId`                                                                                                                                                              | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `ScreeningId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    | scn_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Body`                                                                                                                                                                 | [ScreeningCancelRequest](../../Models/Shared/ScreeningCancelRequest.md)                                                                                                | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    |                                                                                                                                                                        |

### Response

**[CancelScreeningResponse](../../Models/Operations/CancelScreeningResponse.md)**

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