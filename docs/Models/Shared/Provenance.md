# Provenance

Customer-submitted input. It does not mean the customer or NoResponse verified ownership.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Provenance.Submitted;

// Open enum: use .Of() to create instances from custom string values
var custom = Provenance.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Submitted` | submitted   |