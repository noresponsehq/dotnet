# NotFoundErrorCode

Machine codes an HTTP 404 answer can carry.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = NotFoundErrorCode.NotFound;

// Open enum: use .Of() to create instances from custom string values
var custom = NotFoundErrorCode.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `NotFound` | not_found  |