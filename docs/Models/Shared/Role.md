# Role

A built-in capability set. Specialist roles are independent; effective access is returned by GET /v1/workspaces/{workspace_id}/capabilities.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Role.Owner;

// Open enum: use .Of() to create instances from custom string values
var custom = Role.Of("custom_value");
```


## Values

| Name           | Value          |
| -------------- | -------------- |
| `Owner`        | owner          |
| `Admin`        | admin          |
| `Developer`    | developer      |
| `Viewer`       | viewer         |
| `Investigator` | investigator   |
| `Auditor`      | auditor        |
| `Billing`      | billing        |