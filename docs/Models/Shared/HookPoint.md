# HookPoint

The provider event a provider_hook context was sent for; absent for any other transport.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = HookPoint.PreRegistration;

// Open enum: use .Of() to create instances from custom string values
var custom = HookPoint.Of("custom_value");
```


## Values

| Name               | Value              |
| ------------------ | ------------------ |
| `PreRegistration`  | pre_registration   |
| `PostRegistration` | post_registration  |
| `PreLogin`         | pre_login          |
| `PostLogin`        | post_login         |
| `TokenIssuance`    | token_issuance     |