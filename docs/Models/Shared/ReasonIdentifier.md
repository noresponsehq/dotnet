# ReasonIdentifier

The submitted identifier or client context field a reason concerns; absent when it concerns the whole assessment. Client context is a claimed hint, not a device fingerprint.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ReasonIdentifier.Email;

// Open enum: use .Of() to create instances from custom string values
var custom = ReasonIdentifier.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Email`    | email      |
| `Username` | username   |
| `Phone`    | phone      |
| `Name`     | name       |
| `Ip`       | ip         |
| `Context`  | context    |