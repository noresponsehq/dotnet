# KeyRotationScope

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = KeyRotationScope.Assess;

// Open enum: use .Of() to create instances from custom string values
var custom = KeyRotationScope.Of("custom_value");
```


## Values

| Name           | Value          |
| -------------- | -------------- |
| `Assess`       | assess         |
| `History`      | history        |
| `Delete`       | delete         |
| `Webhooks`     | webhooks       |
| `Detailed`     | detailed       |
| `Integrations` | integrations   |
| `Management`   | management     |