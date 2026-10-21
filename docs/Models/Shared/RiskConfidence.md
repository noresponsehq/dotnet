# RiskConfidence

How much of the planned evidence answered; it says nothing about risk.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = RiskConfidence.Low;

// Open enum: use .Of() to create instances from custom string values
var custom = RiskConfidence.Of("custom_value");
```


## Values

| Name     | Value    |
| -------- | -------- |
| `Low`    | low      |
| `Medium` | medium   |
| `High`   | high     |