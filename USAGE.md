<!-- Start SDK Example Usage [usage] -->
```csharp
using NoResponse;
using NoResponse.Models.Shared;

var sdk = new NoResponseSDK(apiKey: "<YOUR_BEARER_TOKEN_HERE>");

var res = await sdk.Assessments.CreateAsync(
    idempotencyKey: "<value>",
    body: new AssessmentRequest() {
        AccountId = "acct_7f3a9c",
        Identifiers = new AssessmentRequestIdentifiers() {
            Email = "new.user@example.com",
        },
    }
);

// handle response
```
<!-- End SDK Example Usage [usage] -->