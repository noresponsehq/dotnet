# EnforcementStatus

applied when the identity provider accepted the action, failed when it refused it or could not be reached. Only applied counts as enforced.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = EnforcementStatus.Applied;

// Open enum: use .Of() to create instances from custom string values
var custom = EnforcementStatus.Of("custom_value");
```


## Values

| Name      | Value     |
| --------- | --------- |
| `Applied` | applied   |
| `Failed`  | failed    |