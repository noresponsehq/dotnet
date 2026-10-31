# Origin

api (the default) searches the assessments your integration sent. lookup searches the assessments your team ran by hand on the console's Lookup page. The API sets an assessment's origin from the credential that sent it. A lookup is not in any other list, count, webhook event or account.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Origin.Api;
```


## Values

| Name     | Value    |
| -------- | -------- |
| `Api`    | api      |
| `Lookup` | lookup   |