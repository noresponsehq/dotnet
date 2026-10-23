# KeyUsage

## Overview

### Available Operations

* [Record](#record) - Report requests made with API keys your signup gate issued.

## Record

Records requests made with API keys your signup gate issued to your accounts, one event per request: the key's SHA-256 (the issued_credential.sha256 the account's assessment carried), the client IP, the time and optionally the user agent, model and token counts. Most callers use the SDK's usage buffer, which batches events and sends them in the background. Sampling is allowed: sample_rate weights an event's request and token counts. Requires the assess scope. Idempotency-Key is optional in the SDK and handled as on POST /v1/assessments: a retry with the same key and body answers the same record with Idempotent-Replayed: true and counts nothing again; a different body answers 409. A batch holds 1 to 500 events and at most 256 KiB. No raw event is kept: usage is aggregated per key, and IPs, user agents and models are kept only as keyed digests, with the IP's country, for 24 hours. When one key is used from 20 or more IP addresses, or 3 or more countries, within 24 hours, every account whose assessment carried that key gets the shared_api_key reason for 30 days.

### Example Usage: batch

<!-- UsageSnippet language="csharp" operationID="recordKeyUsage" method="post" path="/v1/key_usage/batches" example="batch" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.KeyUsage.RecordAsync(
    body: new KeyUsageBatchRequest() {
        Events = new List<KeyUsageEvent>() {
            new KeyUsageEvent() {
                KeySha256 = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08",
                Ip = "203.0.113.24",
                OccurredAt = "2026-10-23T09:14:07Z",
                UserAgent = "python-requests/2.32.3",
                Model = "gpt-4o-mini",
                InputTokens = 812,
                OutputTokens = 164,
            },
            new KeyUsageEvent() {
                KeySha256 = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08",
                Ip = "2001:db8:4a::17",
                OccurredAt = "2026-10-23T09:14:09.250Z",
                SampleRate = 0.25D,
            },
        },
    },
    idempotencyKey: "signup-acct_7f3a9c"
);

// handle response
```
### Example Usage: recorded

<!-- UsageSnippet language="csharp" operationID="recordKeyUsage" method="post" path="/v1/key_usage/batches" example="recorded" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.KeyUsage.RecordAsync(
    body: new KeyUsageBatchRequest() {
        Events = new List<KeyUsageEvent>() {
            new KeyUsageEvent() {
                KeySha256 = "2bdbf5c72f2a36df0ef40fb92203612274d4546f56dba946070a6a79c2401a1b",
                Ip = "203.0.113.24",
                OccurredAt = "2026-10-23T09:14:07.000Z",
                UserAgent = "python-requests/2.32.3",
                Model = "gpt-4o-mini",
            },
        },
    },
    idempotencyKey: "signup-acct_7f3a9c"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                                                                             | Type                                                                                                                                                                                                                                                                  | Required                                                                                                                                                                                                                                                              | Description                                                                                                                                                                                                                                                           | Example                                                                                                                                                                                                                                                               |
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `Body`                                                                                                                                                                                                                                                                | [KeyUsageBatchRequest](../../Models/Shared/KeyUsageBatchRequest.md)                                                                                                                                                                                                   | :heavy_check_mark:                                                                                                                                                                                                                                                    | N/A                                                                                                                                                                                                                                                                   |                                                                                                                                                                                                                                                                       |
| `IdempotencyKey`                                                                                                                                                                                                                                                      | *string*                                                                                                                                                                                                                                                              | :heavy_minus_sign:                                                                                                                                                                                                                                                    | Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the tenant principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call. | signup-acct_7f3a9c                                                                                                                                                                                                                                                    |

### Response

**[RecordKeyUsageResponse](../../Models/Operations/RecordKeyUsageResponse.md)**

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