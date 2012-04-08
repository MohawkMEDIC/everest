Mohawk College EHR API XML ITS 1.0 Formatter
============================================

Introduction

The XML ITS 1.0 formatter is responsible for graphing objects from the RMIM 
format used to create them, into an XML instance using the appropriate rules.
In addition to this processing, the formatter verifies that the rendered 
instance is valid (matches conformance).

Notes:
- When de-graphing (parsing) objects from XML into the RMIM format, it is 
  best to ensure that no code validation errors occur as these tend to slow
  processing down extremely. For example: Sending the code "Animal" for a
  CS that is bound to HL7AdministrativeGender will fail.
- When parsing an object with ValidateConformnace option set to "true", 
  invalid messages will still be returned to the caller with the 
  code of AcceptedNonConfomrant.
- For best performance, you may choose to set the ValidateConformance option
  to false. Normally, when graphing or parsing objects, the formatter will
  check the instance against conformance rules. If this is disabled, no
  checking will be performed (this setting does not perculate to graph 
  aides). This means that non-conformant objects can be graphed to XML
  and non-conformant XML can be graphed to RMIM objects.