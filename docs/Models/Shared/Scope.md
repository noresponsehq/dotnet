# Scope

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Scope.Assess;

// Open enum: use .Of() to create instances from custom string values
var custom = Scope.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Assess`   | assess     |
| `History`  | history    |
| `Webhooks` | webhooks   |
| `Detailed` | detailed   |