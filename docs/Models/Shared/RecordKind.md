# RecordKind

What sort of value a field holds, which decides how it is cleaned, compared and searched again. It comes from the value itself where its shape is certain (an email, an IP, a URL, an international phone number), else from the answer stored for the source's column, else text. A value of any kind is returned.

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