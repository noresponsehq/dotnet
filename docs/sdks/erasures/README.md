# Erasures

## Overview

Durable deletion receipts.

### Available Operations

* [Get](#get) - Read an erasure receipt

## Get

Read retained erasure progress and downstream acknowledgements. Completion does not certify deletion from backups.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getErasureReceipt" method="get" path="/v1/erasures/{erasure_id}" example="receipt" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Erasures.GetAsync(erasureId: "<id>");

// handle response
```

### Parameters

| Parameter          | Type               | Required           | Description        |
| ------------------ | ------------------ | ------------------ | ------------------ |
| `ErasureId`        | *string*           | :heavy_check_mark: | N/A                |

### Response

**[GetErasureReceiptResponse](../../Models/Operations/GetErasureReceiptResponse.md)**

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