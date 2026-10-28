# Bucket

The period each count covers, in UTC; a week starts on Monday.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Bucket.Hour;

// Open enum: use .Of() to create instances from custom string values
var custom = Bucket.Of("custom_value");
```


## Values

| Name   | Value  |
| ------ | ------ |
| `Hour` | hour   |
| `Day`  | day    |
| `Week` | week   |