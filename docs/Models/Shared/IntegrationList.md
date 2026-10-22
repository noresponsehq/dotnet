# IntegrationList

GET /v1/integrations: every integration of the tenant, oldest first, at most 10. No secret is ever listed.


## Fields

| Field                                                   | Type                                                    | Required                                                | Description                                             |
| ------------------------------------------------------- | ------------------------------------------------------- | ------------------------------------------------------- | ------------------------------------------------------- |
| `Items`                                                 | List<[Integration](../../Models/Shared/Integration.md)> | :heavy_check_mark:                                      | N/A                                                     |