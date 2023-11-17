using Acumatica.ADPCGateway;
using Acumatica.ADPCGateway.Model;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data.Update.ExchangeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using static PX.SM.WikiRevision;

namespace AcumaticaDummyCreditCardPlugin
{
    public class ADCPHostedFormProcessor : ICCHostedFormProcessor
    {
        private IEnumerable<SettingsValue> settingValues;

        public ADCPHostedFormProcessor(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }
        public HostedFormData GetDataForCreateForm(CustomerData customerData, AddressData addressData)
        {
            string baseUrl, hostedFormURL = string.Empty;

            if (HttpContext.Current != null)
            {
                Page currentPage = HttpContext.Current.CurrentHandler as Page;
                if (HttpContext.Current.Request != null && HttpContext.Current.Request.UrlReferrer != null && currentPage != null && HttpContext.Current.Request.Url != null)
                {
                    baseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                    var baseUrl2 = VirtualPathUtility.ToAbsolute("~/");
                    hostedFormURL = baseUrl + baseUrl2 + "Frames/ADCPPaymentConnector.html";
                }
            }

            string hfkey = Requests.GetHostedFormUrlKey(ADCPHelper.GetPCGredentials(settingValues));
            string HFSUrl = ComposeHFSUrl(settingValues, hfkey);

            Dictionary<string, string> parms = new Dictionary<string, string>()
            {
                {"Type", "CreateOnly"},
                {"HFSKey", HFSUrl},
                {"CPID", customerData.CustomerProfileID},
                {"CustomerCD", customerData.CustomerCD},
                {"Width", "400" },
                {"Height", "300"}
            };

            return new HostedFormData() {
                Caption = "Create Payment Profile",
                Url = hostedFormURL,
                UseGetMethod = true,
               // Token = token,
                Parameters = parms
            };
        }

        private string ComposeHFSUrl(IEnumerable<SettingsValue> settingValues, string hfkey)
        { 
            string url =settingValues.First(x => x.DetailID == ADCPConstants.ADPCURL).Value;
            string tenant = settingValues.First(x => x.DetailID == ADCPConstants.ADPCTenant).Value;

            string hFSUrl = string.Format("{0}/Webhooks/{1}/{2}", url, tenant, hfkey);
            return hFSUrl;
        }

        public HostedFormData GetDataForManageForm(CustomerData customerData, CreditCardData cardData)
        {
            throw new System.NotImplementedException();
        }
    }
}