<!-- Start SDK Example Usage [usage] -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseClient(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

var res = await sdk.Assessments.CreateAsync(
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
        },
    },
    idempotencyKey: "<value>"
);

// handle response
```
<!-- End SDK Example Usage [usage] -->