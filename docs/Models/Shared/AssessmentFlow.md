# AssessmentFlow

The customer event an assessment is made for: signup (a new account), login (a returning sign-in) or purchase (a payment or credit grant). The account's risk is reported with the decision for this flow.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = AssessmentFlow.Signup;
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Signup`   | signup     |
| `Login`    | login      |
| `Purchase` | purchase   |