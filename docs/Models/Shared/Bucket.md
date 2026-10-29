# Bucket

The period each count covers: an hour, or a day or a week (from Monday) in time_zone. Hour buckets are whole UTC hours in any time zone.

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