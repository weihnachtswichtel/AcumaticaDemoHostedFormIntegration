using AcumaticaDummyProcessingCenterGatewayAPI;
using PX.CCProcessingBase;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Objects.AR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProcessingInput = PX.CCProcessingBase.Interfaces.V2.ProcessingInput;

namespace AcumaticaDummyCreditCardPlugin
{
    public class ADCPHostedPaymentFormProcessor : ICCHostedPaymentFormProcessor
    {
        private IEnumerable<SettingsValue> settingValues;

        public ADCPHostedPaymentFormProcessor(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }
        public HostedFormData GetDataForPaymentForm(ProcessingInput inputData)
        {
            string baseUrl, hostedFormURL;
            baseUrl = PXContext.Session["CCPaymentConnectorUrl"] as string;                                       //Case when Hosted Payment Form called from Sales Orders Screen

            if (string.IsNullOrEmpty(baseUrl))
            {
                baseUrl = PXContext.GetSlot<string>(nameof(Extentions.GetPaymentConnectorUrl));                   //Case when Hosted Payment Form called fom Sales Order Screen for Acumatica ERP version == 2022 R1
            }

            if (string.IsNullOrEmpty(baseUrl)) { 
                baseUrl = System.Web.HttpContext.Current.GetPaymentConnectorUrl();                                //Case when Payment Form called from Payments and Applications Screen
            }

            if (string.IsNullOrEmpty(baseUrl))
            {
                baseUrl = CCPaymentProcessingHelper.GetPaymentConnectorUrl(HttpContext.Current);                   //Case for Hosted Payment Form called fom Sales Order Screen for Acumatica ERP version >= 2022 R2
            }

            hostedFormURL = baseUrl.Replace("PaymentConnector.html", "ADCPPaymentConnector.html");

            string url = settingValues.First(x => x.DetailID == ADCPConstants.ADPCURL).Value;
            string username = settingValues.First(x => x.DetailID == ADCPConstants.ADPCUserName).Value;
            string password = settingValues.First(x => x.DetailID == ADCPConstants.ADPCPassword).Value;
            string tenant = settingValues.First(x => x.DetailID == ADCPConstants.ADPCTenant).Value;

            Requests req = new Requests();
            string hfkey = req.GetHostedFormUrlKey(url, username, password, tenant);
            string HFSUrl = ComposeHFSUrl(settingValues, hfkey);

            //string customerCD = GetCustomerCD(inputData.DocumentData.DocType, inputData.DocumentData.DocRefNbr);    //For Demo only

            Dictionary<string, string> parms = new Dictionary<string, string>()
            {
                {"Type",         inputData.TranType.ToString()},
                {"HFSKey",       HFSUrl},
                {"CPID",         inputData.CustomerData.CustomerProfileID},
                {"CustomerCD",   inputData.CustomerData.CustomerCD},
                {"CustomerName", inputData.CustomerData.CustomerName},
                {"Email",        inputData.CustomerData.Email},
                {"Amount",       inputData.Amount.ToString()},
                {"Currency",     inputData.CuryID},
                {"DocType",      inputData.DocumentData.DocType},
                {"DocRefNbr",    inputData.DocumentData.DocRefNbr},
                {"TranUID",      inputData.TranUID.ToString()}                                                           //TranUid implementation for 2021 R1
            };
            

            return new HostedFormData()
            {
                Caption = "CLCharge",
                Url     = hostedFormURL,
                UseGetMethod = true,
               // Token   = "CLTokenHostedPaymentForm",
                Parameters = parms
            };
        }

        private string ComposeHFSUrl(IEnumerable<SettingsValue> settingValues, string hfkey)
        {
            string url = settingValues.First(x => x.DetailID == ADCPConstants.ADPCURL).Value;
            string tenant = settingValues.First(x => x.DetailID == ADCPConstants.ADPCTenant).Value;

            string hFSUrl = string.Format("{0}/Webhooks/{1}/{2}", url, tenant, hfkey);
            return hFSUrl;
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