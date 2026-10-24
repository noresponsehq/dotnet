# EnforcementState

The account's state in the workspace's identity provider just before the action: onboarding, provisional (created, not yet activated), active, restricted or rejected.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = EnforcementState.Onboarding;

// Open enum: use .Of() to create instances from custom string values
var custom = EnforcementState.Of("custom_value");
```


## Values

| Name          | Value         |
| ------------- | ------------- |
| `Onboarding`  | onboarding    |
| `Provisional` | provisional   |
| `Active`      | active        |
| `Restricted`  | restricted    |
| `Rejected`    | rejected      |