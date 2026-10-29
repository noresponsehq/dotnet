# OwnerType

The credential namespace of the creator. An external user ID equal to an API-key label does not grant ownership of that key's private view.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = OwnerType.User;

// Open enum: use .Of() to create instances from custom string values
var custom = OwnerType.Of("custom_value");
```


## Values

| Name     | Value    |
| -------- | -------- |
| `User`   | user     |
| `ApiKey` | api_key  |