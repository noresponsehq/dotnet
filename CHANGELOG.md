# Changelog

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
