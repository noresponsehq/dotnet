# Outcome

What the provider later learned about the assessed account. confirmed_abuse, account_takeover and chargeback report abuse; false_positive and legitimate withdraw an earlier abuse report of this workspace for the assessment; unknown is recorded only. Outcomes are append-only and the latest one recorded for an assessment wins.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Outcome.ConfirmedAbuse;

// Open enum: use .Of() to create instances from custom string values
var custom = Outcome.Of("custom_value");
```


## Values

| Name              | Value             |
| ----------------- | ----------------- |
| `ConfirmedAbuse`  | confirmed_abuse   |
| `AccountTakeover` | account_takeover  |
| `Chargeback`      | chargeback        |
| `FalsePositive`   | false_positive    |
| `Legitimate`      | legitimate        |
| `Unknown`         | unknown           |