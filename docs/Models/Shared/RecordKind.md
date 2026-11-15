# RecordKind

What sort of value a field holds, which decides how it is cleaned, compared and searched again. It comes from the source where the source declares it, as a connector does for the fields it states, else from the value itself where its shape is certain (an email, an IP, a URL, an international phone number), else from the answer committed for the source's column, else from the answer stored for it, else text. A value of any kind is returned. Each kind's description says what it holds and the form value takes.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = RecordKind.Email;

// Open enum: use .Of() to create instances from custom string values
var custom = RecordKind.Of("custom_value");
```


## Values

| Name             | Value            |
| ---------------- | ---------------- |
| `Email`          | email            |
| `Phone`          | phone            |
| `Username`       | username         |
| `AccountId`      | account_id       |
| `Url`            | url              |
| `Domain`         | domain           |
| `Image`          | image            |
| `PersonName`     | person_name      |
| `Gender`         | gender           |
| `Age`            | age              |
| `Language`       | language         |
| `Organisation`   | organisation     |
| `JobTitle`       | job_title        |
| `Country`        | country          |
| `Region`         | region           |
| `City`           | city             |
| `Address`        | address          |
| `Postcode`       | postcode         |
| `Coordinates`    | coordinates      |
| `Timezone`       | timezone         |
| `Date`           | date             |
| `DateTime`       | date_time        |
| `Duration`       | duration         |
| `Ip`             | ip               |
| `MacAddress`     | mac_address      |
| `DeviceId`       | device_id        |
| `UserAgent`      | user_agent       |
| `Password`       | password         |
| `Hash`           | hash             |
| `Salt`           | salt             |
| `Token`          | token            |
| `SecurityAnswer` | security_answer  |
| `CardNumber`     | card_number      |
| `BankAccount`    | bank_account     |
| `CryptoAddress`  | crypto_address   |
| `Amount`         | amount           |
| `IdNumber`       | id_number        |
| `Vehicle`        | vehicle          |
| `Label`          | label            |
| `LongText`       | long_text        |
| `Count`          | count            |
| `Score`          | score            |
| `Flag`           | flag             |
| `Structured`     | structured       |
| `Text`           | text             |