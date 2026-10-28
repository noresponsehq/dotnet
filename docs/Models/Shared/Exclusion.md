# Exclusion

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Exclusion.ExpiredOrErasedRecords;

// Open enum: use .Of() to create instances from custom string values
var custom = Exclusion.Of("custom_value");
```


## Values

| Name                             | Value                            |
| -------------------------------- | -------------------------------- |
| `ExpiredOrErasedRecords`         | expired_or_erased_records        |
| `SecretsAndCredentialDigests`    | secrets_and_credential_digests   |
| `PrivateSourceAndWorkPayloads`   | private_source_and_work_payloads |
| `CrossCustomerData`              | cross_customer_data              |
| `LegacyOutcomeNotes`             | legacy_outcome_notes             |
| `UncapturedHistoricalDecisions`  | uncaptured_historical_decisions  |