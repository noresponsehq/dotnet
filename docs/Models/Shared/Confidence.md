# Confidence

Ordinal confidence in the recommendation under the current policy: low, medium, high or unavailable.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Confidence.Low;

// Open enum: use .Of() to create instances from custom string values
var custom = Confidence.Of("custom_value");
```


## Values

| Name          | Value         |
| ------------- | ------------- |
| `Low`         | low           |
| `Medium`      | medium        |
| `High`        | high          |
| `Unavailable` | unavailable   |