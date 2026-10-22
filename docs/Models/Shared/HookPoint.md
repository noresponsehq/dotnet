# HookPoint

The provider event a provider_hook context was sent for. pre_registration runs before the user exists, so subject and the request's account_id may be absent there. Sent only with transport provider_hook.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = HookPoint.PreRegistration;
```


## Values

| Name               | Value              |
| ------------------ | ------------------ |
| `PreRegistration`  | pre_registration   |
| `PostRegistration` | post_registration  |
| `PreLogin`         | pre_login          |
| `PostLogin`        | post_login         |
| `TokenIssuance`    | token_issuance     |