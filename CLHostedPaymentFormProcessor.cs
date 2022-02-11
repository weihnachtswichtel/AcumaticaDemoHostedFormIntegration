using PX.CCProcessingBase;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Objects.AR;
using System;
using System.Collections.Generic;
using ProcessingInput = PX.CCProcessingBase.Interfaces.V2.ProcessingInput;

namespace CookielessHostedForm
{
    public class CLHostedPaymentFormProcessor : ICCHostedPaymentFormProcessor
    {
        private IEnumerable<SettingsValue> settingValues;

        public CLHostedPaymentFormProcessor(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }
        public HostedFormData GetDataForPaymentForm(ProcessingInput inputData)
        {
            string baseUrl, hostedFormURL;
            baseUrl = PXContext.Session["CCPaymentConnectorUrl"] as string;                                       //Case when Hosted Payment Form called from Sales Orders Screen

            if (string.IsNullOrEmpty(baseUrl))
            {
                baseUrl = PXContext.GetSlot<string>(nameof(Extentions.GetPaymentConnectorUrl));                   //Case when Hosted Payment Form called fom Sales Order Screen for Acumatica ERP version >= 2022 R1
            }

            if (string.IsNullOrEmpty(baseUrl)) { 
                baseUrl = System.Web.HttpContext.Current.GetPaymentConnectorUrl();                                //Case when Payment Form called from Payments and Applications Screen
            } 
            hostedFormURL = baseUrl.Replace("PaymentConnector.html", "CLPaymentConnector.html");

            string customerCD = GetCustomerCD(inputData.DocumentData.DocType, inputData.DocumentData.DocRefNbr);    //For Demo only

            Dictionary<string, string> parms = new Dictionary<string, string>()
            {
                {"Type",    inputData.TranType.ToString()},
                {"Amount",  inputData.Amount.ToString()},
                {"Currency",inputData.CuryID},
                {"DocType", inputData.DocumentData.DocType},
                {"DocRefNbr", inputData.DocumentData.DocRefNbr},
                {"CPID", customerCD+"CCPID"},
                {"TranUID", inputData.TranUID.ToString()}                                                           //TranUid implementation for 2021 R1
            };
            

            return new HostedFormData()
            {
                Caption = "CLCharge",
                Url     = hostedFormURL,
                UseGetMethod = true,
                Token   = "CLTokenHostedPaymentForm",
                Parameters = parms
            };
        }

        private static string GetCustomerCD(string DocType, string DocRefNbr)
        {
            //this is for the for demo only. inputData.CustomerData is comming null to this interface unlike Hosted form Processor
            //So in order to pass the cpid to the hosted form, we retrieve it manually.
            ARPaymentEntry graph = PXGraph.CreateInstance<ARPaymentEntry>();

            Customer customer = PXSelectJoin<Customer, InnerJoin<ARPayment,
               On<Customer.bAccountID, Equal<ARPayment.customerID>>>,
           Where<
               ARPayment.docType, Equal<Required<ARPayment.docType>>,
               And<Where<
                   ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>>.Select(graph, DocType, DocRefNbr );

            return customer.AcctCD;
        }
    }
}