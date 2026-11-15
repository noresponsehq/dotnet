# People

## Overview

Workspace people, submitted identity, source evidence and expiry.

### Available Operations

* [List](#list) - List workspace people.
* [Create](#create) - Create or reuse a workspace person.
* [Get](#get) - Read a workspace person.
* [Delete](#delete) - Delete a workspace person.

## List

Lists retained people in the key workspace and environment, including people in no group. Each returned person records a person.viewed audit event. Expired people are excluded before background deletion.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listWorkspacePeople" method="get" path="/v1/people" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.People.ListAsync(limit: 100);

// handle response
```

### Parameters

| Parameter                             | Type                                  | Required                              | Description                           |
| ------------------------------------- | ------------------------------------- | ------------------------------------- | ------------------------------------- |
| `Limit`                               | *long*                                | :heavy_minus_sign:                    | N/A                                   |
| `After`                               | *string*                              | :heavy_minus_sign:                    | Pass next_cursor from the prior page. |

### Response

**[ListWorkspacePeopleResponse](../../Models/Operations/ListWorkspacePeopleResponse.md)**

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

Creates a person without a group. An explicit person_id or external_id reuses the named workspace person. A reference-only request keeps submitted details; identity supplied with that reference replaces those details. A shared email or phone only suggests a match. Suggested matches are returned only with people-read access. The workspace retention setting determines expires_at. An existing person keeps their original expiry. An Idempotency-Key returns the same retained person on retries, without creating another. Changed input with the same key answers 409 idempotency_key_reused. A customer reference that names another person or several people answers 409 person_reference_conflict.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="createWorkspacePerson" method="post" path="/v1/people" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.People.CreateAsync(
    body: new WorkspacePersonRequest() {
        ExternalId = "case-person-1042",
        Name = "Jordan Example",
        Email = "jordan@example.com",
    },
    idempotencyKey: "request-01JA2Z8K6Q4V7W9XBCDEFGHJKM"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                                                                                                                                                                | Type                                                                                                                                                                                                                                                                                                                                                     | Required                                                                                                                                                                                                                                                                                                                                                 | Description                                                                                                                                                                                                                                                                                                                                              | Example                                                                                                                                                                                                                                                                                                                                                  |
| -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `Body`                                                                                                                                                                                                                                                                                                                                                   | [WorkspacePersonRequest](../../Models/Shared/WorkspacePersonRequest.md)                                                                                                                                                                                                                                                                                  | :heavy_check_mark:                                                                                                                                                                                                                                                                                                                                       | N/A                                                                                                                                                                                                                                                                                                                                                      | {<br/>"external_id": "case-person-1042",<br/>"name": "Jordan Example",<br/>"email": "jordan@example.com"<br/>}                                                                                                                                                                                                                                           |
| `IdempotencyKey`                                                                                                                                                                                                                                                                                                                                         | *string*                                                                                                                                                                                                                                                                                                                                                 | :heavy_minus_sign:                                                                                                                                                                                                                                                                                                                                       | Opaque request key, 16 to 128 characters. Repeating the key and request returns the same resource in its current state while it is retained. A different request with the same key answers 409. Use a new key for a deliberate new operation. Keys are scoped to the environment and operation; person searches also scope them to the group and person. | request-01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                                                                                                                                                                                                       |

### Response

**[CreateWorkspacePersonResponse](../../Models/Operations/CreateWorkspacePersonResponse.md)**

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

Returns submitted details, the current retention expiry and the most recent retained completed or partial investigation evidence, together with retained follow-up search evidence from all of this person's groups. Search evidence remains available after its group is deleted. Migrated results whose original per-person status is unknown may also supply evidence. Earlier results and pending jobs remain available through investigation history; each investigation retains its own collected result. Full source records, posts and media are returned by default; explicit evidence.read denial withholds collected evidence and free-text notes. Each read records person.viewed without raw evidence in the audit log. A missing or expired person answers 404.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkspacePerson" method="get" path="/v1/people/{person_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.People.GetAsync(personId: "per_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```

### Parameters

| Parameter                      | Type                           | Required                       | Description                    | Example                        |
| ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ |
| `PersonId`                     | *string*                       | :heavy_check_mark:             | N/A                            | per_01JA2Z8K6Q4V7W9XBCDEFGHJKM |

### Response

**[GetWorkspacePersonResponse](../../Models/Operations/GetWorkspacePersonResponse.md)**

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

Deletes a person and their retained evidence, investigation history and group memberships. Requires the separate delete permission. The audit retains only the person id and deletion metadata.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="deleteWorkspacePerson" method="delete" path="/v1/people/{person_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.People.DeleteAsync(personId: "per_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```

### Parameters

| Parameter                      | Type                           | Required                       | Description                    | Example                        |
| ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ |
| `PersonId`                     | *string*                       | :heavy_check_mark:             | N/A                            | per_01JA2Z8K6Q4V7W9XBCDEFGHJKM |

### Response

**[DeleteWorkspacePersonResponse](../../Models/Operations/DeleteWorkspacePersonResponse.md)**

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