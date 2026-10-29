# Conclusion

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Conclusion.Benign;

// Open enum: use .Of() to create instances from custom string values
var custom = Conclusion.Of("custom_value");
```


## Values

| Name           | Value          |
| -------------- | -------------- |
| `Benign`       | benign         |
| `Abuse`        | abuse          |
| `Inconclusive` | inconclusive   |