# WebhookEndpoints

## Overview

Your webhook endpoints: register, list, rotate signing keys, send test events and delete.

### Available Operations

* [List](#list) - List the tenant's webhook endpoints.
* [Create](#create) - Register a webhook endpoint.
* [Delete](#delete) - Delete a webhook endpoint.
* [RotateSigningKey](#rotatesigningkey) - Rotate an endpoint's signing key.
* [SendTestEvent](#sendtestevent) - Send a test event to an endpoint.

## List

Lists your webhook endpoints in every state, ordered by endpoint_id, with the ids of their signing keys and without secrets. Requires the webhooks scope.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="listWebhookEndpoints" method="get" path="/v1/webhooks" example="endpoints" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseClient(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

var res = await sdk.WebhookEndpoints.ListAsync();

// handle response
```

### Response

**[ListWebhookEndpointsResponse](../../Models/Operations/ListWebhookEndpointsResponse.md)**

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

Registers a webhook endpoint and returns it with its first signing secret, shown only in this answer: store it where your receiver reads it. The URL is https on port 443 or 8443 with a DNS host whose every address is public; it is checked now and on every delivery. The endpoint starts unverified and receives assessment events once it echoes an endpoint.verification challenge, which sendTestEvent sends. You can hold 5 endpoints. Requires the webhooks scope, and response_mode detailed also the detailed scope. Registration is not idempotent: a retry after a lost answer registers a second endpoint, which you can list and delete.

### Example Usage: created

<!-- UsageSnippet language="csharp" operationID="createWebhookEndpoint" method="post" path="/v1/webhooks" example="created" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseClient(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

WebhookEndpointRequest req = new WebhookEndpointRequest() {
    Url = "https://hooks.example.com/noresponse",
};

var res = await sdk.WebhookEndpoints.CreateAsync(req);

// handle response
```
### Example Usage: detailed

<!-- UsageSnippet language="csharp" operationID="createWebhookEndpoint" method="post" path="/v1/webhooks" example="detailed" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseClient(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

WebhookEndpointRequest req = new WebhookEndpointRequest() {
    Url = "https://hooks.example.com:8443/noresponse/detailed",
    ResponseMode = ResponseMode.Detailed,
};

var res = await sdk.WebhookEndpoints.CreateAsync(req);

// handle response
```
### Example Usage: standard

<!-- UsageSnippet language="csharp" operationID="createWebhookEndpoint" method="post" path="/v1/webhooks" example="standard" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseClient(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

WebhookEndpointRequest req = new WebhookEndpointRequest() {
    Url = "https://hooks.example.com/noresponse",
};

var res = await sdk.WebhookEndpoints.CreateAsync(req);

// handle response
```

### Parameters

| Parameter                                                               | Type                                                                    | Required                                                                | Description                                                             |
| ----------------------------------------------------------------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------- |
| `request`                                                               | [WebhookEndpointRequest](../../Models/Shared/WebhookEndpointRequest.md) | :heavy_check_mark:                                                      | The request object to use for the request.                              |

### Response

**[CreateWebhookEndpointResponse](../../Models/Operations/CreateWebhookEndpointResponse.md)**

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

Deletes the endpoint and its signing keys. Deliveries still queued for it are dropped. Requires the webhooks scope. An endpoint that is already gone answers 404.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="deleteWebhookEndpoint" method="delete" path="/v1/webhooks/{endpoint_id}" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseClient(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

var res = await sdk.WebhookEndpoints.DeleteAsync(endpointId: "whe_01JA2ZD0E1F2G3H4J5K6M7N8P9");

// handle response
```

### Parameters

| Parameter                                                                                 | Type                                                                                      | Required                                                                                  | Description                                                                               | Example                                                                                   |
| ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- |
| `EndpointId`                                                                              | *string*                                                                                  | :heavy_check_mark:                                                                        | The endpoint's whe_ id. An id that is unknown, malformed or another tenant's answers 404. | whe_01JA2ZD0E1F2G3H4J5K6M7N8P9                                                            |

### Response

**[DeleteWebhookEndpointResponse](../../Models/Operations/DeleteWebhookEndpointResponse.md)**

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

## RotateSigningKey

Issues a new signing key for the endpoint and returns its secret once. Every delivery from now on is signed with it; the key it replaces keeps verifying for 7 days, so your receiver can hold both while it switches. Requires the webhooks scope. Rotation is not idempotent: a retry after a lost answer issues another key, which replaces the first.

### Example Usage: empty

<!-- UsageSnippet language="csharp" operationID="rotateWebhookSigningKey" method="post" path="/v1/webhooks/{endpoint_id}/rotate" example="empty" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseClient(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

var res = await sdk.WebhookEndpoints.RotateSigningKeyAsync(
    endpointId: "whe_01JA2ZD0E1F2G3H4J5K6M7N8P9",
    body: new WebhookKeyRotationRequest() {}
);

// handle response
```
### Example Usage: rotated

<!-- UsageSnippet language="csharp" operationID="rotateWebhookSigningKey" method="post" path="/v1/webhooks/{endpoint_id}/rotate" example="rotated" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseClient(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

var res = await sdk.WebhookEndpoints.RotateSigningKeyAsync(
    endpointId: "whe_01JA2ZD0E1F2G3H4J5K6M7N8P9",
    body: new WebhookKeyRotationRequest() {}
);

// handle response
```

### Parameters

| Parameter                                                                                 | Type                                                                                      | Required                                                                                  | Description                                                                               | Example                                                                                   |
| ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- |
| `EndpointId`                                                                              | *string*                                                                                  | :heavy_check_mark:                                                                        | The endpoint's whe_ id. An id that is unknown, malformed or another tenant's answers 404. | whe_01JA2ZD0E1F2G3H4J5K6M7N8P9                                                            |
| `Body`                                                                                    | [WebhookKeyRotationRequest](../../Models/Shared/WebhookKeyRotationRequest.md)             | :heavy_check_mark:                                                                        | N/A                                                                                       |                                                                                           |

### Response

**[RotateWebhookSigningKeyResponse](../../Models/Operations/RotateWebhookSigningKeyResponse.md)**

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

## SendTestEvent

endpoint.verification sends a signed challenge now and answers whether the endpoint echoed it within 5 seconds; an echo verifies the endpoint, including a disabled one. assessment.updated queues one signed event with test: true and a fixed example assessment in the endpoint's response mode, delivered and retried like any other event; the endpoint must be verified. Both use the endpoint's current signing key. Requires the webhooks scope.

### Example Usage: assessment_updated

<!-- UsageSnippet language="csharp" operationID="sendWebhookTestEvent" method="post" path="/v1/webhooks/{endpoint_id}/test" example="assessment_updated" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseClient(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

var res = await sdk.WebhookEndpoints.SendTestEventAsync(
    endpointId: "whe_01JA2ZD0E1F2G3H4J5K6M7N8P9",
    body: new WebhookTestRequest() {
        Event = WebhookTestRequestEvent.AssessmentUpdated,
    }
);

// handle response
```
### Example Usage: verification

<!-- UsageSnippet language="csharp" operationID="sendWebhookTestEvent" method="post" path="/v1/webhooks/{endpoint_id}/test" example="verification" -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseClient(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

var res = await sdk.WebhookEndpoints.SendTestEventAsync(
    endpointId: "whe_01JA2ZD0E1F2G3H4J5K6M7N8P9",
    body: new WebhookTestRequest() {
        Event = WebhookTestRequestEvent.EndpointVerification,
    }
);

// handle response
```

### Parameters

| Parameter                                                                                 | Type                                                                                      | Required                                                                                  | Description                                                                               | Example                                                                                   |
| ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- |
| `EndpointId`                                                                              | *string*                                                                                  | :heavy_check_mark:                                                                        | The endpoint's whe_ id. An id that is unknown, malformed or another tenant's answers 404. | whe_01JA2ZD0E1F2G3H4J5K6M7N8P9                                                            |
| `Body`                                                                                    | [WebhookTestRequest](../../Models/Shared/WebhookTestRequest.md)                           | :heavy_check_mark:                                                                        | N/A                                                                                       |                                                                                           |

### Response

**[SendWebhookTestEventResponse](../../Models/Operations/SendWebhookTestEventResponse.md)**

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