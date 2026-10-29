# MemberDeniedAction

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = MemberDeniedAction.EvidenceRead;

// Open enum: use .Of() to create instances from custom string values
var custom = MemberDeniedAction.Of("custom_value");
```


## Values

| Name            | Value           |
| --------------- | --------------- |
| `EvidenceRead`  | evidence.read   |
| `ExportsRead`   | exports.read    |
| `PolicyPublish` | policy.publish  |