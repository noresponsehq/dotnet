# Accounts

## Overview

Your accounts, each with one current risk, and their assessment history.

### Available Operations

* [Get](#get) - Read an account's one current risk.
* [ListAssessments](#listassessments) - Page through one account's unexpired assessments.

## Get

An account is the account_id you send with your assessments. Its current risk is the risk of its latest assessment with a decision (status complete or partial), scored as GET /v1/assessments/{assessment_id} scores that assessment, with a decision for each flow. Every assessment updates its account: once an assessment completes, the account carries its risk. Requires the history scope. Read-only and safe to retry. An account with no unexpired assessment that has a decision answers 404 not_found, and a malformed account_id answers 400 invalid_request.

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

| Parameter                                                                                                                                                                                                       | Type                                                                                                                                                                                                            | Required                                                                                                                                                                                                        | Description                                                                                                                                                                                                     | Example                                                                                                                                                                                                         |
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `AccountId`                                                                                                                                                                                                     | *string*                                                                                                                                                                                                        | :heavy_check_mark:                                                                                                                                                                                              | The account_id the tenant sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                     |

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

## ListAssessments

The account's history: the same page as POST /v1/assessments/query for this account_id, newest first, each at its latest assessment_version. Requires the history scope. Read-only and safe to retry. An account with no assessments answers 200 with empty items. The cursor is opaque and bound to your tenant and the account; a tampered or expired cursor answers 400 invalid_cursor. response_mode=detailed needs the detailed scope. The account_id travels in the URL; use POST /v1/assessments/query to keep it out of access logs.

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

| Parameter                                                                                                                                                                                                       | Type                                                                                                                                                                                                            | Required                                                                                                                                                                                                        | Description                                                                                                                                                                                                     | Example                                                                                                                                                                                                         |
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `AccountId`                                                                                                                                                                                                     | *string*                                                                                                                                                                                                        | :heavy_check_mark:                                                                                                                                                                                              | The account_id the tenant sends with its assessments, as one path segment (percent-encode '\|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request. | acct_7f3a9c                                                                                                                                                                                                     |
| `Limit`                                                                                                                                                                                                         | *long*                                                                                                                                                                                                          | :heavy_minus_sign:                                                                                                                                                                                              | Items per page.                                                                                                                                                                                                 |                                                                                                                                                                                                                 |
| `Cursor`                                                                                                                                                                                                        | *string*                                                                                                                                                                                                        | :heavy_minus_sign:                                                                                                                                                                                              | next_cursor from the previous page, unchanged.                                                                                                                                                                  |                                                                                                                                                                                                                 |
| `ResponseMode`                                                                                                                                                                                                  | [ResponseMode](../../Models/Shared/ResponseMode.md)                                                                                                                                                             | :heavy_minus_sign:                                                                                                                                                                                              | Optional standard or separately authorised detailed customer view; raw is rejected.                                                                                                                             |                                                                                                                                                                                                                 |

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