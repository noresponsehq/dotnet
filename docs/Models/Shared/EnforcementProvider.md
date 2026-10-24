# EnforcementProvider

The identity provider the action ran in; custom for the workspace's own user store.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = EnforcementProvider.Clerk;

// Open enum: use .Of() to create instances from custom string values
var custom = EnforcementProvider.Of("custom_value");
```


## Values

| Name     | Value    |
| -------- | -------- |
| `Clerk`  | clerk    |
| `Workos` | workos   |
| `Auth0`  | auth0    |
| `Custom` | custom   |