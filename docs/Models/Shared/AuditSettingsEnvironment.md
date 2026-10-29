# AuditSettingsEnvironment

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = AuditSettingsEnvironment.Live;

// Open enum: use .Of() to create instances from custom string values
var custom = AuditSettingsEnvironment.Of("custom_value");
```


## Values

| Name   | Value  |
| ------ | ------ |
| `Live` | live   |
| `Test` | test   |