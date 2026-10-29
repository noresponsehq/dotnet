# UsageCountFlow

assessments only.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = UsageCountFlow.AccountCreation;

// Open enum: use .Of() to create instances from custom string values
var custom = UsageCountFlow.Of("custom_value");
```


## Values

| Name              | Value             |
| ----------------- | ----------------- |
| `AccountCreation` | account_creation  |
| `Login`           | login             |
| `Purchase`        | purchase          |