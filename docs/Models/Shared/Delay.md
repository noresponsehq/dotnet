# Delay

How long after the assessment's creation the change was made.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Delay.Under10s;

// Open enum: use .Of() to create instances from custom string values
var custom = Delay.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Under10s` | under_10s  |
| `Under1m`  | under_1m   |
| `Under10m` | under_10m  |
| `Under1h`  | under_1h   |
| `Under1d`  | under_1d   |
| `Over1d`   | over_1d    |