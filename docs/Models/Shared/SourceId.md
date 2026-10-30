# SourceId

A NoResponse source: github (GitHub accounts), phone (the phone lookup), breach (breach exposure), rdap (the email domain's registration), mx (the email domain's mail servers), ipintel (IP intelligence) or an email account source that searches one platform for an account by email address (adobe, nvidia, wix, life360, zapier).

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
| `Adobe`   | adobe     |
| `Nvidia`  | nvidia    |
| `Wix`     | wix       |
| `Life360` | life360   |
| `Zapier`  | zapier    |