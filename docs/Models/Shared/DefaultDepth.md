# DefaultDepth

The depth a request for this flow that names none is served at. Defaults: full for account_creation and purchase, quick for login.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = DefaultDepth.Quick;

// Open enum: use .Of() to create instances from custom string values
var custom = DefaultDepth.Of("custom_value");
```


## Values

| Name    | Value   |
| ------- | ------- |
| `Quick` | quick   |
| `Full`  | full    |