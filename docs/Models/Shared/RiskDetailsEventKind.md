# RiskDetailsEventKind

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = RiskDetailsEventKind.Created;

// Open enum: use .Of() to create instances from custom string values
var custom = RiskDetailsEventKind.Of("custom_value");
```


## Values

| Name         | Value        |
| ------------ | ------------ |
| `Created`    | created      |
| `LastActive` | last_active  |
| `Breached`   | breached     |