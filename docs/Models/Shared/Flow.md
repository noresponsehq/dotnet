# Flow

The provider flow the assessment gates. Use the top-level flow instead; this member is ignored when the top-level flow is present. Absent means signup.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Flow.Signup;
```


## Values

| Name            | Value           |
| --------------- | --------------- |
| `Signup`        | signup          |
| `Login`         | login           |
| `Recovery`      | recovery        |
| `ProfileChange` | profile_change  |