# IpSummaryType

The address's network type.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = IpSummaryType.Residential;

// Open enum: use .Of() to create instances from custom string values
var custom = IpSummaryType.Of("custom_value");
```


## Values

| Name          | Value         |
| ------------- | ------------- |
| `Residential` | residential   |
| `Hosting`     | hosting       |
| `Vpn`         | vpn           |
| `Proxy`       | proxy         |
| `Tor`         | tor           |
| `Mobile`      | mobile        |
| `Unknown`     | unknown       |