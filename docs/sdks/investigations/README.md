# Investigations

## Overview

Person investigation jobs, retained results and contextual decisions.

### Available Operations

* [List](#list) - List investigation history.
* [Create](#create) - Start an investigation for a person.
* [Get](#get) - Read an investigation and its evidence.
* [Review](#review) - Record an investigation decision.
* [Cancel](#cancel) - Cancel an investigation

## List

List investigation history. The API key determines the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listInvestigations" method="get" path="/v1/investigations" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Investigations.ListAsync(limit: 100);

// handle response
```

### Parameters

| Parameter          | Type               | Required           | Description        |
| ------------------ | ------------------ | ------------------ | ------------------ |
| `PersonId`         | *string*           | :heavy_minus_sign: | N/A                |
| `Before`           | *string*           | :heavy_minus_sign: | N/A                |
| `Limit`            | *long*             | :heavy_minus_sign: | N/A                |

### Response

**[ListInvestigationsResponse](../../Models/Operations/ListInvestigationsResponse.md)**

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

Starts an investigation against a retained workspace person, independently of group membership. enrichment overrides, field by field, how the environment's enrichment setting runs after this investigation; the person's latest investigation decides the enrichment that runs on them. Idempotency-Key is required: the same key and input return the same retained job in its current state without repeating billable work. Different input with that key answers 409 idempotency_key_reused; a deliberate new investigation uses a new key. The response includes Location and Retry-After. The person's retention expiry limits both execution and retained history. The API key selects the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="createInvestigation" method="post" path="/v1/investigations" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Investigations.CreateAsync(
    body: new InvestigationRequest() {
        Depth = ScreeningDepth.Standard,
        Platforms = new List<string>() {
            "x",
        },
        PersonId = "per_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    },
    idempotencyKey: "investigation-case-1042-first"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                                                                                                                                                                                                                                                                       | Type                                                                                                                                                                                                                                                                                                                                                                                                                                                            | Required                                                                                                                                                                                                                                                                                                                                                                                                                                                        | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                     | Example                                                                                                                                                                                                                                                                                                                                                                                                                                                         |
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `Body`                                                                                                                                                                                                                                                                                                                                                                                                                                                          | [InvestigationRequest](../../Models/Shared/InvestigationRequest.md)                                                                                                                                                                                                                                                                                                                                                                                             | :heavy_check_mark:                                                                                                                                                                                                                                                                                                                                                                                                                                              | N/A                                                                                                                                                                                                                                                                                                                                                                                                                                                             |                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| `IdempotencyKey`                                                                                                                                                                                                                                                                                                                                                                                                                                                | *string*                                                                                                                                                                                                                                                                                                                                                                                                                                                        | :heavy_minus_sign:                                                                                                                                                                                                                                                                                                                                                                                                                                              | Opaque request key, 16 to 128 characters. Repeating the key and request returns the same resource in its current state while it is retained. A different request with the same key answers 409. Use a new key for a deliberate new operation. Keys are scoped to the environment and operation; person searches also scope them to the group and person. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call. | investigation-case-1042-first                                                                                                                                                                                                                                                                                                                                                                                                                                   |

### Response

**[CreateInvestigationResponse](../../Models/Operations/CreateInvestigationResponse.md)**

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

Returns this investigation's status, coverage, source records, posts, media and contextual review history. Full evidence is returned by default; explicit evidence.read denial keeps submitted person details, status, coverage and decision summaries and hides collected evidence and free-text notes. The read is durably audited as person.viewed. The API key selects the workspace and environment. Missing, foreign or expired results answer 404.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getInvestigation" method="get" path="/v1/investigations/{investigation_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Investigations.GetAsync(investigationId: "inv_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```

### Parameters

| Parameter                      | Type                           | Required                       | Description                    | Example                        |
| ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ |
| `InvestigationId`              | *string*                       | :heavy_check_mark:             | N/A                            | inv_01JA2Z8K6Q4V7W9XBCDEFGHJKM |

### Response

**[GetInvestigationResponse](../../Models/Operations/GetInvestigationResponse.md)**

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

## Review

Record an investigation decision. The API key determines the workspace and environment.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="reviewInvestigation" method="post" path="/v1/investigations/{investigation_id}/reviews" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Investigations.ReviewAsync(
    investigationId: "inv_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new PersonReviewRequest() {
        Decision = PersonDecision.Watch,
        Note = "Known to the venue team.",
    }
);

// handle response
```

### Parameters

| Parameter                                                         | Type                                                              | Required                                                          | Description                                                       | Example                                                           |
| ----------------------------------------------------------------- | ----------------------------------------------------------------- | ----------------------------------------------------------------- | ----------------------------------------------------------------- | ----------------------------------------------------------------- |
| `InvestigationId`                                                 | *string*                                                          | :heavy_check_mark:                                                | N/A                                                               | inv_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                    |
| `Body`                                                            | [PersonReviewRequest](../../Models/Shared/PersonReviewRequest.md) | :heavy_check_mark:                                                | N/A                                                               |                                                                   |

### Response

**[ReviewInvestigationResponse](../../Models/Operations/ReviewInvestigationResponse.md)**

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

Stops this investigation. A completed or already cancelled investigation keeps its current result. Other investigations for the person keep running.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="cancelInvestigation" method="post" path="/v1/investigations/{investigation_id}/cancel" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Investigations.CancelAsync(
    investigationId: "inv_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new ScreeningCancelRequest() {}
);

// handle response
```

### Parameters

| Parameter                                                               | Type                                                                    | Required                                                                | Description                                                             | Example                                                                 |
| ----------------------------------------------------------------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------- |
| `InvestigationId`                                                       | *string*                                                                | :heavy_check_mark:                                                      | N/A                                                                     | inv_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                          |
| `Body`                                                                  | [ScreeningCancelRequest](../../Models/Shared/ScreeningCancelRequest.md) | :heavy_check_mark:                                                      | N/A                                                                     |                                                                         |

### Response

**[CancelInvestigationResponse](../../Models/Operations/CancelInvestigationResponse.md)**

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