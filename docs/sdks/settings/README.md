# Settings

## Overview

### Available Operations

* [Get](#get) - Read an environment's settings.
* [Update](#update) - Change an environment's settings.

## Get

Reads the settings of the signing key's environment: retention in days; per flow (account_creation, login, purchase) the step_up, review and block thresholds, the default depth and the admission; and the sources the environment refuses. A flow without thresholds of its own shows account_creation's with inherits_account_creation true. Requires the management scope; the other environment answers 404 not_found. Read-only and safe to retry.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getSettings" method="get" path="/v1/workspaces/{workspace_id}/settings/{environment}" example="default" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Settings.GetAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Live
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |

### Response

**[GetSettingsResponse](../../Models/Operations/GetSettingsResponse.md)**

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

Changes only what the body names, in one transaction: retention_days, refused_sources, and per flow step_up, review, block (null turns blocking off), default_depth, admission, hold_seconds, on_expiry, or inherits_account_creation to return login or purchase to account_creation's thresholds. After the change each flow keeps 0 < step_up < review <= 100 and block off or review <= block <= 100; a change that breaks it answers 400 naming the flow. A policy change is one new policy_version with the signing key's prefix as updated_by, and every change is recorded in the workspace's audit log with the key as its actor. Requires the management scope; the other environment answers 404 not_found. A change reaches assessments within a minute.

### Example Usage: changed

<!-- UsageSnippet language="csharp" operationID="updateSettings" method="patch" path="/v1/workspaces/{workspace_id}/settings/{environment}" example="changed" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Settings.UpdateAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Live,
    body: new SettingsRequest() {}
);

// handle response
```
### Example Usage: flows

<!-- UsageSnippet language="csharp" operationID="updateSettings" method="patch" path="/v1/workspaces/{workspace_id}/settings/{environment}" example="flows" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;
using System.Collections.Generic;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Settings.UpdateAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Live,
    body: new SettingsRequest() {
        RetentionDays = 90,
        RefusedSources = new List<SourceId>() {
            SourceId.Phone,
        },
        Flows = new SettingsRequestFlows() {
            AccountCreation = new FlowChange() {
                Admission = Admission.Held,
                HoldSeconds = 600,
                OnExpiry = OnExpiry.Review,
            },
            Login = new FlowChange() {
                StepUp = 30,
                Review = 60,
                Block = null,
                DefaultDepth = DefaultDepth.Full,
            },
            Purchase = new FlowChange() {
                InheritsAccountCreation = true,
            },
        },
    }
);

// handle response
```
### Example Usage: signup

<!-- UsageSnippet language="csharp" operationID="updateSettings" method="patch" path="/v1/workspaces/{workspace_id}/settings/{environment}" example="signup" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Settings.UpdateAsync(
    workspaceId: "tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM",
    environment: KeyEnvironment.Test,
    body: new SettingsRequest() {
        Flows = new SettingsRequestFlows() {
            AccountCreation = new FlowChange() {
                Block = 95,
            },
        },
    }
);

// handle response
```

### Parameters

| Parameter                                                                                                                                                              | Type                                                                                                                                                                   | Required                                                                                                                                                               | Description                                                                                                                                                            | Example                                                                                                                                                                |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WorkspaceId`                                                                                                                                                          | *string*                                                                                                                                                               | :heavy_check_mark:                                                                                                                                                     | Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.                                     | tnt_01JA2Z8K6Q4V7W9XBCDEFGHJKM                                                                                                                                         |
| `Environment`                                                                                                                                                          | [KeyEnvironment](../../Models/Shared/KeyEnvironment.md)                                                                                                                | :heavy_check_mark:                                                                                                                                                     | The environment: live or test. Each has its own settings. Any other value answers 400. An API key reads and changes only its own environment's; the other answers 404. |                                                                                                                                                                        |
| `Body`                                                                                                                                                                 | [SettingsRequest](../../Models/Shared/SettingsRequest.md)                                                                                                              | :heavy_check_mark:                                                                                                                                                     | N/A                                                                                                                                                                    |                                                                                                                                                                        |

### Response

**[UpdateSettingsResponse](../../Models/Operations/UpdateSettingsResponse.md)**

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