# ListChangeState

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ListChangeState.Propagating;

// Open enum: use .Of() to create instances from custom string values
var custom = ListChangeState.Of("custom_value");
```


## Values

| Name          | Value         |
| ------------- | ------------- |
| `Propagating` | propagating   |
| `Completed`   | completed     |