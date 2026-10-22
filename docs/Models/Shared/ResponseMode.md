# ResponseMode

standard returns the consolidated risk response; detailed adds merged provider-neutral facts when the key has detailed scope. There is no raw mode.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ResponseMode.Standard;

// Open enum: use .Of() to create instances from custom string values
var custom = ResponseMode.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Standard` | standard   |
| `Detailed` | detailed   |