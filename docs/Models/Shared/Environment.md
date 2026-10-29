# Environment

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Environment.Live;

// Open enum: use .Of() to create instances from custom string values
var custom = Environment.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Live`      | live        |
| `Test`      | test        |
| `Workspace` | workspace   |