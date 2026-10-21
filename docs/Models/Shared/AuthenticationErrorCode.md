# AuthenticationErrorCode

Machine codes an HTTP 401 answer can carry.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = AuthenticationErrorCode.Unauthorized;

// Open enum: use .Of() to create instances from custom string values
var custom = AuthenticationErrorCode.Of("custom_value");
```


## Values

| Name           | Value          |
| -------------- | -------------- |
| `Unauthorized` | unauthorized   |