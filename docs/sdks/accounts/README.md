# Accounts

## Overview

Your accounts, each with one current risk, and their assessment history.

### Available Operations

* [ListChanged](#listchanged) - Poll the accounts whose risk changed.
* [Get](#get) - Read an account's one current risk.
* [Delete](#delete) - Erase an account and its history.
* [ListAssessments](#listassessments) - Page through one account's unexpired assessments.
* [ListLinked](#listlinked) - List accounts that share a submitted identifier with this account.
* [RecordEnforcement](#recordenforcement) - Record what you did to an account in your identity provider.
* [ListEnforcements](#listenforcements) - Page through account enforcements
* [RecordOutcome](#recordoutcome) - Record or correct an account outcome
* [ListOutcomes](#listoutcomes) - List retained account feedback
* [RetrieveDecision](#retrievedecision) - Retrieve an exact historical account decision
* [AssociateCredential](#associatecredential) - Associate a customer-issued API key
* [ListCredentials](#listcredentials) - List issued credential associations
* [RetireCredential](#retirecredential) - Retire an issued credential association
* [Export](#export) - Export an account
* [ListDecisions](#listdecisions) - Page through recorded account decisions
* [Search](#search) - Search distinct accounts by submitted identifier or account ID

## ListChanged

Your accounts whose risk changed, oldest change first, each as accounts.get answers it. Every page carries next_cursor: pass it back as changed_since to read the changes made after that page; without changed_since the list starts from the oldest change. When nothing changed, items is empty and next_cursor is the cursor you sent, so poll by calling again with the latest next_cursor. An account appears again each time its risk changes, with a higher risk_version; keep the copy with the highest. unenforced=true lists only accounts with a decision other than allow for some flow and no applied enforcement at their current risk_version; such a page can hold fewer than limit items, or none, while later changes exist, so keep polling with next_cursor. Requires the history scope. Read-only and safe to retry. A tampered or foreign cursor answers 400 invalid_cursor.

### Example Usage: caughtUp

<!-- UsageSnippet language="csharp" operationID="listAccounts" method="get" path="/v1/accounts" example="caughtUp" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Accounts.ListChangedAsync(
    limit: 50,
    unenforced: false
);

// handle response
```
### Example Usage: page

<!-- UsageSnippet language="csharp" operationID="listAccounts" method="get" path="/v1/accounts" example="page" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Accounts.ListChangedAsync(
    limit: 50,
    unenforced: false
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                               | Type                                                                                                                                                                                    | Required                                                                                                                                                                                | Description                                                                                                                                                                             |
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `ChangedSince`                                                                                                                                                                          | *string*                                                                                                                                                                                | :heavy_minus_sign:                                                                                                                                                                      | next_cursor from an earlier page, unchanged. Absent, the list starts from the oldest change.                                                                                            |
| `Limit`                                                                                                                                                                                 | *long*                                                                                                                                                                                  | :heavy_minus_sign:                                                                                                                                                                      | Most accounts per page.                                                                                                                                                                 |
| `Unenforced`                                                                                                                                                                            | *bool*                                                                                                                                                                                  | :heavy_minus_sign:                                                                                                                                                                      | true lists only accounts with no applied enforcement at their current risk_version that have a decision other than allow for some flow or a hold no later applied enforcement resolved. |

### Response

**[ListAccountsResponse](../../Models/Operations/ListAccountsResponse.md)**

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

An account is the account_id you send with your assessments. Its current risk is the risk of its latest assessment with a decision (status complete or partial), scored as GET /v1/assessments/{assessment_id} scores that assessment, with a decision for each flow; risk.decision is the decision for the flow of that assessment. Every assessment answers with this same account. Every assessment updates its account: once an assessment completes, the account carries its risk. Requires the history scope. Read-only and safe to retry. An account with no unexpired assessment that has a decision answers 404 not_found, and a malformed account_id answers 400 invalid_request.

### Example Usage: blocked

<!-- UsageSnippet language="csharp" operationID="getAccount" method="get" path="/v1/accounts/{account_id}" example="blocked" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Accounts.GetAsync(accountId: "acct_7f3a9c");

// handle response
```
### Example Usage: current

<!-- UsageSnippet language="csharp" operationID="getAccount" method="get" path="/v1/accounts/{account_id}" example="current" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Accounts.GetAsync(accountId: "acct_7f3a9c");

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                          | Type                                                                                                                                                                                                               | Required                                                                                                                                                                                                           | Description                                                                                                                                                                                                        | Example                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                        |

### Response

**[GetAccountResponse](../../Models/Operations/GetAccountResponse.md)**

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

Erases the account in the environment of your key, for an erasure request from your user: every assessment of it, with its revisions, evidence and outcomes, and the identity-graph marks its outcomes set. An assessment of the account begun before the call is refused or stopped. Afterwards the account and each erased assessment answer 404 and the history is empty. When the call deleted at least one assessment, an account.deleted event goes to your verified webhook endpoints. Requires the history scope. Idempotent and safe to retry: a repeat, or an account with nothing to erase, answers 200 with assessments_deleted 0 and sends no event. The account_id can be assessed again later, as a new account.

### Example Usage: erased

<!-- UsageSnippet language="csharp" operationID="deleteAccount" method="delete" path="/v1/accounts/{account_id}" example="erased" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Accounts.DeleteAsync(accountId: "acct_7f3a9c");

// handle response
```
### Example Usage: repeat

<!-- UsageSnippet language="csharp" operationID="deleteAccount" method="delete" path="/v1/accounts/{account_id}" example="repeat" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Accounts.DeleteAsync(accountId: "acct_7f3a9c");

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                          | Type                                                                                                                                                                                                               | Required                                                                                                                                                                                                           | Description                                                                                                                                                                                                        | Example                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                        |

### Response

**[DeleteAccountResponse](../../Models/Operations/DeleteAccountResponse.md)**

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

## ListAssessments

The account's history: the same page as POST /v1/assessments/query for this account_id, newest first, each at its latest assessment_version. Requires the history scope. Read-only and safe to retry. An account with no assessments answers 200 with empty items. The cursor is opaque and bound to your workspace and the account; a tampered or expired cursor answers 400 invalid_cursor. response_mode=detailed needs the detailed scope. Each item's identifiers holds the identifiers its assessment was sent, exactly as received. The account_id travels in the URL; use POST /v1/assessments/query to keep it out of access logs.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listAccountAssessments" method="get" path="/v1/accounts/{account_id}/assessments" example="page" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

ListAccountAssessmentsResponse? res = await sdk.Accounts.ListAssessmentsAsync(
    accountId: "acct_7f3a9c",
    limit: 20
);

while(res != null)
{
    // handle items

    res = await res.Next!();
}
```

### Parameters

| Parameter                                                                                                                                                                                                          | Type                                                                                                                                                                                                               | Required                                                                                                                                                                                                           | Description                                                                                                                                                                                                        | Example                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                        |
| `Limit`                                                                                                                                                                                                            | *long*                                                                                                                                                                                                             | :heavy_minus_sign:                                                                                                                                                                                                 | Items per page.                                                                                                                                                                                                    |                                                                                                                                                                                                                    |
| `Cursor`                                                                                                                                                                                                           | *string*                                                                                                                                                                                                           | :heavy_minus_sign:                                                                                                                                                                                                 | next_cursor from the previous page, unchanged.                                                                                                                                                                     |                                                                                                                                                                                                                    |
| `ResponseMode`                                                                                                                                                                                                     | [ResponseMode](../../Models/Shared/ResponseMode.md)                                                                                                                                                                | :heavy_minus_sign:                                                                                                                                                                                                 | Optional standard or separately authorised detailed customer view; raw is rejected.                                                                                                                                |                                                                                                                                                                                                                    |

### Response

**[ListAccountAssessmentsResponse](../../Models/Operations/ListAccountAssessmentsResponse.md)**

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

## ListLinked

Lists the other accounts in this environment whose retained assessments submitted an email, phone or customer-namespace username that one of this account's retained assessments also submitted. Matching uses the identifier search rules: exact, canonical and, for email, mailbox. Each linked account names one entry per shared kind, at the strictest basis that links the two accounts, with the original value, assessment_id and submitted_at of each account's newest retained submission of it. Only customer-submitted identifiers link accounts; source-discovered identifiers do not. Expired, deleted and erased assessments link nothing. The account itself is not listed, and an account with no retained submissions answers 200 with empty items. A shared identifier is not verified ownership. Totals are exact below 10000 and capped at 10000. The cursor is opaque, expiring and bound to your workspace, environment and the account; a tampered or expired cursor answers 400 invalid_cursor. Requires the history scope, or investigate with evidence. Read-only and safe to retry.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listLinkedAccounts" method="get" path="/v1/accounts/{account_id}/linked_accounts" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

ListLinkedAccountsResponse? res = await sdk.Accounts.ListLinkedAsync(
    accountId: "acct_7f3a9c",
    limit: 25
);

while(res != null)
{
    // handle items

    res = await res.Next!();
}
```

### Parameters

| Parameter                                                                                                                                                                                                          | Type                                                                                                                                                                                                               | Required                                                                                                                                                                                                           | Description                                                                                                                                                                                                        | Example                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                        |
| `Limit`                                                                                                                                                                                                            | *long*                                                                                                                                                                                                             | :heavy_minus_sign:                                                                                                                                                                                                 | Linked accounts per page.                                                                                                                                                                                          |                                                                                                                                                                                                                    |
| `Cursor`                                                                                                                                                                                                           | *string*                                                                                                                                                                                                           | :heavy_minus_sign:                                                                                                                                                                                                 | next_cursor from the previous page, unchanged.                                                                                                                                                                     |                                                                                                                                                                                                                    |

### Response

**[ListLinkedAccountsResponse](../../Models/Operations/ListLinkedAccountsResponse.md)**

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

## RecordEnforcement

Records what you did to the account in your identity provider for one flow's decision at one risk_version: the action (or none), the account's state before it, the provider and whether it was applied or failed. It is your action, not an outcome, and it leaves the account's risk unchanged. A record is kept once per account, risk_version, flow and action: the first report answers 201, and a repeat answers 200 with the stored record unchanged, so retries are safe without an Idempotency-Key. An applied report after a failed one replaces the failed record, keeping its id and created_at, and answers 201. A risk_version older than the account's current one is accepted and stored, but only an applied record at the current risk_version takes the account out of listChanged with unenforced=true; a newer one answers 409 risk_version_ahead. An account with no current risk answers 404 not_found. error_code is allowed only with status failed. Requires the history scope.

### Example Usage: applied

<!-- UsageSnippet language="csharp" operationID="recordEnforcement" method="post" path="/v1/accounts/{account_id}/enforcements" example="applied" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Accounts.RecordEnforcementAsync(
    accountId: "acct_7f3a9c",
    body: new EnforcementRequest() {
        RiskVersion = 3,
        Flow = Flow.AccountCreation,
        Action = EnforcementAction.Restrict,
        StateBefore = EnforcementState.Provisional,
        Status = EnforcementStatus.Applied,
        Provider = EnforcementProvider.Clerk,
    }
);

// handle response
```
### Example Usage: failed

<!-- UsageSnippet language="csharp" operationID="recordEnforcement" method="post" path="/v1/accounts/{account_id}/enforcements" example="failed" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Accounts.RecordEnforcementAsync(
    accountId: "acct_7f3a9c",
    body: new EnforcementRequest() {
        RiskVersion = 3,
        Flow = Flow.AccountCreation,
        Action = EnforcementAction.Restrict,
        StateBefore = EnforcementState.Provisional,
        Status = EnforcementStatus.Failed,
        Provider = EnforcementProvider.Clerk,
        ErrorCode = "clerk.rate_limited",
    }
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                          | Type                                                                                                                                                                                                               | Required                                                                                                                                                                                                           | Description                                                                                                                                                                                                        | Example                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                        |
| `Body`                                                                                                                                                                                                             | [EnforcementRequest](../../Models/Shared/EnforcementRequest.md)                                                                                                                                                    | :heavy_check_mark:                                                                                                                                                                                                 | N/A                                                                                                                                                                                                                |                                                                                                                                                                                                                    |

### Response

**[RecordEnforcementResponse](../../Models/Operations/RecordEnforcementResponse.md)**

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

## ListEnforcements

List retained enforcement reports for one account with an authenticated cursor.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listAccountEnforcements" method="get" path="/v1/accounts/{account_id}/enforcements" example="page" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

ListAccountEnforcementsResponse? res = await sdk.Accounts.ListEnforcementsAsync(
    accountId: "acct_7f3a9c",
    limit: 20
);

while(res != null)
{
    // handle items

    res = await res.Next!();
}
```

### Parameters

| Parameter                                                                                                                                                                                                          | Type                                                                                                                                                                                                               | Required                                                                                                                                                                                                           | Description                                                                                                                                                                                                        | Example                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                        |
| `Limit`                                                                                                                                                                                                            | *long*                                                                                                                                                                                                             | :heavy_minus_sign:                                                                                                                                                                                                 | Items per page.                                                                                                                                                                                                    |                                                                                                                                                                                                                    |
| `Cursor`                                                                                                                                                                                                           | *string*                                                                                                                                                                                                           | :heavy_minus_sign:                                                                                                                                                                                                 | next_cursor from the previous page, unchanged.                                                                                                                                                                     |                                                                                                                                                                                                                    |

### Response

**[ListAccountEnforcementsResponse](../../Models/Operations/ListAccountEnforcementsResponse.md)**

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

Record a factual report, not an enforcement action. A known outcome replaces the effective report. unknown alone records uncertainty without changing it; unknown with supersedes explicitly retracts it. supersedes must name the current effective report or the request answers 409 outcome_conflict. Reports remain append-only. Idempotency-Key is required. observed_at defaults to receipt time and cannot be over five minutes ahead.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="recordAccountOutcome" method="post" path="/v1/accounts/{account_id}/outcomes" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Accounts.RecordOutcomeAsync(
    accountId: "acct_7f3a9c",
    body: new AccountOutcomeRequest() {
        Outcome = Outcome.Unknown,
        ObservedAt = "2026-01-01T00:00:00Z",
        ExternalRef = "case_123",
        Supersedes = "out_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    },
    idempotencyKey: "account-acct_7f3a9c"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                                                                                | Type                                                                                                                                                                                                                                                                     | Required                                                                                                                                                                                                                                                                 | Description                                                                                                                                                                                                                                                              | Example                                                                                                                                                                                                                                                                  |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                                                                              | *string*                                                                                                                                                                                                                                                                 | :heavy_check_mark:                                                                                                                                                                                                                                                       | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request.                                                      | acct_7f3a9c                                                                                                                                                                                                                                                              |
| `Body`                                                                                                                                                                                                                                                                   | [AccountOutcomeRequest](../../Models/Shared/AccountOutcomeRequest.md)                                                                                                                                                                                                    | :heavy_check_mark:                                                                                                                                                                                                                                                       | N/A                                                                                                                                                                                                                                                                      |                                                                                                                                                                                                                                                                          |
| `IdempotencyKey`                                                                                                                                                                                                                                                         | *string*                                                                                                                                                                                                                                                                 | :heavy_minus_sign:                                                                                                                                                                                                                                                       | Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the workspace principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call. | account-acct_7f3a9c                                                                                                                                                                                                                                                      |

### Response

**[RecordAccountOutcomeResponse](../../Models/Operations/RecordAccountOutcomeResponse.md)**

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

## ListOutcomes

List retained account reports, newest first. The page-level effective outcome id names the current report. Each receipt preserves the effective report and risk version from its original write. Page with the returned cursor.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listAccountOutcomes" method="get" path="/v1/accounts/{account_id}/outcomes" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

ListAccountOutcomesResponse? res = await sdk.Accounts.ListOutcomesAsync(
    accountId: "acct_7f3a9c",
    limit: 20
);

while(res != null)
{
    // handle items

    res = await res.Next!();
}
```

### Parameters

| Parameter                                                                                                                                                                                                          | Type                                                                                                                                                                                                               | Required                                                                                                                                                                                                           | Description                                                                                                                                                                                                        | Example                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                        |
| `Limit`                                                                                                                                                                                                            | *long*                                                                                                                                                                                                             | :heavy_minus_sign:                                                                                                                                                                                                 | Items per page.                                                                                                                                                                                                    |                                                                                                                                                                                                                    |
| `Cursor`                                                                                                                                                                                                           | *string*                                                                                                                                                                                                           | :heavy_minus_sign:                                                                                                                                                                                                 | next_cursor from the previous page, unchanged.                                                                                                                                                                     |                                                                                                                                                                                                                    |

### Response

**[ListAccountOutcomesResponse](../../Models/Operations/ListAccountOutcomesResponse.md)**

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

## RetrieveDecision

Retrieve the account decision at one recorded risk version. The answer uses the original assessment and account-specific risk inputs. Earlier versions that were not captured return 404. The assessment version, policy version and account calculation version identify the decision.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getAccountDecision" method="get" path="/v1/accounts/{account_id}/decisions/{risk_version}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Accounts.RetrieveDecisionAsync(
    accountId: "acct_7f3a9c",
    riskVersion: 269840
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                          | Type                                                                                                                                                                                                               | Required                                                                                                                                                                                                           | Description                                                                                                                                                                                                        | Example                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                        |
| `RiskVersion`                                                                                                                                                                                                      | *long*                                                                                                                                                                                                             | :heavy_check_mark:                                                                                                                                                                                                 | The canonical decimal risk_version originally served.                                                                                                                                                              |                                                                                                                                                                                                                    |

### Response

**[GetAccountDecisionResponse](../../Models/Operations/GetAccountDecisionResponse.md)**

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

## AssociateCredential

Associate the SHA-256 of a high-entropy key issued by your service. Send the hash instead of the key. Repeating a hash returns its original association, including its retirement state. A hash associated with another account returns a conflict.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="associateCredential" method="post" path="/v1/accounts/{account_id}/credentials" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Accounts.AssociateCredentialAsync(
    accountId: "acct_7f3a9c",
    body: new CredentialRequest() {
        Sha256 = "5da2158606a4cb396280c842b578872bb64b96c4eb80dc02e6fb4e0e864b77c6",
    }
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                          | Type                                                                                                                                                                                                               | Required                                                                                                                                                                                                           | Description                                                                                                                                                                                                        | Example                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                        |
| `Body`                                                                                                                                                                                                             | [CredentialRequest](../../Models/Shared/CredentialRequest.md)                                                                                                                                                      | :heavy_check_mark:                                                                                                                                                                                                 | N/A                                                                                                                                                                                                                |                                                                                                                                                                                                                    |

### Response

**[AssociateCredentialResponse](../../Models/Operations/AssociateCredentialResponse.md)**

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

## ListCredentials

Retained credential associations, ordered by id. Page with the authenticated cursor. Associations are removed on account erasure.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listAccountCredentials" method="get" path="/v1/accounts/{account_id}/credentials" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

ListAccountCredentialsResponse? res = await sdk.Accounts.ListCredentialsAsync(
    accountId: "acct_7f3a9c",
    limit: 20
);

while(res != null)
{
    // handle items

    res = await res.Next!();
}
```

### Parameters

| Parameter                                                                                                                                                                                                          | Type                                                                                                                                                                                                               | Required                                                                                                                                                                                                           | Description                                                                                                                                                                                                        | Example                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                        |
| `Limit`                                                                                                                                                                                                            | *long*                                                                                                                                                                                                             | :heavy_minus_sign:                                                                                                                                                                                                 | Items per page.                                                                                                                                                                                                    |                                                                                                                                                                                                                    |
| `Cursor`                                                                                                                                                                                                           | *string*                                                                                                                                                                                                           | :heavy_minus_sign:                                                                                                                                                                                                 | next_cursor from the previous page, unchanged.                                                                                                                                                                     |                                                                                                                                                                                                                    |

### Response

**[ListAccountCredentialsResponse](../../Models/Operations/ListAccountCredentialsResponse.md)**

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

## RetireCredential

Mark this association retired, idempotently. This does not revoke the actual key. Requires delete scope.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="retireAccountCredential" method="delete" path="/v1/accounts/{account_id}/credentials/{credential_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Accounts.RetireCredentialAsync(
    accountId: "acct_7f3a9c",
    credentialId: "<id>"
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                          | Type                                                                                                                                                                                                               | Required                                                                                                                                                                                                           | Description                                                                                                                                                                                                        | Example                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                        |
| `CredentialId`                                                                                                                                                                                                     | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | N/A                                                                                                                                                                                                                |                                                                                                                                                                                                                    |

### Response

**[RetireAccountCredentialResponse](../../Models/Operations/RetireAccountCredentialResponse.md)**

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

## Export

Retrieve one consistent snapshot of retained customer-accessible records, including assessment revisions, account decisions, feedback, key associations, enforcements and request diagnostics. Each assessment revision's detail.identifiers holds the identifiers that assessment was sent, exactly as received. Secrets, restricted source information, other customers' data and legacy free-text notes are excluded. The same standard/detailed permissions apply. An export exceeding 10000 records or 8 MiB returns export_too_large without a partial result. Historical decisions that were not captured are identified as an exclusion.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="exportAccount" method="get" path="/v1/accounts/{account_id}/export" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Accounts.ExportAsync(accountId: "acct_7f3a9c");

// handle response
```

### Parameters

| Parameter                                                                                                                                                                                                          | Type                                                                                                                                                                                                               | Required                                                                                                                                                                                                           | Description                                                                                                                                                                                                        | Example                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                        |
| `ResponseMode`                                                                                                                                                                                                     | [ResponseMode](../../Models/Shared/ResponseMode.md)                                                                                                                                                                | :heavy_minus_sign:                                                                                                                                                                                                 | standard returns the consolidated risk response; detailed adds merged provider-neutral facts when the key has detailed scope. There is no raw mode.                                                                |                                                                                                                                                                                                                    |

### Response

**[ExportAccountResponse](../../Models/Operations/ExportAccountResponse.md)**

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

## ListDecisions

List retained immutable risk decisions for one account with an authenticated cursor.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listAccountDecisions" method="get" path="/v1/accounts/{account_id}/decisions" example="page" -->
```csharp
using NoResponse;
using NoResponse.Models.Operations;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

ListAccountDecisionsResponse? res = await sdk.Accounts.ListDecisionsAsync(
    accountId: "acct_7f3a9c",
    limit: 20
);

while(res != null)
{
    // handle items

    res = await res.Next!();
}
```

### Parameters

| Parameter                                                                                                                                                                                                          | Type                                                                                                                                                                                                               | Required                                                                                                                                                                                                           | Description                                                                                                                                                                                                        | Example                                                                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `AccountId`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                           | :heavy_check_mark:                                                                                                                                                                                                 | The account_id the workspace sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                        |
| `Limit`                                                                                                                                                                                                            | *long*                                                                                                                                                                                                             | :heavy_minus_sign:                                                                                                                                                                                                 | Items per page.                                                                                                                                                                                                    |                                                                                                                                                                                                                    |
| `Cursor`                                                                                                                                                                                                           | *string*                                                                                                                                                                                                           | :heavy_minus_sign:                                                                                                                                                                                                 | next_cursor from the previous page, unchanged.                                                                                                                                                                     |                                                                                                                                                                                                                    |

### Response

**[ListAccountDecisionsResponse](../../Models/Operations/ListAccountDecisionsResponse.md)**

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

## Search

Search distinct accounts observed in retained assessments in this environment. identifier matches an exact, canonical or explicitly selected mailbox form of a submitted email, phone or customer-namespace username. It matches historical retained inputs and returns matched_identifier with the original value and matching assessment. Each row has its newest retained assessment, current account decision when available, retained assessment count, and total distinct-account count. identifier and legacy email are mutually exclusive; account_id and needs_action combine with either using AND. Set needs_action to select accounts with a retained open or in-progress review, or a current review/block account decision without an effective retained account outcome. This filter applies before counting and pagination and adds waiting_for reasons. Current decisions include account list overrides and shared-key scoring and preserve admission holds. Typed identifier search uses stable opaque account order; ordinary and legacy email searches retain newest-seen order. Results are live across pages; each page's items and total share a database snapshot, while current account risk is read separately. Cursors are expiring and bound to scope, operation and filters. Totals are exact below 10000 and capped at 10000. Requires history or investigate authority and identifier-read permission for identifier, identifier_expression or email filtering. needs_action additionally requires management scope. Source-discovered associations are not searched and equality is not verified ownership. identifier_expression combines submitted terms with bounded AND/OR, at most 8 raw terms and 3 group levels. Each node selects exactly one of term, and, or. Groups have 2 to 8 operands. Duplicate normalized terms are idempotent and still count toward the limit. identifier_expression, identifier and email are mutually exclusive. Expression results use matched_identifiers with original submitted witnesses and zero-based depth-first term_index. All satisfied unique terms are returned in first-occurrence order. Cursors bind scope, ordered structure, original values, matching modes and all other filters; changing only page limit is allowed. Required expression scope assessment evaluates the whole expression in one retained submission; scope account allows different retained submissions of the same account to satisfy different terms. Both use stable opaque account ordering and distinct-account counting before pagination.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="searchAccounts" method="post" path="/v1/accounts/search" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

AccountDirectorySearchRequest req = new AccountDirectorySearchRequest() {
    IdentifierExpression = new IdentifierExpression() {
        Scope = ScopeEnum.Account,
        Expression = new IdentifierExpressionNode() {
            And = new List<IdentifierExpressionNodeLevelTwo>() {
                new IdentifierExpressionNodeLevelTwo() {
                    Term = new IdentifierSearch() {
                        Kind = IdentifierSearchKind.Phone,
                        Value = "+14155552671",
                        Match = Match.Canonical,
                    },
                },
                new IdentifierExpressionNodeLevelTwo() {
                    Or = new List<IdentifierExpressionNodeLevelOne>() {
                        new IdentifierExpressionNodeLevelOne() {
                            Term = new IdentifierSearch() {
                                Kind = IdentifierSearchKind.Email,
                                Value = "case@example.com",
                                Match = Match.Exact,
                            },
                        },
                        new IdentifierExpressionNodeLevelOne() {
                            Term = new IdentifierSearch() {
                                Kind = IdentifierSearchKind.Email,
                                Value = "other@example.com",
                                Match = Match.Exact,
                            },
                        },
                    },
                },
            },
        },
    },
};

var res = await sdk.Accounts.SearchAsync(req);

// handle response
```

### Parameters

| Parameter                                                                             | Type                                                                                  | Required                                                                              | Description                                                                           |
| ------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------- |
| `request`                                                                             | [AccountDirectorySearchRequest](../../Models/Shared/AccountDirectorySearchRequest.md) | :heavy_check_mark:                                                                    | The request object to use for the request.                                            |

### Response

**[SearchAccountsResponse](../../Models/Operations/SearchAccountsResponse.md)**

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