using PX.CCProcessingBase;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Objects.AR;
using System;
using System.Collections.Generic;
using System.Web;
using ProcessingInput = PX.CCProcessingBase.Interfaces.V2.ProcessingInput;

namespace CookielessHostedForm
{
    public class CLHostedPaymentFormProcessor : ICCHostedPaymentFormProcessor
    {
        private IEnumerable<SettingsValue> settingValues;

        private string _serviceOkCallbackUrl;
        private string _serviceCancelCalbackUrl;
        public string ServiceOkCallbackUrl
        {

            get
            {
                if (string.IsNullOrWhiteSpace(_serviceOkCallbackUrl))
                {
                    string responseURL = string.Empty;
                    if (UseServiceCallback())
                    {
                        Dictionary<string, string> qDict = GetUrlQueryDict();
                        if (qDict.ContainsKey("NoteId") && qDict.ContainsKey("CompanyName"))
                        {
                            qDict.Add("Result", "Ok");
                            responseURL = CCServiceEndpointHelper.GetUrl(CCServiceAction.AcceptPaymentFormCallback, qDict);
                        }
                    }
                    _serviceOkCallbackUrl = responseURL;
                }
                return _serviceOkCallbackUrl;
            }
            set
            {
                _serviceOkCallbackUrl = value;
            }
        }

        public string ServiceCancelUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_serviceCancelCalbackUrl))
                {
                    string responseURL = string.Empty;
                    if (UseServiceCallback())
                    {
                        Dictionary<string, string> qDict = GetUrlQueryDict();
                        if (qDict.ContainsKey("NoteId") && qDict.ContainsKey("CompanyName"))
                        {
                            qDict.Add("Result", "Cancel");
                            responseURL = CCServiceEndpointHelper.GetUrl(CCServiceAction.AcceptPaymentFormCallback, qDict);
                        }
                    }
                    _serviceCancelCalbackUrl = responseURL;
                }
                return _serviceCancelCalbackUrl;
            }
            set
            {
                _serviceCancelCalbackUrl = value;
            }
        }

        private bool UseServiceCallback()
        {
            bool ret = false;
            if (HttpContext.Current != null && HttpContext.Current.Request.Url != null)
            {
                HttpRequest request = HttpContext.Current.Request;
                string servicePath = request.ApplicationPath + "/" + CCServiceEndpointHelper.ServicePath;
                servicePath = servicePath.Replace("//", "/");
                if (request.Url.AbsolutePath == servicePath)
                {
                    ret = true;
                }
            }
            return ret;
        }

        private Dictionary<string, string> GetUrlQueryDict()
        {
            Dictionary<string, string> qDict = new Dictionary<string, string>();
            HttpRequest request = HttpContext.Current.Request;
            var qData = HttpUtility.ParseQueryString(request.Url.Query);
            string noteId = qData.Get("NoteId");
            string docType = qData.Get("DocType");
            string companyName = qData.Get("CompanyName");
            string tranId = qData.Get("TranUID");
            if (docType != null)
            {
                qDict.Add("DocType", docType);
            }
            if (noteId != null && Guid.TryParse(noteId, out Guid id))
            {
                qDict.Add("NoteId", id.ToString());
            }
            if (tranId != null && Guid.TryParse(tranId, out Guid tranGuid))
            {
                qDict.Add("TranUID", tranGuid.ToString());
            }
            if (companyName != null)
            {
                qDict.Add("CompanyName", companyName);
            }
            return qDict;
        }

        private string EncodeUrlQuery(string input)
        {
            Uri uri = new Uri(input);
            string encodedQury = Uri.EscapeDataString(uri.Query);
            var result = uri.GetLeftPart(UriPartial.Path) + encodedQury;
            return result;
        }

        public CLHostedPaymentFormProcessor(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }
        public HostedFormData GetDataForPaymentForm(ProcessingInput inputData)
        {
            bool isMobile = false;
            string baseUrl, hostedFormURL;
            baseUrl = PXContext.Session["CCPaymentConnectorUrl"] as string;                                       //Case when Hosted Payment Form called from Sales Orders Screen

            if (string.IsNullOrEmpty(baseUrl))
            {
                baseUrl = PXContext.GetSlot<string>(nameof(Extentions.GetPaymentConnectorUrl));                   //Case when Hosted Payment Form called fom Sales Order Screen for Acumatica ERP version >= 2022 R1
            }

            if (string.IsNullOrEmpty(baseUrl)) { 
                baseUrl = System.Web.HttpContext.Current.GetPaymentConnectorUrl();                                //Case when Payment Form called from Payments and Applications Screen
            }

            if (string.IsNullOrEmpty(baseUrl)) {                                                                  //Mobile Case
                baseUrl = CCServiceEndpointHelper.GetUrl(CCServiceAction.AcceptPaymentFormCallback);
                if (!string.IsNullOrEmpty(baseUrl)) { isMobile = true; }
            }

            if (!isMobile)
            {
                hostedFormURL = baseUrl.Replace("PaymentConnector.html", "CLPaymentConnector.html");
            }
            else {
                hostedFormURL = baseUrl.Split(new string[] { "CCService?" }, StringSplitOptions.None)[0] + "Frames/CLPaymentConnector.html";
            }


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

            if (isMobile) {
                //Get the initial data for transaction to use later in GetUnsettledTransactions(). This should not be used for real scenarios
                PXContext.Session.SetString("MobileDummyFix", string.Join(Environment.NewLine, parms));

                //Add encoded callback that should be invoked on Pay button and Cancel button
                parms.Add("CallbackOK", EncodeUrlQuery(ServiceOkCallbackUrl));
                parms.Add("CallbackCancel", EncodeUrlQuery(ServiceCancelUrl));
            }

            return new HostedFormData()
            {
                Caption = "CLCharge",
                Url = hostedFormURL,
                UseGetMethod = true,
                Token = !isMobile ? "CLTokenHostedPaymentForm" : "CLTokenHostedPaymentFormMobile",
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