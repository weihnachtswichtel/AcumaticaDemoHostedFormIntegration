# Acumatica Dummy Credit Card Plug-in
## Purpose

The main purpose of this project is to demonstrate the implementation of the Credit Card Plug-in for Acumatica ERP. It is attempted to provide some tips, suggestions and clarifications in the comments. The project could be used just to explore the way the Credit Card Plug-in works.

## Content and Structure

Credit Card Plug-in is an integration between the Acumatica ERP and Processing Center or Gateway that actually manages Card Payments. To cover the implementation of possibly most interfaces and provide working examples that would be close to the real life situation, the project is divided into 3 parts:

 1. AcumaticaDummyProcessingCenter
 2. AcumaticaDummyProcessingCenterGatewayAPI
 3. AcumaticaDummyCreditCardPlugin

### AcumaticaDummyProcessingCenter
This is an Acumatica ERP customization that plays the role of Processing Center. It saves information about the Customers (aka Customer Profiles), vaulted Cards (aka Payment Profiles) and Transactions. Records could be created or modified  by Plug-in implementation or manually. Which could be useful for demonstration, when not only Acumatica ERP is linked to the merchant account, e.g. eCommerce scenario. It is possible to deploy the customization on the same Acumatica ERP instance or any other instance on the same machine where the Credit Card Plug-in is deployed.

### AcumaticaDummyProcessingCenterGatewayAPI
This project represents the SDK or some kind of set of interfaces that is usually provided by the Processing Centers for 3rd party developers to enable the integration with their services.
It contains REST API Request templates for the WebService Endpoint of **AcumaticaDummyProcessingCenter** based on the [AcumaticaRESTAPIClientForCSharp](https://github.com/Acumatica/AcumaticaRESTAPIClientForCSharp). The built DLL is a part of **AcumaticaDummyCreditCardPlugin** customization.

### AcumaticaDummyCreditCardPlugin
The actual implementation of the **ICCPlugin** interfaces, which uses API requests to communicate with Dummy Processing Center.
To configure the Plug-in, on the Processing Center screen you should specify the credentials of Acumatica ERP instance where **AcumaticaDummyProcessingCenter** is deployed.

***Currently supported features and scenarios:***
 - Test credentials
 - Vault cards via Customer Payment Method screen (HostedForm)
 - Process payments using vaulted cards
 - Process payments using new cards (HostedPaymentForm)
 - Record CC Payment
 - Validate CC Payment

### Legacy version
The repository also contains the previous version of plug-in in the **Legacy** folder with a ICCPlugin implementation that is not linked to any Processing Center, and therefore having limited functionality. The main focus is to demonstrate possibility to pass the data from Acumatica to Hosted Form and vice versa.