# WrittenProfileStatus

queued: the profile is being written. written: summary, statements and leads hold it. failed: it could not be written; error_code says why and no content is returned.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = WrittenProfileStatus.Queued;

// Open enum: use .Of() to create instances from custom string values
var custom = WrittenProfileStatus.Of("custom_value");
```


## Values

| Name      | Value     |
| --------- | --------- |
| `Queued`  | queued    |
| `Written` | written   |
| `Failed`  | failed    |