# ListEntryKind

email matches one address, whatever the case of its local part. email_domain matches every address of the registrable domain and of its subdomains.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ListEntryKind.Email;

// Open enum: use .Of() to create instances from custom string values
var custom = ListEntryKind.Of("custom_value");
```


## Values

| Name          | Value         |
| ------------- | ------------- |
| `Email`       | email         |
| `EmailDomain` | email_domain  |