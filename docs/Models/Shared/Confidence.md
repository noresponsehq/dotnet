# Confidence

Evidence coverage: how much of the planned source evidence gave a definite answer. This is not confidence that an identity is genuine, agreement between identifiers, a risk score or a probability. unavailable means no usable evidence answered; a terminal uncovered assessment can have this value and recommend review.

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