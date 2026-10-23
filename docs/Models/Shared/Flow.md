# Flow

The event an assessment is made for: account_creation (a user creates an account), login (a user returns to an existing account) or purchase (a payment or a credit grant). The account's risk is reported with the decision for this flow.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Flow.AccountCreation;

// Open enum: use .Of() to create instances from custom string values
var custom = Flow.Of("custom_value");
```


## Values

| Name              | Value             |
| ----------------- | ----------------- |
| `AccountCreation` | account_creation  |
| `Login`           | login             |
| `Purchase`        | purchase          |