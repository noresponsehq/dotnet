# ScreeningNotAnalysedReason

Why an item read was not analysed. video: a video, whose caption alone is read. image_unavailable: the image could not be fetched from the platform. image_unsupported: not a JPEG, PNG, GIF or WebP image, or too large to send. image_limit: past the most images one person's analysis reads. refused: the model declined to read it. unfinished: the model cut its batch short or answered outside the schema, or the request was rejected. unverified: a finding cited it with words it does not contain or an image it did not describe. unspecified: the analyser gave no reason.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ScreeningNotAnalysedReason.Video;

// Open enum: use .Of() to create instances from custom string values
var custom = ScreeningNotAnalysedReason.Of("custom_value");
```


## Values

| Name               | Value              |
| ------------------ | ------------------ |
| `Video`            | video              |
| `ImageUnavailable` | image_unavailable  |
| `ImageUnsupported` | image_unsupported  |
| `ImageLimit`       | image_limit        |
| `Refused`          | refused            |
| `Unfinished`       | unfinished         |
| `Unverified`       | unverified         |
| `Unspecified`      | unspecified        |