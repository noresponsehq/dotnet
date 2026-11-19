# ScreeningSpendLineRole

What the implementation did. analyser appears only on spend recorded before the engine stopped analysing posts.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ScreeningSpendLineRole.Resolver;

// Open enum: use .Of() to create instances from custom string values
var custom = ScreeningSpendLineRole.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Resolver`  | resolver    |
| `Collector` | collector   |
| `Analyser`  | analyser    |