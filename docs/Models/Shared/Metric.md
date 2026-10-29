# Metric

assessments: assessments admitted. webhook_deliveries: events sent to endpoints, one per event per endpoint. key_usage_events: key-usage events accepted.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Metric.Assessments;

// Open enum: use .Of() to create instances from custom string values
var custom = Metric.Of("custom_value");
```


## Values

| Name                | Value               |
| ------------------- | ------------------- |
| `Assessments`       | assessments         |
| `WebhookDeliveries` | webhook_deliveries  |
| `KeyUsageEvents`    | key_usage_events    |