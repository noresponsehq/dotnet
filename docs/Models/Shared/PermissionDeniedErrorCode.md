# PermissionDeniedErrorCode

Machine codes an HTTP 403 answer can carry.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = PermissionDeniedErrorCode.WorkspaceForbidden;

// Open enum: use .Of() to create instances from custom string values
var custom = PermissionDeniedErrorCode.Of("custom_value");
```


## Values

| Name                 | Value                |
| -------------------- | -------------------- |
| `WorkspaceForbidden` | workspace_forbidden  |
| `RoleForbidden`      | role_forbidden       |
| `EmailUnverified`    | email_unverified     |
| `OnboardingInReview` | onboarding_in_review |
| `OnboardingRefused`  | onboarding_refused   |