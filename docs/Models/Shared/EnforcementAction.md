# EnforcementAction

What the workspace did in its identity provider: revoke_sessions (signed the account out everywhere), restrict (limited what the account can do), unrestrict (lifted an earlier restriction), fail_onboarding (stopped an account that was still onboarding), reject_activation (refused to activate a provisional account), activate (let a held onboarding or provisional account in) or none (decided to take no action).

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = EnforcementAction.RevokeSessions;

// Open enum: use .Of() to create instances from custom string values
var custom = EnforcementAction.Of("custom_value");
```


## Values

| Name               | Value              |
| ------------------ | ------------------ |
| `RevokeSessions`   | revoke_sessions    |
| `Restrict`         | restrict           |
| `Unrestrict`       | unrestrict         |
| `FailOnboarding`   | fail_onboarding    |
| `RejectActivation` | reject_activation  |
| `Activate`         | activate           |
| `None`             | none               |