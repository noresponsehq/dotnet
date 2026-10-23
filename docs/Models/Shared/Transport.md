# Transport

Where the context was derived: backend (also when the request named no transport), browser, or provider_hook for a hosted provider hook.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Transport.Backend;

// Open enum: use .Of() to create instances from custom string values
var custom = Transport.Of("custom_value");
```


## Values

| Name           | Value          |
| -------------- | -------------- |
| `Backend`      | backend        |
| `Browser`      | browser        |
| `ProviderHook` | provider_hook  |