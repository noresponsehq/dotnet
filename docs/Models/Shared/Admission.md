# Admission

immediate (the default): an assessment of this flow is decided from the evidence its answer has, and later evidence may change the decision. held: while the account's current assessment of this flow has not completed, the account's decision for the flow is held, and the final decision arrives as account.risk_changed when the investigation completes or hold_seconds pass, whichever is first. Each flow has its own admission.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Admission.Immediate;

// Open enum: use .Of() to create instances from custom string values
var custom = Admission.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Immediate` | immediate   |
| `Held`      | held        |