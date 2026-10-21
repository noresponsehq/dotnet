# RateLimitException

HTTP 429 problem document.


## Fields

| Field                                                                   | Type                                                                    | Required                                                                | Description                                                             |
| ----------------------------------------------------------------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------- |
| `Type`                                                                  | *string*                                                                | :heavy_check_mark:                                                      | N/A                                                                     |
| `Title`                                                                 | *string*                                                                | :heavy_check_mark:                                                      | N/A                                                                     |
| `Status`                                                                | *long*                                                                  | :heavy_check_mark:                                                      | N/A                                                                     |
| `Detail`                                                                | *string*                                                                | :heavy_check_mark:                                                      | N/A                                                                     |
| `Instance`                                                              | *string*                                                                | :heavy_minus_sign:                                                      | N/A                                                                     |
| `Code`                                                                  | [RateLimitErrorCode](../../Models/Shared/RateLimitErrorCode.md)         | :heavy_check_mark:                                                      | Machine codes an HTTP 429 answer can carry.                             |
| `RequestId`                                                             | *string*                                                                | :heavy_check_mark:                                                      | N/A                                                                     |
| `InvalidParams`                                                         | List<[ProblemInvalidParam](../../Models/Shared/ProblemInvalidParam.md)> | :heavy_minus_sign:                                                      | N/A                                                                     |