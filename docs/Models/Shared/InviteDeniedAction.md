# InviteDeniedAction

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = InviteDeniedAction.EvidenceRead;

// Open enum: use .Of() to create instances from custom string values
var custom = InviteDeniedAction.Of("custom_value");
```


## Values

| Name            | Value           |
| --------------- | --------------- |
| `EvidenceRead`  | evidence.read   |
| `ExportsRead`   | exports.read    |
| `PolicyPublish` | policy.publish  |