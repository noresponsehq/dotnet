# ListRequestDiagnosticsRequest


## Fields

| Field                                             | Type                                              | Required                                          | Description                                       | Example                                           |
| ------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- |
| `From`                                            | *string*                                          | :heavy_check_mark:                                | Inclusive start of a range of at most seven days. | 2026-10-21T00:00:00Z                              |
| `To`                                              | *string*                                          | :heavy_check_mark:                                | Exclusive end of the range.                       | 2026-10-22T00:00:00Z                              |
| `RequestId`                                       | *string*                                          | :heavy_minus_sign:                                | Optional filter or paging parameter.              |                                                   |
| `Route`                                           | *string*                                          | :heavy_minus_sign:                                | Optional filter or paging parameter.              |                                                   |
| `Status`                                          | *long*                                            | :heavy_minus_sign:                                | Optional filter or paging parameter.              |                                                   |
| `Limit`                                           | *long*                                            | :heavy_minus_sign:                                | Optional filter or paging parameter.              |                                                   |
| `Cursor`                                          | *string*                                          | :heavy_minus_sign:                                | Optional filter or paging parameter.              |                                                   |