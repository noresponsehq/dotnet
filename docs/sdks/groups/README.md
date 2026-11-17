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
* [RemovePerson](#removeperson) - Remove a person from a group
* [ReviewPerson](#reviewperson) - Record a decision on a person.
* [JudgeRecord](#judgerecord) - Join one of a person's records to them, or reject it.
* [StartSearch](#startsearch) - Search again on a value of one of a person's records.
* [RunSearch](#runsearch) - Run a counted password or hash search.
* [ListScreenings](#listscreenings) - List a group's screenings.
* [StartScreening](#startscreening) - Start a screening.
* [GetScreening](#getscreening) - Read a screening.
* [CancelScreening](#cancelscreening) - Cancel a screening.
* [ExportPeople](#exportpeople) - Export the people in a group.

## List

Groups, newest first. The people scope grants this operation. Explicit evidence.read restrictions override default evidence access. The groups scope is also accepted for existing integrations. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listGroups" method="get" path="/v1/groups" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.ListAsync(limit: 20);

// handle response
```

### Parameters

| Parameter                        | Type                             | Required                         | Description                      |
| -------------------------------- | -------------------------------- | -------------------------------- | -------------------------------- |
| `Limit`                          | *long*                           | :heavy_minus_sign:               | At most this many items.         |
| `Before`                         | *string*                         | :heavy_minus_sign:               | Read groups older than this one. |

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

Creates a group with its name, start, venue, host, the principals it protects and the sensitive topics its screening reads posts against. An environment holds at most 1000 groups. Send an Idempotency-Key to retry safely: the same key and request return the same resource in its current state while it is retained, even after work completes. Reusing the key with different input answers 409 idempotency_key_reused. Use a new key for a deliberate new operation. The people_write scope grants this operation. Explicit evidence.read restrictions override default evidence access. The groups scope is also accepted for existing integrations. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="createGroup" method="post" path="/v1/groups" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.CreateAsync(
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
    },
    idempotencyKey: "request-01JA2Z8K6Q4V7W9XBCDEFGHJKM"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                                                                                                                                                                | Type                                                                                                                                                                                                                                                                                                                                                     | Required                                                                                                                                                                                                                                                                                                                                                 | Description                                                                                                                                                                                                                                                                                                                                              | Example                                                                                                                                                                                                                                                                                                                                                  |
| -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `Body`                                                                                                                                                                                                                                                                                                                                                   | [GroupRequest](../../Models/Shared/GroupRequest.md)                                                                                                                                                                                                                                                                                                      | :heavy_check_mark:                                                                                                                                                                                                                                                                                                                                       | N/A                                                                                                                                                                                                                                                                                                                                                      |                                                                                                                                                                                                                                                                                                                                                          |
| `IdempotencyKey`                                                                                                                                                                                                                                                                                                                                         | *string*                                                                                                                                                                                                                                                                                                                                                 | :heavy_minus_sign:                                                                                                                                                                                                                                                                                                                                       | Opaque request key, 16 to 128 characters. Repeating the key and request returns the same resource in its current state while it is retained. A different request with the same key answers 409. Use a new key for a deliberate new operation. Keys are scoped to the environment and operation; person searches also scope them to the group and person. | request-01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                                                                                                                                                                                                       |

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

The group and how many people it holds. The people scope grants this operation. Explicit evidence.read restrictions override default evidence access. The groups scope is also accepted for existing integrations. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getGroup" method="get" path="/v1/groups/{group_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.GetAsync(groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```

### Parameters

| Parameter                      | Type                           | Required                       | Description                    | Example                        |
| ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ |
| `GroupId`                      | *string*                       | :heavy_check_mark:             | N/A                            | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM |

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

Changes the fields the body names. With expected_version, the change applies only to that version (409 group_conflict otherwise). The people_write scope grants this operation. Explicit evidence.read restrictions override default evidence access. The groups scope is also accepted for existing integrations. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="updateGroup" method="patch" path="/v1/groups/{group_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.UpdateAsync(
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

| Parameter                                         | Type                                              | Required                                          | Description                                       | Example                                           |
| ------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- |
| `GroupId`                                         | *string*                                          | :heavy_check_mark:                                | N/A                                               | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                    |
| `Body`                                            | [GroupUpdate](../../Models/Shared/GroupUpdate.md) | :heavy_check_mark:                                | N/A                                               |                                                   |

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

Deletes the group, its memberships and its review context, and cancels unfinished group work. Workspace people, source evidence and completed investigations remain available. Retained investigations no longer name the deleted group. The delete scope is required; group read, write, investigation and review access do not grant deletion. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="deleteGroup" method="delete" path="/v1/groups/{group_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.DeleteAsync(groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```

### Parameters

| Parameter                      | Type                           | Required                       | Description                    | Example                        |
| ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ |
| `GroupId`                      | *string*                       | :heavy_check_mark:             | N/A                            | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM |

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

The roster in upload order, with each person's role, match state, highest finding band and decision. Filter by band (or none), decision (or undecided) and role (or unstated). The people scope grants this operation. Explicit evidence.read restrictions override default evidence access. The groups scope is also accepted for existing integrations. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listPeople" method="get" path="/v1/groups/{group_id}/people" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

ListPeopleRequest req = new ListPeopleRequest() {
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

Adds 1 to 500 workspace people to this group, at most 512 KiB. An explicit person_id or external_id reuses the named workspace person. A reference-only row keeps submitted details; an identity supplied with that reference replaces those details. A shared email or phone only suggests matches and does not merge people. A changed identity clears current group screening summaries; prior investigation history remains available. Identities are encrypted under the workspace key. Idempotency-Key is required: the same key and body return the original import result with Idempotent-Replayed: true while its people remain retained. An expired or deleted imported person answers 404; expired or deleted match suggestions are omitted. A different body answers 409 idempotency_key_reused. A group holds at most 20000 people. The people_write scope grants this operation; groups is also accepted. Suggested matches require people-read access. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="addPeople" method="post" path="/v1/groups/{group_id}/people" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.AddPeopleAsync(
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

Returns submitted details, this group's latest screening evidence and contextual decisions, and the other groups that explicitly include the same workspace person. Membership requires an explicit person or customer reference. The people scope grants this operation; groups is also accepted. Each successful read is durably audited as person.viewed. Full source records, posts and media are returned by default. Explicit evidence.read denial keeps submitted details, coverage, job status and decision summaries, hides collected evidence and free-text notes, and sets evidence_restricted to true. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getPerson" method="get" path="/v1/groups/{group_id}/people/{person_id}" example="records" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.GetPersonAsync(
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    personId: "per_01JA2Z8K6Q4V7W9XBCDEFGHJKM"
);

// handle response
```

### Parameters

| Parameter                      | Type                           | Required                       | Description                    | Example                        |
| ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ |
| `GroupId`                      | *string*                       | :heavy_check_mark:             | N/A                            | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM |
| `PersonId`                     | *string*                       | :heavy_check_mark:             | N/A                            | per_01JA2Z8K6Q4V7W9XBCDEFGHJKM |

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

## RemovePerson

Removes this membership and its group review history. Keeps the workspace person, source evidence and completed investigations. Needs delete access. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="removeGroupPerson" method="delete" path="/v1/groups/{group_id}/people/{person_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.RemovePersonAsync(
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    personId: "per_01JA2Z8K6Q4V7W9XBCDEFGHJKM"
);

// handle response
```

### Parameters

| Parameter                      | Type                           | Required                       | Description                    | Example                        |
| ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ |
| `GroupId`                      | *string*                       | :heavy_check_mark:             | N/A                            | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM |
| `PersonId`                     | *string*                       | :heavy_check_mark:             | N/A                            | per_01JA2Z8K6Q4V7W9XBCDEFGHJKM |

### Response

**[RemoveGroupPersonResponse](../../Models/Operations/RemoveGroupPersonResponse.md)**

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

Marks the person cleared, watch or escalated with a note. Decisions are kept in order; the latest is the person's. The next screening refreshes everyone on watch or escalated. The decisions scope grants this operation. Explicit evidence.read restrictions override default evidence access. The groups scope is also accepted for existing integrations. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="reviewPerson" method="post" path="/v1/groups/{group_id}/people/{person_id}/review" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.ReviewPersonAsync(
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

| Parameter                                                         | Type                                                              | Required                                                          | Description                                                       | Example                                                           |
| ----------------------------------------------------------------- | ----------------------------------------------------------------- | ----------------------------------------------------------------- | ----------------------------------------------------------------- | ----------------------------------------------------------------- |
| `GroupId`                                                         | *string*                                                          | :heavy_check_mark:                                                | N/A                                                               | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                    |
| `PersonId`                                                        | *string*                                                          | :heavy_check_mark:                                                | N/A                                                               | per_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                    |
| `Body`                                                            | [PersonReviewRequest](../../Models/Shared/PersonReviewRequest.md) | :heavy_check_mark:                                                | N/A                                                               |                                                                   |

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

Records a verdict on one of the records the person's latest lookups found: joined makes the record the person's whatever its likelihood, and rejected keeps it a lead. The verdict sticks for as long as a lookup finds the same record, and a later verdict on the same record replaces it. A record the person's latest lookup does not hold answers 404. Each verdict is audited as person.record_judged. The decisions scope grants this operation. Explicit evidence.read restrictions override default evidence access. The groups scope is also accepted for existing integrations. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="judgeRecord" method="post" path="/v1/groups/{group_id}/people/{person_id}/records" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.JudgeRecordAsync(
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

| Parameter                                                           | Type                                                                | Required                                                            | Description                                                         | Example                                                             |
| ------------------------------------------------------------------- | ------------------------------------------------------------------- | ------------------------------------------------------------------- | ------------------------------------------------------------------- | ------------------------------------------------------------------- |
| `GroupId`                                                           | *string*                                                            | :heavy_check_mark:                                                  | N/A                                                                 | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                      |
| `PersonId`                                                          | *string*                                                            | :heavy_check_mark:                                                  | N/A                                                                 | per_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                      |
| `Body`                                                              | [RecordVerdictRequest](../../Models/Shared/RecordVerdictRequest.md) | :heavy_check_mark:                                                  | N/A                                                                 |                                                                     |

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

## StartSearch

Starts a new search on the value of one field of one of the person's records: a lookup's, a profile's or one an earlier search found. Name the record and the field; the value itself is not sent. The field's kind must be email, phone, username, person_name, password, hash, ip or domain. A domain search finds the breach rows of every email address at the domain, which must be a registrable domain or a host under one; any other domain answers 400. After a workspace person's own investigation or a group's screening, while the environment's enrichment mode, or an investigation's override, is automatic, enrichment also starts searches by itself, with requested_by enrichment, on the distinctive values of the enrichment kinds (usernames by default; email addresses and phone numbers when named) of records more likely than min_likelihood (0.5 by default) to be the person's: each value at most once per person (once more if that search failed), at most 5 searches from one search's records and 20 per person, at most 6 searches deep, within max_spend and the deadline when set, and within the screening cap; at the screening cap it waits and tries again later. A record a search finds starts from the likelihood of the record it came from, divided by one plus the results the search found on that site, or anywhere for a name search, so a chain grows less likely with each search and stops. A group takes mode suggest while the workspace has not set a mode. In suggest mode enrichment starts none by itself, and the person report lists what it would start in suggested_searches. The search runs the breach search and the account and phone lookups that take the value's kind, under the environment's source controls, and finishes seconds later. A password or hash search counts first: it stops at counted with rows, how many breach rows hold the value, and runs only when a person runs it. Name a table, as a lookup remaining entry names it, to fetch the rows an earlier search left out, up to 100. What the search finds joins the person's lookup records as leads, each match naming the search; the person report lists the searches, each with the value and the record and field it came from, and its records are scored like the rest. Without an Idempotency-Key, starting the same open search returns it. Its estimate counts against the environment's screening_daily_cap, and one the cap cannot hold answers 409 screening_cap_exceeded. A record or field the person does not hold answers 404. Each search is audited as person.search_started. Send an Idempotency-Key to retry safely: the same key and request return the same resource in its current state while it is retained, even after work completes. Reusing the key with different input answers 409 idempotency_key_reused. Use a new key for a deliberate new operation. The investigations scope grants this operation. Explicit evidence.read restrictions override default evidence access. The groups scope is also accepted for existing integrations. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="startSearch" method="post" path="/v1/groups/{group_id}/people/{person_id}/searches" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.StartSearchAsync(
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    personId: "per_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new SearchRequest() {
        RecordId = "rec_3f9a1c22d4e5b6a73f9a1c22d4e5b6a7",
        Field = "username",
        Table = "FORUM_EXAMPLE_COM_2019",
    },
    idempotencyKey: "request-01JA2Z8K6Q4V7W9XBCDEFGHJKM"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                                                                                                                                                                | Type                                                                                                                                                                                                                                                                                                                                                     | Required                                                                                                                                                                                                                                                                                                                                                 | Description                                                                                                                                                                                                                                                                                                                                              | Example                                                                                                                                                                                                                                                                                                                                                  |
| -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `GroupId`                                                                                                                                                                                                                                                                                                                                                | *string*                                                                                                                                                                                                                                                                                                                                                 | :heavy_check_mark:                                                                                                                                                                                                                                                                                                                                       | N/A                                                                                                                                                                                                                                                                                                                                                      | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                                                                                                                                                                                                           |
| `PersonId`                                                                                                                                                                                                                                                                                                                                               | *string*                                                                                                                                                                                                                                                                                                                                                 | :heavy_check_mark:                                                                                                                                                                                                                                                                                                                                       | N/A                                                                                                                                                                                                                                                                                                                                                      | per_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                                                                                                                                                                                                           |
| `Body`                                                                                                                                                                                                                                                                                                                                                   | [SearchRequest](../../Models/Shared/SearchRequest.md)                                                                                                                                                                                                                                                                                                    | :heavy_check_mark:                                                                                                                                                                                                                                                                                                                                       | N/A                                                                                                                                                                                                                                                                                                                                                      |                                                                                                                                                                                                                                                                                                                                                          |
| `IdempotencyKey`                                                                                                                                                                                                                                                                                                                                         | *string*                                                                                                                                                                                                                                                                                                                                                 | :heavy_minus_sign:                                                                                                                                                                                                                                                                                                                                       | Opaque request key, 16 to 128 characters. Repeating the key and request returns the same resource in its current state while it is retained. A different request with the same key answers 409. Use a new key for a deliberate new operation. Keys are scoped to the environment and operation; person searches also scope them to the group and person. | request-01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                                                                                                                                                                                                       |

### Response

**[StartSearchResponse](../../Models/Operations/StartSearchResponse.md)**

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

## RunSearch

Runs a password or hash search once a person has seen its count. A search still counting answers 409 group_conflict; one already queued, running or finished answers as it is. The body is an empty object. Its estimate counts against the environment's screening_daily_cap. Each run is audited as person.search_run. The investigations scope grants this operation. Explicit evidence.read restrictions override default evidence access. The groups scope is also accepted for existing integrations. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="runSearch" method="post" path="/v1/groups/{group_id}/people/{person_id}/searches/{search_id}/run" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.RunSearchAsync(
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    personId: "per_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    searchId: "srh_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new ScreeningCancelRequest() {}
);

// handle response
```

### Parameters

| Parameter                                                               | Type                                                                    | Required                                                                | Description                                                             | Example                                                                 |
| ----------------------------------------------------------------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------- |
| `GroupId`                                                               | *string*                                                                | :heavy_check_mark:                                                      | N/A                                                                     | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                          |
| `PersonId`                                                              | *string*                                                                | :heavy_check_mark:                                                      | N/A                                                                     | per_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                          |
| `SearchId`                                                              | *string*                                                                | :heavy_check_mark:                                                      | N/A                                                                     | srh_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                          |
| `Body`                                                                  | [ScreeningCancelRequest](../../Models/Shared/ScreeningCancelRequest.md) | :heavy_check_mark:                                                      | N/A                                                                     |                                                                         |

### Response

**[RunSearchResponse](../../Models/Operations/RunSearchResponse.md)**

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

Screenings, newest first. The people scope grants this operation. Explicit evidence.read restrictions override default evidence access. The groups scope is also accepted for existing integrations. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listScreenings" method="get" path="/v1/groups/{group_id}/screenings" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.ListScreeningsAsync(
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    limit: 20
);

// handle response
```

### Parameters

| Parameter                            | Type                                 | Required                             | Description                          | Example                              |
| ------------------------------------ | ------------------------------------ | ------------------------------------ | ------------------------------------ | ------------------------------------ |
| `GroupId`                            | *string*                             | :heavy_check_mark:                   | N/A                                  | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM       |
| `Limit`                              | *long*                               | :heavy_minus_sign:                   | At most this many items.             |                                      |
| `Before`                             | *string*                             | :heavy_minus_sign:                   | Read screenings older than this one. |                                      |

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

Queues a screening of every person no screening has reached and everyone on watch or escalated. One screening of a group runs at a time; it finishes completed, partial (some people not covered, counted in people_failed) or failed, and fails with error_code expired after 24 hours. Idempotency-Key is required and binds the body for the screening's life. The 202 body carries estimated_spend, what the selected people would cost at the depth and platforms. A screening whose estimate exceeds the environment's screening_daily_cap, less the day's recorded spend and what open screenings may still spend, answers 409 screening_cap_exceeded and queues nothing. The test environment screens with a deterministic simulator that contacts no source. A platform that no connector serves in the environment answers 409 screening_unavailable. The investigations scope grants this operation. Explicit evidence.read restrictions override default evidence access. The groups scope is also accepted for existing integrations. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="startScreening" method="post" path="/v1/groups/{group_id}/screenings" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.StartScreeningAsync(
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new ScreeningRequest() {
        Depth = ScreeningDepth.Standard,
        Platforms = new List<string>() {
            "bluesky",
        },
    },
    idempotencyKey: "account-acct_7f3a9c"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                                                                                | Type                                                                                                                                                                                                                                                                     | Required                                                                                                                                                                                                                                                                 | Description                                                                                                                                                                                                                                                              | Example                                                                                                                                                                                                                                                                  |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
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

The screening's status and progress. The people scope grants this operation. Explicit evidence.read restrictions override default evidence access. The groups scope is also accepted for existing integrations. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getScreening" method="get" path="/v1/groups/{group_id}/screenings/{screening_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.GetScreeningAsync(
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    screeningId: "scn_01JA2Z8K6Q4V7W9XBCDEFGHJKM"
);

// handle response
```

### Parameters

| Parameter                      | Type                           | Required                       | Description                    | Example                        |
| ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ |
| `GroupId`                      | *string*                       | :heavy_check_mark:             | N/A                            | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM |
| `ScreeningId`                  | *string*                       | :heavy_check_mark:             | N/A                            | scn_01JA2Z8K6Q4V7W9XBCDEFGHJKM |

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

Stops a queued or running screening. People it already screened keep their results. Cancelling a finished screening answers it unchanged. The body is an empty object. The investigations scope grants this operation. Explicit evidence.read restrictions override default evidence access. The groups scope is also accepted for existing integrations. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="cancelScreening" method="post" path="/v1/groups/{group_id}/screenings/{screening_id}/cancel" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Groups.CancelScreeningAsync(
    groupId: "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    screeningId: "scn_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new ScreeningCancelRequest() {}
);

// handle response
```

### Parameters

| Parameter                                                               | Type                                                                    | Required                                                                | Description                                                             | Example                                                                 |
| ----------------------------------------------------------------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------- |
| `GroupId`                                                               | *string*                                                                | :heavy_check_mark:                                                      | N/A                                                                     | grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                          |
| `ScreeningId`                                                           | *string*                                                                | :heavy_check_mark:                                                      | N/A                                                                     | scn_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                          |
| `Body`                                                                  | [ScreeningCancelRequest](../../Models/Shared/ScreeningCancelRequest.md) | :heavy_check_mark:                                                      | N/A                                                                     |                                                                         |

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

## ExportPeople

Returns one page for export. Each returned person is durably audited as person.exported with the actor, group, person ID and request ID. Requires both people read access and exports. Explicit evidence restrictions also hide free-text decision notes. No raw evidence enters audit logs. Follow next_after until absent for a complete export. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="exportPeople" method="get" path="/v1/groups/{group_id}/export" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

ExportPeopleRequest req = new ExportPeopleRequest() {
    GroupId = "grp_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
};

var res = await sdk.Groups.ExportPeopleAsync(req);

// handle response
```

### Parameters

| Parameter                                                             | Type                                                                  | Required                                                              | Description                                                           |
| --------------------------------------------------------------------- | --------------------------------------------------------------------- | --------------------------------------------------------------------- | --------------------------------------------------------------------- |
| `request`                                                             | [ExportPeopleRequest](../../Models/Operations/ExportPeopleRequest.md) | :heavy_check_mark:                                                    | The request object to use for the request.                            |

### Response

**[ExportPeopleResponse](../../Models/Operations/ExportPeopleResponse.md)**

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