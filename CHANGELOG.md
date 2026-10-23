# Changelog

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
