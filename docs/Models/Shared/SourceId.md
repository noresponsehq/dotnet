# SourceId

A NoResponse source: github (GitHub accounts), phone (the phone lookup), breach (breach exposure), rdap (the email domain's registration), mx (the email domain's mail servers) or ipintel (IP intelligence).

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = SourceId.Github;

// Open enum: use .Of() to create instances from custom string values
var custom = SourceId.Of("custom_value");
```


## Values

| Name      | Value     |
| --------- | --------- |
| `Github`  | github    |
| `Phone`   | phone     |
| `Breach`  | breach    |
| `Rdap`    | rdap      |
| `Mx`      | mx        |
| `Ipintel` | ipintel   |