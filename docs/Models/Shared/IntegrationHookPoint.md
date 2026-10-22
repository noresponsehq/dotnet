# IntegrationHookPoint

pre_registration runs before the user exists; post_login runs after a successful sign-in.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = IntegrationHookPoint.PreRegistration;

// Open enum: use .Of() to create instances from custom string values
var custom = IntegrationHookPoint.Of("custom_value");
```


## Values

| Name              | Value             |
| ----------------- | ----------------- |
| `PreRegistration` | pre_registration  |
| `PostLogin`       | post_login        |