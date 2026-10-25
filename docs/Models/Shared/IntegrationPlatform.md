# IntegrationPlatform

The authentication provider that calls the endpoint.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = IntegrationPlatform.Workos;

// Open enum: use .Of() to create instances from custom string values
var custom = IntegrationPlatform.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Workos`   | workos     |
| `Auth0`    | auth0      |
| `Supabase` | supabase   |