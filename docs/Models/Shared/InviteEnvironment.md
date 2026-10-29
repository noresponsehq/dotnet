# InviteEnvironment

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = InviteEnvironment.Live;

// Open enum: use .Of() to create instances from custom string values
var custom = InviteEnvironment.Of("custom_value");
```


## Values

| Name   | Value  |
| ------ | ------ |
| `Live` | live   |
| `Test` | test   |