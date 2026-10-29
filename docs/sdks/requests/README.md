# Requests

## Overview

Retained request diagnostics and operational summaries.

### Available Operations

* [List](#list) - Search request diagnostics
* [Aggregate](#aggregate) - Summarise retained request attempts

## List

Search authenticated request attempts by time, route, status or request correlation id. The interval is at most seven days. Each attempt has a unique id and may include the exact served decision. Records are bounded best effort and exclude request bodies, secrets and source payloads. A repeated correlation id does not overwrite another attempt.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listRequestDiagnostics" method="get" path="/v1/requests" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

ListRequestDiagnosticsRequest req = new ListRequestDiagnosticsRequest() {
    From = "2026-10-21T00:00:00Z",
    To = "2026-10-22T00:00:00Z",
};

ListRequestDiagnosticsResponse? res = await sdk.Requests.ListAsync(req);

while(res != null)
{
    // handle items

    res = await res.Next!();
}
```

### Parameters

| Parameter                                                                                 | Type                                                                                      | Required                                                                                  | Description                                                                               |
| ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- |
| `request`                                                                                 | [ListRequestDiagnosticsRequest](../../Models/Operations/ListRequestDiagnosticsRequest.md) | :heavy_check_mark:                                                                        | The request object to use for the request.                                                |

### Response

**[ListRequestDiagnosticsResponse](../../Models/Operations/ListRequestDiagnosticsResponse.md)**

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

## Aggregate

Count all retained matching request attempts and latency samples within seven days. Capture is best effort.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="aggregateRequests" method="post" path="/v1/requests/aggregate" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

RequestAggregateRequest req = new RequestAggregateRequest() {
    From = "2026-01-01T00:00:00Z",
    To = "2026-01-01T00:00:00Z",
    Route = "POST /v1/assessments",
    ApiKeyId = "01JA2Z8K6Q4V7W9XBCDEFGHJKM",
};

var res = await sdk.Requests.AggregateAsync(req);

// handle response
```

### Parameters

| Parameter                                                                 | Type                                                                      | Required                                                                  | Description                                                               |
| ------------------------------------------------------------------------- | ------------------------------------------------------------------------- | ------------------------------------------------------------------------- | ------------------------------------------------------------------------- |
| `request`                                                                 | [RequestAggregateRequest](../../Models/Shared/RequestAggregateRequest.md) | :heavy_check_mark:                                                        | The request object to use for the request.                                |

### Response

**[AggregateRequestsResponse](../../Models/Operations/AggregateRequestsResponse.md)**

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