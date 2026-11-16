# Topic

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Topic.Identity;

// Open enum: use .Of() to create instances from custom string values
var custom = Topic.Of("custom_value");
```


## Values

| Name            | Value           |
| --------------- | --------------- |
| `Identity`      | identity        |
| `Location`      | location        |
| `Movement`      | movement        |
| `Timeline`      | timeline        |
| `Interests`     | interests       |
| `Relationships` | relationships   |
| `Work`          | work            |
| `Education`     | education       |
| `Accounts`      | accounts        |
| `Credentials`   | credentials     |
| `Devices`       | devices         |
| `Absence`       | absence         |