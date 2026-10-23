# Assessments

## Overview

Signup and gateway assessments.

### Available Operations

* [Create](#create) - Assess identifiers for a signup or gateway decision.
* [Get](#get) - Poll the authoritative assessment of one assessment.
* [Delete](#delete) - Delete one assessment.
* [RecordOutcome](#recordoutcome) - Report what was later learned about an assessed account.
* [Query](#query) - Page through one account's unexpired assessments.

## Create

Assesses one signup, login or purchase (flow) and answers with the account's one current risk: account.risk.decision is the decision for this flow (allow, step_up, review or block, where block means your block threshold was reached), and detail holds this assessment's own facts. depth sets how long the answer waits for sources: quick waits only for the fast ones (500 ms), full for the whole budget (3000 ms at most); it defaults to full for signup and purchase and quick for login. 200 carries complete or partial, 202 carries pending with Location and Retry-After. While the account's first assessment is pending, account is absent: apply your own fallback for that, 429, 503 or a timeout. NoResponse blocks nothing itself; you decide. Requires the assess scope. Idempotency-Key is optional in the SDK and is bound to the body for 24 hours: the same key with the same body replays the latest assessment with Idempotent-Replayed: true and starts no new work; the same key with a different body answers 409 idempotency_key_reused. response_mode defaults to standard; detailed needs the detailed scope.

### Example Usage: complete

<!-- UsageSnippet language="csharp" operationID="createAssessment" method="post" path="/v1/assessments" example="complete" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.CreateAsync(
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Flow = Flow.Signup,
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
            Username = "new.user",
            Phone = "+61412345678",
            Name = "Alex Example",
            Ip = "203.0.113.45",
        },
        PolicyContext = new AssessmentRequestPolicyContext() {
            Region = "AU",
        },
        RequestId = "req-2026-10-14-0001",
        ClientContext = new AssessmentRequestClientContext() {
            Timezone = "Australia/Sydney",
            Locale = "en-AU",
            AppVersion = "4.12.0",
            UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0 Safari/537.36",
            DeviceId = "dev_5c1e8a",
            SessionId = "sess_9b2d41",
            ClientObservedAt = "2026-10-14T08:59:58.000Z",
        },
        AuthContext = new AuthContext() {
            Provider = "clerk",
            Issuer = "https://example-app.clerk.accounts.dev",
            Audience = "https://app.example.com",
            Audiences = new List<string>() {
                "https://app.example.com",
            },
            Subject = "user_2EXAMPLE",
            SessionId = "sess_2EXAMPLE",
            OrganizationId = "org_2EXAMPLE",
            TenantId = "tenant_2EXAMPLE",
            TokenId = "2EXAMPLEJTI",
            IssuedAt = "2026-10-14T08:59:40.000Z",
            ExpiresAt = "2026-10-14T09:00:40.000Z",
            SessionCreatedAt = "2026-10-14T08:57:00.000Z",
            AuthTime = "2026-10-14T08:55:00.000Z",
            AccountCreatedAt = "2026-10-14T08:56:00.000Z",
            Verified = true,
            Mfa = new AuthContextMfa() {
                Methods = new List<string>() {
                    "totp",
                },
            },
            Roles = new List<string>() {
                "admin",
            },
            Scopes = new List<string>() {
                "org:sys_profile:manage",
            },
        },
        IssuedCredential = new AssessmentRequestIssuedCredential() {
            Sha256 = "2bdbf5c72f2a36df0ef40fb92203612274d4546f56dba946070a6a79c2401a1b",
        },
    },
    idempotencyKey: "signup-acct_7f3a9c"
);

// handle response
```
### Example Usage: detailed

<!-- UsageSnippet language="csharp" operationID="createAssessment" method="post" path="/v1/assessments" example="detailed" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.CreateAsync(
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Flow = Flow.Signup,
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
            Username = "new.user",
            Phone = "+61412345678",
            Name = "Alex Example",
            Ip = "203.0.113.45",
        },
        PolicyContext = new AssessmentRequestPolicyContext() {
            Region = "AU",
        },
        RequestId = "req-2026-10-14-0001",
        ClientContext = new AssessmentRequestClientContext() {
            Timezone = "Australia/Sydney",
            Locale = "en-AU",
            AppVersion = "4.12.0",
            UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0 Safari/537.36",
            DeviceId = "dev_5c1e8a",
            SessionId = "sess_9b2d41",
            ClientObservedAt = "2026-10-14T08:59:58.000Z",
        },
        AuthContext = new AuthContext() {
            Provider = "clerk",
            Issuer = "https://example-app.clerk.accounts.dev",
            Audience = "https://app.example.com",
            Audiences = new List<string>() {
                "https://app.example.com",
            },
            Subject = "user_2EXAMPLE",
            SessionId = "sess_2EXAMPLE",
            OrganizationId = "org_2EXAMPLE",
            TenantId = "tenant_2EXAMPLE",
            TokenId = "2EXAMPLEJTI",
            IssuedAt = "2026-10-14T08:59:40.000Z",
            ExpiresAt = "2026-10-14T09:00:40.000Z",
            SessionCreatedAt = "2026-10-14T08:57:00.000Z",
            AuthTime = "2026-10-14T08:55:00.000Z",
            AccountCreatedAt = "2026-10-14T08:56:00.000Z",
            Verified = true,
            Mfa = new AuthContextMfa() {
                Methods = new List<string>() {
                    "totp",
                },
            },
            Roles = new List<string>() {
                "admin",
            },
            Scopes = new List<string>() {
                "org:sys_profile:manage",
            },
        },
        IssuedCredential = new AssessmentRequestIssuedCredential() {
            Sha256 = "2bdbf5c72f2a36df0ef40fb92203612274d4546f56dba946070a6a79c2401a1b",
        },
    },
    idempotencyKey: "signup-acct_7f3a9c"
);

// handle response
```
### Example Usage: full

<!-- UsageSnippet language="csharp" operationID="createAssessment" method="post" path="/v1/assessments" example="full" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.CreateAsync(
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Flow = Flow.Signup,
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
            Username = "new_user_42",
            Phone = "+447700900123",
        },
        PolicyContext = new AssessmentRequestPolicyContext() {
            Region = "GB",
        },
        RequestId = "req-2026-10-14-0001",
        DeadlineMs = 300,
    },
    idempotencyKey: "signup-acct_7f3a9c"
);

// handle response
```
### Example Usage: login

<!-- UsageSnippet language="csharp" operationID="createAssessment" method="post" path="/v1/assessments" example="login" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.CreateAsync(
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Flow = Flow.Signup,
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
            Username = "new.user",
            Phone = "+61412345678",
            Name = "Alex Example",
            Ip = "203.0.113.45",
        },
        PolicyContext = new AssessmentRequestPolicyContext() {
            Region = "AU",
        },
        RequestId = "req-2026-10-14-0001",
        ClientContext = new AssessmentRequestClientContext() {
            Timezone = "Australia/Sydney",
            Locale = "en-AU",
            AppVersion = "4.12.0",
            UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0 Safari/537.36",
            DeviceId = "dev_5c1e8a",
            SessionId = "sess_9b2d41",
            ClientObservedAt = "2026-10-14T08:59:58.000Z",
        },
        AuthContext = new AuthContext() {
            Provider = "clerk",
            Issuer = "https://example-app.clerk.accounts.dev",
            Audience = "https://app.example.com",
            Audiences = new List<string>() {
                "https://app.example.com",
            },
            Subject = "user_2EXAMPLE",
            SessionId = "sess_2EXAMPLE",
            OrganizationId = "org_2EXAMPLE",
            TenantId = "tenant_2EXAMPLE",
            TokenId = "2EXAMPLEJTI",
            IssuedAt = "2026-10-14T08:59:40.000Z",
            ExpiresAt = "2026-10-14T09:00:40.000Z",
            SessionCreatedAt = "2026-10-14T08:57:00.000Z",
            AuthTime = "2026-10-14T08:55:00.000Z",
            AccountCreatedAt = "2026-10-14T08:56:00.000Z",
            Verified = true,
            Mfa = new AuthContextMfa() {
                Methods = new List<string>() {
                    "totp",
                },
            },
            Roles = new List<string>() {
                "admin",
            },
            Scopes = new List<string>() {
                "org:sys_profile:manage",
            },
        },
        IssuedCredential = new AssessmentRequestIssuedCredential() {
            Sha256 = "2bdbf5c72f2a36df0ef40fb92203612274d4546f56dba946070a6a79c2401a1b",
        },
    },
    idempotencyKey: "signup-acct_7f3a9c"
);

// handle response
```
### Example Usage: minimal

<!-- UsageSnippet language="csharp" operationID="createAssessment" method="post" path="/v1/assessments" example="minimal" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.CreateAsync(
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Flow = Flow.Signup,
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
        },
    },
    idempotencyKey: "signup-acct_7f3a9c"
);

// handle response
```
### Example Usage: name_and_ip

<!-- UsageSnippet language="csharp" operationID="createAssessment" method="post" path="/v1/assessments" example="name_and_ip" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.CreateAsync(
    body: new AssessmentRequest() {
        AccountId = "acct_8412",
        Flow = Flow.Signup,
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "jane.doe@example.com",
            Username = "janedoe",
            Phone = "+14155550123",
            Name = "Jane A. Doe",
            Ip = "73.162.18.45",
        },
        PolicyContext = new AssessmentRequestPolicyContext() {
            Region = "US",
        },
        RequestId = "req_01",
        DeadlineMs = 500,
        ResponseMode = ResponseMode.Standard,
    },
    idempotencyKey: "signup-acct_7f3a9c"
);

// handle response
```
### Example Usage: partial

<!-- UsageSnippet language="csharp" operationID="createAssessment" method="post" path="/v1/assessments" example="partial" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.CreateAsync(
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Flow = Flow.Signup,
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
            Username = "new.user",
            Phone = "+61412345678",
            Name = "Alex Example",
            Ip = "203.0.113.45",
        },
        PolicyContext = new AssessmentRequestPolicyContext() {
            Region = "AU",
        },
        RequestId = "req-2026-10-14-0001",
        ClientContext = new AssessmentRequestClientContext() {
            Timezone = "Australia/Sydney",
            Locale = "en-AU",
            AppVersion = "4.12.0",
            UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0 Safari/537.36",
            DeviceId = "dev_5c1e8a",
            SessionId = "sess_9b2d41",
            ClientObservedAt = "2026-10-14T08:59:58.000Z",
        },
        AuthContext = new AuthContext() {
            Provider = "clerk",
            Issuer = "https://example-app.clerk.accounts.dev",
            Audience = "https://app.example.com",
            Audiences = new List<string>() {
                "https://app.example.com",
            },
            Subject = "user_2EXAMPLE",
            SessionId = "sess_2EXAMPLE",
            OrganizationId = "org_2EXAMPLE",
            TenantId = "tenant_2EXAMPLE",
            TokenId = "2EXAMPLEJTI",
            IssuedAt = "2026-10-14T08:59:40.000Z",
            ExpiresAt = "2026-10-14T09:00:40.000Z",
            SessionCreatedAt = "2026-10-14T08:57:00.000Z",
            AuthTime = "2026-10-14T08:55:00.000Z",
            AccountCreatedAt = "2026-10-14T08:56:00.000Z",
            Verified = true,
            Mfa = new AuthContextMfa() {
                Methods = new List<string>() {
                    "totp",
                },
            },
            Roles = new List<string>() {
                "admin",
            },
            Scopes = new List<string>() {
                "org:sys_profile:manage",
            },
        },
        IssuedCredential = new AssessmentRequestIssuedCredential() {
            Sha256 = "2bdbf5c72f2a36df0ef40fb92203612274d4546f56dba946070a6a79c2401a1b",
        },
    },
    idempotencyKey: "signup-acct_7f3a9c"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                                                                             | Type                                                                                                                                                                                                                                                                  | Required                                                                                                                                                                                                                                                              | Description                                                                                                                                                                                                                                                           | Example                                                                                                                                                                                                                                                               |
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `Body`                                                                                                                                                                                                                                                                | [AssessmentRequest](../../Models/Shared/AssessmentRequest.md)                                                                                                                                                                                                         | :heavy_check_mark:                                                                                                                                                                                                                                                    | N/A                                                                                                                                                                                                                                                                   |                                                                                                                                                                                                                                                                       |
| `IdempotencyKey`                                                                                                                                                                                                                                                      | *string*                                                                                                                                                                                                                                                              | :heavy_minus_sign:                                                                                                                                                                                                                                                    | Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the tenant principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call. | signup-acct_7f3a9c                                                                                                                                                                                                                                                    |

### Response

**[CreateAssessmentResponse](../../Models/Operations/CreateAssessmentResponse.md)**

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

Returns the highest assessment_version, with the account's current risk. Requires the assess or history scope. While status is pending, or partial with a source still running, the response carries Retry-After; poll no faster than it says. Another tenant's assessment, an unknown or expired assessment and a malformed assessment_id all answer 404 not_found. response_mode=detailed needs the detailed scope.

### Example Usage: complete

<!-- UsageSnippet language="csharp" operationID="getAssessment" method="get" path="/v1/assessments/{assessment_id}" example="complete" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.GetAsync(assessmentId: "asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```
### Example Usage: detailed

<!-- UsageSnippet language="csharp" operationID="getAssessment" method="get" path="/v1/assessments/{assessment_id}" example="detailed" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.GetAsync(assessmentId: "asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```
### Example Usage: login

<!-- UsageSnippet language="csharp" operationID="getAssessment" method="get" path="/v1/assessments/{assessment_id}" example="login" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.GetAsync(assessmentId: "asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```
### Example Usage: partial

<!-- UsageSnippet language="csharp" operationID="getAssessment" method="get" path="/v1/assessments/{assessment_id}" example="partial" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.GetAsync(assessmentId: "asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```
### Example Usage: pending

<!-- UsageSnippet language="csharp" operationID="getAssessment" method="get" path="/v1/assessments/{assessment_id}" example="pending" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.GetAsync(assessmentId: "asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```
### Example Usage: pending_known_account

<!-- UsageSnippet language="csharp" operationID="getAssessment" method="get" path="/v1/assessments/{assessment_id}" example="pending_known_account" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.GetAsync(assessmentId: "asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```

### Parameters

| Parameter                                                                                                         | Type                                                                                                              | Required                                                                                                          | Description                                                                                                       | Example                                                                                                           |
| ----------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------- |
| `AssessmentId`                                                                                                    | *string*                                                                                                          | :heavy_check_mark:                                                                                                | Path segment, matched case-sensitively. A value that does not match the assessment_id shape answers 404, not 400. | asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                    |
| `ResponseMode`                                                                                                    | [ResponseMode](../../Models/Shared/ResponseMode.md)                                                               | :heavy_minus_sign:                                                                                                | Optional standard or separately authorised detailed customer view; raw is rejected.                               |                                                                                                                   |

### Response

**[GetAssessmentResponse](../../Models/Operations/GetAssessmentResponse.md)**

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

Deletes one assessment in the environment of your key, with its versions, evidence and outcomes and the identity-graph marks its outcomes set, and scores its account again from what remains. Afterwards the assessment answers 404 and leaves the account's history. The answer names the account and the assessment its current risk now comes from; current_assessment_id is absent when the account has no assessment with a decision left. Requires the history scope. An unknown, expired or already deleted assessment answers 404, so a repeat answers 404; to erase a whole account idempotently, use accounts.delete.

### Example Usage: last

<!-- UsageSnippet language="csharp" operationID="deleteAssessment" method="delete" path="/v1/assessments/{assessment_id}" example="last" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.DeleteAsync(assessmentId: "asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```
### Example Usage: next

<!-- UsageSnippet language="csharp" operationID="deleteAssessment" method="delete" path="/v1/assessments/{assessment_id}" example="next" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.DeleteAsync(assessmentId: "asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");

// handle response
```

### Parameters

| Parameter                                                                                                         | Type                                                                                                              | Required                                                                                                          | Description                                                                                                       | Example                                                                                                           |
| ----------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------- |
| `AssessmentId`                                                                                                    | *string*                                                                                                          | :heavy_check_mark:                                                                                                | Path segment, matched case-sensitively. A value that does not match the assessment_id shape answers 404, not 400. | asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                    |

### Response

**[DeleteAssessmentResponse](../../Models/Operations/DeleteAssessmentResponse.md)**

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

## RecordOutcome

Records what you later learned about an assessed account: confirmed_abuse, account_takeover and chargeback report abuse; false_positive and legitimate withdraw your earlier report; unknown is recorded only. Outcomes are append-only and the latest one wins. Requires the assess scope. Idempotency-Key is optional in the SDK and handled as on POST /v1/assessments: a retry with the same key and body answers the same record with Idempotent-Replayed: true; a different body answers 409. An unknown, expired or foreign assessment answers 404 not_found.

### Example Usage: abuse

<!-- UsageSnippet language="csharp" operationID="recordAssessmentOutcome" method="post" path="/v1/assessments/{assessment_id}/outcomes" example="abuse" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.RecordOutcomeAsync(
    assessmentId: "asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new AssessmentOutcomeRequest() {
        Outcome = Outcome.ConfirmedAbuse,
        ObservedAt = "2026-10-20T05:00:00Z",
        ExternalRef = "case-123",
        Notes = "Card testing from this account; closed by trust and safety.",
    },
    idempotencyKey: "signup-acct_7f3a9c"
);

// handle response
```
### Example Usage: recorded

<!-- UsageSnippet language="csharp" operationID="recordAssessmentOutcome" method="post" path="/v1/assessments/{assessment_id}/outcomes" example="recorded" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.RecordOutcomeAsync(
    assessmentId: "asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new AssessmentOutcomeRequest() {
        Outcome = Outcome.Legitimate,
        ObservedAt = "2026-10-14T09:04:30.000Z",
        ExternalRef = "case-48213",
        Notes = "Chargeback filed two days after signup.",
    },
    idempotencyKey: "signup-acct_7f3a9c"
);

// handle response
```
### Example Usage: withdrawn

<!-- UsageSnippet language="csharp" operationID="recordAssessmentOutcome" method="post" path="/v1/assessments/{assessment_id}/outcomes" example="withdrawn" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.RecordOutcomeAsync(
    assessmentId: "asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    body: new AssessmentOutcomeRequest() {
        Outcome = Outcome.FalsePositive,
    },
    idempotencyKey: "signup-acct_7f3a9c"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                                                                             | Type                                                                                                                                                                                                                                                                  | Required                                                                                                                                                                                                                                                              | Description                                                                                                                                                                                                                                                           | Example                                                                                                                                                                                                                                                               |
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `AssessmentId`                                                                                                                                                                                                                                                        | *string*                                                                                                                                                                                                                                                              | :heavy_check_mark:                                                                                                                                                                                                                                                    | Path segment, matched case-sensitively. A value that does not match the assessment_id shape answers 404, not 400.                                                                                                                                                     | asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                                                                                                                        |
| `Body`                                                                                                                                                                                                                                                                | [AssessmentOutcomeRequest](../../Models/Shared/AssessmentOutcomeRequest.md)                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                                                                    | N/A                                                                                                                                                                                                                                                                   |                                                                                                                                                                                                                                                                       |
| `IdempotencyKey`                                                                                                                                                                                                                                                      | *string*                                                                                                                                                                                                                                                              | :heavy_minus_sign:                                                                                                                                                                                                                                                    | Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the tenant principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call. | signup-acct_7f3a9c                                                                                                                                                                                                                                                    |

### Response

**[RecordAssessmentOutcomeResponse](../../Models/Operations/RecordAssessmentOutcomeResponse.md)**

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

## Query

History is a POST with account_id in the body so an account identifier stays out of URLs and access logs. Requires the history scope. Read-only and safe to retry. Newest first, each at its latest assessment_version. An account with no assessments answers 200 with empty items. The cursor is opaque and bound to your tenant and the account; a tampered or expired cursor answers 400 invalid_cursor. response_mode=detailed needs the detailed scope.

### Example Usage: firstPage

<!-- UsageSnippet language="csharp" operationID="queryAccountAssessments" method="post" path="/v1/assessments/query" example="firstPage" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

AssessmentQuery req = new AssessmentQuery() {
    AccountId = "acct_7f3a9c",
};

QueryAccountAssessmentsResponse? res = await sdk.Assessments.QueryAsync(req);

while(res != null)
{
    // handle items

    res = await res.Next!();
}
```
### Example Usage: page

<!-- UsageSnippet language="csharp" operationID="queryAccountAssessments" method="post" path="/v1/assessments/query" example="page" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

AssessmentQuery req = new AssessmentQuery() {
    AccountId = "acct_7f3a9c",
    Cursor = "eyJvIjoyMH0",
};

QueryAccountAssessmentsResponse? res = await sdk.Assessments.QueryAsync(req);

while(res != null)
{
    // handle items

    res = await res.Next!();
}
```

### Parameters

| Parameter                                                 | Type                                                      | Required                                                  | Description                                               |
| --------------------------------------------------------- | --------------------------------------------------------- | --------------------------------------------------------- | --------------------------------------------------------- |
| `request`                                                 | [AssessmentQuery](../../Models/Shared/AssessmentQuery.md) | :heavy_check_mark:                                        | The request object to use for the request.                |

### Response

**[QueryAccountAssessmentsResponse](../../Models/Operations/QueryAccountAssessmentsResponse.md)**

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