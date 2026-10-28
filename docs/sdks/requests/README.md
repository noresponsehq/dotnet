# Requests

## Overview

### Available Operations

* [List](#list) - Search request diagnostics

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

var res = await sdk.Requests.ListAsync(req);

// handle response
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