<!-- Start SDK Example Usage [usage] -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;
using System;

var sdk = new NoResponseClient(apiKey: Environment.GetEnvironmentVariable("NORESPONSE_API_KEY"));

var res = await sdk.Assessments.CreateAsync(
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
        },
    },
    idempotencyKey: "signup-acct_7f3a9c"
);

// handle response
```
<!-- End SDK Example Usage [usage] -->