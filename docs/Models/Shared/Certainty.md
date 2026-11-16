# Certainty

confirmed: a linked record or the searched identifier states it directly. likely: a strong inference. possible: a weak inference.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Certainty.Confirmed;

// Open enum: use .Of() to create instances from custom string values
var custom = Certainty.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Confirmed` | confirmed   |
| `Likely`    | likely      |
| `Possible`  | possible    |