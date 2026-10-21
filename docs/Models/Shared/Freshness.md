# Freshness

Freshness class of the evidence behind a signal. unknown whenever no evidence was obtained.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Freshness.Fresh;

// Open enum: use .Of() to create instances from custom string values
var custom = Freshness.Of("custom_value");
```


## Values

| Name      | Value     |
| --------- | --------- |
| `Fresh`   | fresh     |
| `Stale`   | stale     |
| `Unknown` | unknown   |