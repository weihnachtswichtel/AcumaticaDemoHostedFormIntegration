using PX.Data.Webhooks;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PX.Data;
using System.Web.Http.Results;
using Newtonsoft.Json;
using PX.CCProcessing.authorizeNetApi;
using System.Web.Http;
using static PX.Data.BQL.BqlPlaceholder;
using System.Net;
using PX.Common;

namespace AcumaticaDummyProcessingCenter
{
    
    public class ADPCHostedFormWebHookHandler : IWebhookHandler
    {

        public class HFRequest
        {
            public string Type { get; set; }
            public string CPID { get; set; }
            public string CustomerCD { get; set; }
            public string Token { get; set; }
            public string Cardtype { get; set; }
            public string Card { get; set; }
            public string ExpDate { get; set; }
            public decimal Amount { get; set; }
            public string Currency { get; set; }
            public string DocType { get; set; }
            public string DocRefNbr { get; set; }
            public Guid TranUID { get; set; }
            public string TransactionStatus { get; set; }
        }

        public class HFResponse {
            public string CPID { get; set; }
            public string PPID { get; set; }
        }


        public class TextResult : IHttpActionResult
        {
            string _value;
            HttpRequestMessage _request;

            public TextResult(string value, HttpRequestMessage request)
            {
                _value = value;
                _request = request;
            }
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage()
                {
                    Content = new StringContent(_value),
                    RequestMessage = _request
                };
                return Task.FromResult(response);
            }
        }



        public async Task<System.Web.Http.IHttpActionResult> ProcessRequestAsync(
          HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var scope = GetAdminScope())
            {
            try
            {
                    string requestBody = await request.Content.ReadAsStringAsync();
                    HFRequest hFRequest = JsonConvert.DeserializeObject<HFRequest>(requestBody);
                    ADPCCustomerProfileEntry aDPCCustomerProfileEntry = PXGraph.CreateInstance<ADPCCustomerProfileEntry>();
                    ADPCCustomerProfile cp = new ADPCCustomerProfile();
                    if (!string.IsNullOrEmpty(hFRequest.CPID)) {
                        cp.CustomerProfileID = hFRequest.CPID;
                        aDPCCustomerProfileEntry.CustomerProfile.Current = cp;
                    }
                    else
                    {
                        cp.CustomerName = hFRequest.CustomerCD;
                        aDPCCustomerProfileEntry.CustomerProfile.Insert(cp);
                        aDPCCustomerProfileEntry.Save.Press();
                    }
                       
                    ADPCPaymentProfile pp = aDPCCustomerProfileEntry.PaymentProfiles.Insert();
                    pp.CustomerProfileID = aDPCCustomerProfileEntry.CustomerProfile.Current.CustomerProfileID;
                    pp.CardLastFour = hFRequest.Card.Substring(hFRequest.Card.Length - 4);
                    pp.Cardbin = hFRequest.Card.Substring(0, 6);
                    pp.CardType = hFRequest.Cardtype[0].ToString();
                    aDPCCustomerProfileEntry.PaymentProfiles.Current = pp;
                    aDPCCustomerProfileEntry.Save.Press();

                    HFResponse hFResponse = new HFResponse();

                    hFResponse.CPID = aDPCCustomerProfileEntry.PaymentProfiles.Current.CustomerProfileID;
                    hFResponse.PPID = aDPCCustomerProfileEntry.PaymentProfiles.Current.PaymentProfileID.ToString();

                    if (!string.IsNullOrEmpty(hFRequest.Type) && hFRequest.Type !="CreateOnly")
                    {
                        var transactionMaint = PXGraph.CreateInstance<ADPCTransactionEntry>();
                        var transaction = transactionMaint.Transaction.Insert();
                        transaction.TransactionType = "A";
                        transaction.CustomerProfileID = aDPCCustomerProfileEntry.PaymentProfiles.Current.CustomerProfileID;
                        transaction.PaymentProfileID = aDPCCustomerProfileEntry.PaymentProfiles.Current.PaymentProfileID;
                        transaction.TransactionDocument = hFRequest.DocType + hFRequest.DocRefNbr;
                        transaction.TransactionAmount = hFRequest.Amount;
                        transaction.TransactionCurrency = hFRequest.Currency;
                        transaction.TransactionDate = DateTime.UtcNow;
                        transaction.Tranuid = hFRequest.TranUID;
                        transaction.TransactionStatus = string.IsNullOrEmpty(hFRequest.TransactionStatus) ? "A" : hFRequest.TransactionStatus;
                        transactionMaint.Save.Press();
                        return new TextResult(transactionMaint.Transaction.Current.TransactionID, request);
                    }

                    string text = JsonConvert.SerializeObject(hFResponse);
                    return new TextResult(text, request);

                }
            catch (Exception e) { 
                return new ExceptionResult(e, false, new DefaultContentNegotiator(), request, new[] { new JsonMediaTypeFormatter() });
            }
            }
        }

        private IDisposable GetAdminScope()
        {
            var userName = "admin";
            if (PXDatabase.Companies.Length > 0)
            {
                var company = PXAccess.GetCompanyName();
                if (string.IsNullOrEmpty(company))
                {
                    company = PXDatabase.Companies[0];
                }
                userName = userName + "@" + company;
            }
            return new PXLoginScope(userName);
        }

    }
}
