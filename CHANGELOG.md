# Changelog

## 8.0.2

No changes detected.

## 8.0.1

No changes detected.

## 8.0.0

* `Sdk.Assessments.Create()`:  `response` **Changed** (Breaking ⚠️)
* `Sdk.Assessments.Get()`:  `response` **Changed** (Breaking ⚠️)
* `Sdk.Assessments.Query()`:  `response.items[].Risk.Reasons[].Weight` **Removed** (Breaking ⚠️)
* `Sdk.Accounts.ListChanged()`:  `response.items[].Risk.Reasons[].Weight` **Removed** (Breaking ⚠️)
* `Sdk.Accounts.Get()`:  `response.risk.Reasons[].Weight` **Removed** (Breaking ⚠️)
* `Sdk.Accounts.ListAssessments()`:  `response.items[].Risk.Reasons[].Weight` **Removed** (Breaking ⚠️)

## 7.2.2

No changes detected.

## 7.2.1

No changes detected.

## 7.2.0

No changes detected.

## 7.1.8

* `Sdk.Assessments.Create()`:  `response` **Changed**
* `Sdk.Assessments.Get()`:  `response` **Changed**
* `Sdk.Assessments.Query()`:  `response.items[].Risk` **Changed**
* `Sdk.Accounts.ListChanged()`:  `response.items[].Risk.Reasons[]` **Changed**
* `Sdk.Accounts.Get()`:  `response.risk.Reasons[]` **Changed**
* `Sdk.Accounts.ListAssessments()`:  `response.items[].Risk` **Changed**

## 7.1.7

* `Sdk.Assessments.Create()`:  `response.account.Risk.Reasons[].Code` **Changed**
* `Sdk.Assessments.Get()`:  `response.account.Risk.Reasons[].Code` **Changed**
* `Sdk.Assessments.Query()`:  `response.items[].Risk.Reasons[].Code` **Changed**
* `Sdk.Accounts.ListChanged()`:  `response.items[].Risk.Reasons[].Code` **Changed**
* `Sdk.Accounts.Get()`:  `response.risk.Reasons[].Code` **Changed**
* `Sdk.Accounts.ListAssessments()`:  `response.items[].Risk.Reasons[].Code` **Changed**

## 7.1.6

* `Sdk.Integrations.Create()`: 
  *  `request.Platform.Enum(supabase)` **Added**
  *  `response.platform.Enum(supabase)` **Added**
* `Sdk.Integrations.List()`:  `response.items[].Platform.Enum(supabase)` **Added**
* `Sdk.Integrations.Rotate()`:  `response.platform.Enum(supabase)` **Added**

## 7.1.5

* `Sdk.Assessments.Create()`:  `response.account.Risk.Reasons[].Code` **Changed**
* `Sdk.Assessments.Get()`:  `response.account.Risk.Reasons[].Code` **Changed**
* `Sdk.Assessments.Query()`:  `response.items[].Risk.Reasons[].Code` **Changed**
* `Sdk.Accounts.ListChanged()`:  `response.items[].Risk.Reasons[].Code` **Changed**
* `Sdk.Accounts.Get()`:  `response.risk.Reasons[].Code` **Changed**
* `Sdk.Accounts.ListAssessments()`:  `response.items[].Risk.Reasons[].Code` **Changed**

## 7.1.4

No changes detected.

## 7.1.3

No changes detected.

## 7.1.2

No changes detected.

## 7.1.1

* `Sdk.Accounts.RecordEnforcement()`: 
  *  `request.body.Action.Enum(activate)` **Added**
  *  `response.action.Enum(activate)` **Added**

## 7.1.0

* `Sdk.Keys.Create()`: **Added**
* `Sdk.Keys.List()`: **Added**
* `Sdk.Keys.Revoke()`: **Added**
* `Sdk.Settings.Get()`: **Added**
* `Sdk.Settings.Update()`: **Added**
* `Sdk.Assessments.Create()`:  `error.code` **Changed**
* `Sdk.Assessments.Get()`:  `error.code` **Changed**
* `Sdk.Assessments.Delete()`:  `error.code` **Changed**
* `Sdk.Assessments.RecordOutcome()`:  `error.code` **Changed**
* `Sdk.Assessments.Query()`:  `error.code` **Changed**
* `Sdk.Accounts.ListChanged()`:  `error.code` **Changed**
* `Sdk.Accounts.Get()`:  `error.code` **Changed**
* `Sdk.Accounts.Delete()`:  `error.code` **Changed**
* `Sdk.Accounts.ListAssessments()`:  `error.code` **Changed**
* `Sdk.Accounts.RecordEnforcement()`:  `error.code` **Changed**
* `Sdk.KeyUsage.Record()`:  `error.code` **Changed**
* `Sdk.Keys.Rotate()`: 
  *  `response.scopes[].Enum(management)` **Added**
  *  `error.code` **Changed**
* `Sdk.Integrations.Create()`:  `error.code` **Changed**
* `Sdk.Integrations.List()`:  `error.code` **Changed**
* `Sdk.Integrations.Delete()`:  `error.code` **Changed**
* `Sdk.Integrations.Rotate()`:  `error.code` **Changed**
* `Sdk.WebhookEndpoints.List()`:  `error.code` **Changed**
* `Sdk.WebhookEndpoints.Create()`:  `error.code` **Changed**
* `Sdk.WebhookEndpoints.Delete()`:  `error.code` **Changed**
* `Sdk.WebhookEndpoints.RotateSigningKey()`:  `error.code` **Changed**
* `Sdk.WebhookEndpoints.SendTestEvent()`:  `error.code` **Changed**
* `Sdk.WebhookEndpoints.ListDeliveries()`:  `error.code` **Changed**
* `Sdk.WebhookEndpoints.ReplayDelivery()`:  `error.code` **Changed**

## 7.0.5

No changes detected.

## 7.0.4

* `Sdk.Assessments.Create()`:  `response.account.Risk.Reasons[].Code` **Changed**
* `Sdk.Assessments.Get()`:  `response.account.Risk.Reasons[].Code` **Changed**
* `Sdk.Assessments.Query()`:  `response.items[].Risk.Reasons[].Code` **Changed**
* `Sdk.Accounts.ListChanged()`:  `response.items[].Risk.Reasons[].Code` **Changed**
* `Sdk.Accounts.Get()`:  `response.risk.Reasons[].Code` **Changed**
* `Sdk.Accounts.ListAssessments()`:  `response.items[].Risk.Reasons[].Code` **Changed**

## 7.0.3

* `Sdk.Assessments.Create()`:  `response.detail.IncompleteSources[].Lane.Enum(domain)` **Added**
* `Sdk.Assessments.Get()`:  `response.detail.IncompleteSources[].Lane.Enum(domain)` **Added**
* `Sdk.Assessments.Query()`:  `response.items[].IncompleteSources[].Lane.Enum(domain)` **Added**
* `Sdk.Accounts.ListAssessments()`:  `response.items[].IncompleteSources[].Lane.Enum(domain)` **Added**

## 7.0.2

No changes detected.

## 7.0.1

* `Sdk.Assessments.Create()`:  `response.account.Risk.Decision.Enum(held)` **Added**
* `Sdk.Assessments.Get()`:  `response.account.Risk.Decision.Enum(held)` **Added**
* `Sdk.Accounts.ListChanged()`:  `response.items[].Risk.Decision.Enum(held)` **Added**
* `Sdk.Accounts.Get()`:  `response.risk.Decision.Enum(held)` **Added**

## 7.0.0

No changes detected.

## 6.0.1

* `Sdk.Accounts.ListChanged()`: **Added**
* `Sdk.Accounts.RecordEnforcement()`: **Added**
* `Sdk.Assessments.Create()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.Assessments.Get()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.Assessments.Delete()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.Assessments.RecordOutcome()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.Assessments.Query()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.Accounts.Get()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.Accounts.Delete()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.Accounts.ListAssessments()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.KeyUsage.Record()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.Keys.Rotate()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.Integrations.Create()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.Integrations.List()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.Integrations.Delete()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.Integrations.Rotate()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.WebhookEndpoints.List()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.WebhookEndpoints.Create()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.WebhookEndpoints.Delete()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.WebhookEndpoints.RotateSigningKey()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.WebhookEndpoints.SendTestEvent()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.WebhookEndpoints.ListDeliveries()`:  `error.code.Enum(riskVersionAhead)` **Added**
* `Sdk.WebhookEndpoints.ReplayDelivery()`:  `error.code.Enum(riskVersionAhead)` **Added**

## 6.0.0

No changes detected.

## 5.1.1

* `Sdk.Assessments.Create()`:  `response.account.RiskVersion` **Added**
* `Sdk.Assessments.Get()`:  `response.account.RiskVersion` **Added**
* `Sdk.Accounts.Get()`:  `response.riskVersion` **Added**

## 5.1.0

* `Sdk.Keys.Rotate()`:  `response.scopes[].Enum(delete)` **Added**

## 5.0.1

No changes detected.

## 5.0.0

* `Sdk.Assessments.Create()`: 
  *  `request.body.Flow` **Changed** (Breaking ⚠️)
  *  `response` **Changed** (Breaking ⚠️)
  *  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.Assessments.Get()`: 
  *  `response` **Changed** (Breaking ⚠️)
  *  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.Assessments.Delete()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.Assessments.RecordOutcome()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.Assessments.Query()`: 
  *  `response.items[].Risk` **Changed** (Breaking ⚠️)
  *  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.Accounts.Get()`: 
  *  `response` **Changed** (Breaking ⚠️)
  *  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.Accounts.Delete()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.Accounts.ListAssessments()`: 
  *  `response.items[].Risk` **Changed** (Breaking ⚠️)
  *  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.KeyUsage.Record()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.Keys.Rotate()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.Integrations.Create()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.Integrations.List()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.Integrations.Delete()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.Integrations.Rotate()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.WebhookEndpoints.List()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.WebhookEndpoints.Create()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.WebhookEndpoints.Delete()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.WebhookEndpoints.RotateSigningKey()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.WebhookEndpoints.SendTestEvent()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.WebhookEndpoints.ListDeliveries()`:  `error.code` **Changed** (Breaking ⚠️)
* `Sdk.WebhookEndpoints.ReplayDelivery()`:  `error.code` **Changed** (Breaking ⚠️)

## 4.0.1

No changes detected.

## 4.0.0

* `Sdk.WebhookEndpoints.ListDeliveries()`:  `response.items[].EventType` **Changed** (Breaking ⚠️)
* `Sdk.WebhookEndpoints.ReplayDelivery()`:  `response.eventType` **Changed** (Breaking ⚠️)

## 3.0.9

No changes detected.

## 3.0.8

No changes detected.

## 3.0.7

No changes detected.

## 3.0.6

No changes detected.

## 3.0.5

No changes detected.

## 3.0.4

* `Sdk.KeyUsage.Record()`: **Added**
* `Sdk.Assessments.Create()`:  `response.account.Risk.Reasons[].Code.Enum(sharedApiKey)` **Added**
* `Sdk.Assessments.Get()`:  `response.account.Risk.Reasons[].Code.Enum(sharedApiKey)` **Added**
* `Sdk.Assessments.Query()`:  `response.items[].Risk.Reasons[].Code.Enum(sharedApiKey)` **Added**
* `Sdk.Accounts.Get()`:  `response.risk.Reasons[].Code.Enum(sharedApiKey)` **Added**
* `Sdk.Accounts.ListAssessments()`:  `response.items[].Risk.Reasons[].Code.Enum(sharedApiKey)` **Added**

## 3.0.3

No changes detected.

## 3.0.2

No changes detected.

## 3.0.1

No changes detected.

## 3.0.0

* `Sdk.Assessments.Create()`: 
  *  `request.body` **Changed** (Breaking ⚠️)
  *  `response` **Changed** (Breaking ⚠️)
* `Sdk.Assessments.Get()`:  `response` **Changed** (Breaking ⚠️)

## 2.1.16

* `Sdk.Assessments.Delete()`: **Added**

## 2.1.15

No changes detected.

## 2.1.14

No changes detected.

## 2.1.13

No changes detected.

## 2.1.12

* `Sdk.Accounts.Delete()`: **Added**

## 2.1.11

* `Sdk.Assessments.Create()`:  `request.body` **Changed**

## 2.1.10

No changes detected.

## 2.1.9

* `Sdk.WebhookEndpoints.ListDeliveries()`: **Added**
* `Sdk.WebhookEndpoints.ReplayDelivery()`: **Added**

## 2.1.8

No changes detected.

## 2.1.7

No changes detected.

## 2.1.6

No changes detected.

## 2.1.5

* `Sdk.Assessments.Create()`:  `response.risk.Reasons[].Code.Enum(testScenario)` **Added**
* `Sdk.Assessments.Get()`:  `response.risk.Reasons[].Code.Enum(testScenario)` **Added**
* `Sdk.Assessments.Query()`:  `response.items[].Risk.Reasons[].Code.Enum(testScenario)` **Added**
* `Sdk.Accounts.Get()`:  `response.risk.Reasons[].Code.Enum(testScenario)` **Added**
* `Sdk.Accounts.ListAssessments()`:  `response.items[].Risk.Reasons[].Code.Enum(testScenario)` **Added**

## 2.1.4

No changes detected.

## 2.1.3

No changes detected.

## 2.1.2

* `Sdk.Accounts.Get()`: **Added**
* `Sdk.Accounts.ListAssessments()`: **Added**

## 2.1.1

No changes detected.

## 2.1.0

* `Sdk.Assessments.Create()`:  `response.authContext` **Added**
* `Sdk.Assessments.Get()`:  `response.authContext` **Added**
* `Sdk.Assessments.Query()`:  `response.items[].AuthContext` **Added**

## 2.0.18

* `Sdk.WebhookEndpoints.List()`: **Added**
* `Sdk.WebhookEndpoints.Create()`: **Added**
* `Sdk.WebhookEndpoints.Delete()`: **Added**
* `Sdk.WebhookEndpoints.RotateSigningKey()`: **Added**
* `Sdk.WebhookEndpoints.SendTestEvent()`: **Added**
* `Sdk.Assessments.Create()`:  `error.code` **Changed**
* `Sdk.Assessments.Get()`:  `error.code` **Changed**
* `Sdk.Assessments.RecordOutcome()`:  `error.code` **Changed**
* `Sdk.Assessments.Query()`:  `error.code` **Changed**
* `Sdk.Keys.Rotate()`:  `error.code` **Changed**
* `Sdk.Integrations.Create()`:  `error.code` **Changed**
* `Sdk.Integrations.List()`:  `error.code` **Changed**
* `Sdk.Integrations.Delete()`:  `error.code` **Changed**
* `Sdk.Integrations.Rotate()`:  `error.code` **Changed**

## 2.0.17

* `Sdk.Integrations.Create()`: **Added**
* `Sdk.Integrations.List()`: **Added**
* `Sdk.Integrations.Delete()`: **Added**
* `Sdk.Integrations.Rotate()`: **Added**
* `Sdk.Assessments.Create()`:  `error.code.Enum(tooManyIntegrations)` **Added**
* `Sdk.Assessments.Get()`:  `error.code.Enum(tooManyIntegrations)` **Added**
* `Sdk.Assessments.RecordOutcome()`:  `error.code.Enum(tooManyIntegrations)` **Added**
* `Sdk.Assessments.Query()`:  `error.code.Enum(tooManyIntegrations)` **Added**
* `Sdk.Keys.Rotate()`: 
  *  `response.scopes[].Enum(integrations)` **Added**
  *  `error.code.Enum(tooManyIntegrations)` **Added**

## 2.0.16

* `Sdk.Assessments.Create()`: 
  *  `request.body.IssuedCredential` **Added**
  *  `response.risk` **Changed**
* `Sdk.Assessments.Get()`:  `response.risk` **Changed**
* `Sdk.Assessments.Query()`:  `response.items[].Risk` **Changed**

## 2.0.15

No changes detected.

## 2.0.14

* `Sdk.Assessments.Create()`: 
  *  `request.body.AuthContext` **Changed**

## 2.0.13

No changes detected.

## 2.0.12

No changes detected.

## 2.0.11

No changes detected.

## 2.0.10

No changes detected.

## 2.0.9

No changes detected.

## 2.0.8

No changes detected.

## 2.0.7

No changes detected.

## 2.0.6

No changes detected.

## 2.0.5

No changes detected.

## 2.0.4

No changes detected.

## 2.0.3

No changes detected.

## 2.0.2

API changes since the previous release are not listed: no earlier document was recorded.
