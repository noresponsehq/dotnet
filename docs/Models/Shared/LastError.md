# LastError

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = LastError.ScreeningCapExceeded;

// Open enum: use .Of() to create instances from custom string values
var custom = LastError.Of("custom_value");
```


## Values

| Name                   | Value                  |
| ---------------------- | ---------------------- |
| `ScreeningCapExceeded` | screening_cap_exceeded |
| `ScreeningUnavailable` | screening_unavailable  |
| `PersonNotFound`       | person_not_found       |
| `IdempotencyKeyReused` | idempotency_key_reused |