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
        }

        public class HFResponse {
            public string CPID { get; set; }
            public string PPID { get; set; }
        }


        public async Task<System.Web.Http.IHttpActionResult> ProcessRequestAsync(
          HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = GetAdminScope())
                {
                    string requestBody = await request.Content.ReadAsStringAsync();
                    HFRequest hFRequest = JsonConvert.DeserializeObject<HFRequest>(requestBody);
                    ADPCCustomerProfileEntry aDPCCustomerProfileEntry = PXGraph.CreateInstance<ADPCCustomerProfileEntry>();
                    ADPCCustomerProfile cp = new ADPCCustomerProfile();
                    cp.CustomerName = hFRequest.CustomerCD;
                    aDPCCustomerProfileEntry.CustomerProfile.Insert(cp);
                    aDPCCustomerProfileEntry.Save.Press();
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

                    request.Content = new StringContent(JsonConvert.SerializeObject(hFResponse));
                }
            }
            catch (Exception e) { 
                return new ExceptionResult(e, false, new DefaultContentNegotiator(), request, new[] { new JsonMediaTypeFormatter() });
            }

           return new OkResult(request);
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
