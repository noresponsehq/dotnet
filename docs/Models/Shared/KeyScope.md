# KeyScope

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = KeyScope.Assess;

// Open enum: use .Of() to create instances from custom string values
var custom = KeyScope.Of("custom_value");
```


## Values

| Name            | Value           |
| --------------- | --------------- |
| `Assess`        | assess          |
| `History`       | history         |
| `Delete`        | delete          |
| `Webhooks`      | webhooks        |
| `Detailed`      | detailed        |
| `Integrations`  | integrations    |
| `Management`    | management      |
| `Audit`         | audit           |
| `Usage`         | usage           |
| `Investigate`   | investigate     |
| `Evidence`      | evidence        |
| `Exports`       | exports         |
| `AuditManage`   | audit_manage    |
| `Settings`      | settings        |
| `PolicyPublish` | policy_publish  |
| `Members`       | members         |
| `MembersManage` | members_manage  |