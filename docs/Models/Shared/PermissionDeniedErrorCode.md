# PermissionDeniedErrorCode

Machine codes an HTTP 403 answer can carry.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = PermissionDeniedErrorCode.TenantForbidden;

// Open enum: use .Of() to create instances from custom string values
var custom = PermissionDeniedErrorCode.Of("custom_value");
```


## Values

| Name              | Value             |
| ----------------- | ----------------- |
| `TenantForbidden` | tenant_forbidden  |