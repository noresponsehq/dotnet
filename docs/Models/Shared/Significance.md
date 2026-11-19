# Significance

One rule ranks every change: identity changes (accounts, breaches, identifiers) are high, profile fields are medium, new posts are low.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Significance.High;

// Open enum: use .Of() to create instances from custom string values
var custom = Significance.Of("custom_value");
```


## Values

| Name     | Value    |
| -------- | -------- |
| `High`   | high     |
| `Medium` | medium   |
| `Low`    | low      |