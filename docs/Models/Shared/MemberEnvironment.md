# MemberEnvironment

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = MemberEnvironment.Live;

// Open enum: use .Of() to create instances from custom string values
var custom = MemberEnvironment.Of("custom_value");
```


## Values

| Name   | Value  |
| ------ | ------ |
| `Live` | live   |
| `Test` | test   |