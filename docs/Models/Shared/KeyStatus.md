# KeyStatus

active: the key authenticates. expired: a rotation's overlap window has ended (expires_at). revoked: the key was revoked (revoked_at) and answers 401.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = KeyStatus.Active;

// Open enum: use .Of() to create instances from custom string values
var custom = KeyStatus.Of("custom_value");
```


## Values

| Name      | Value     |
| --------- | --------- |
| `Active`  | active    |
| `Expired` | expired   |
| `Revoked` | revoked   |