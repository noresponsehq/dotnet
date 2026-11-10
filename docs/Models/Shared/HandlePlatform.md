# HandlePlatform

A platform a screening can cover. synthetic is the deterministic connector's own platform, for exercising the product end to end with no network.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = HandlePlatform.Instagram;

// Open enum: use .Of() to create instances from custom string values
var custom = HandlePlatform.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Instagram` | instagram   |
| `X`         | x           |
| `Bluesky`   | bluesky     |
| `Youtube`   | youtube     |
| `Tiktok`    | tiktok      |
| `Facebook`  | facebook    |
| `Threads`   | threads     |
| `Reddit`    | reddit      |
| `Telegram`  | telegram    |
| `Linkedin`  | linkedin    |
| `Synthetic` | synthetic   |