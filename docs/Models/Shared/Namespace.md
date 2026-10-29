# Namespace

Required for username: the username submitted by this customer in this workspace/environment. It does not name or join a source platform. Omit for email and phone.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Namespace.Customer;

// Open enum: use .Of() to create instances from custom string values
var custom = Namespace.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Customer` | customer   |