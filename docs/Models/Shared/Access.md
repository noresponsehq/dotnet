# Access

public profiles were read; a private or locked one exists but could not be read.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Access.Public;

// Open enum: use .Of() to create instances from custom string values
var custom = Access.Of("custom_value");
```


## Values

| Name      | Value     |
| --------- | --------- |
| `Public`  | public    |
| `Private` | private   |
| `Locked`  | locked    |