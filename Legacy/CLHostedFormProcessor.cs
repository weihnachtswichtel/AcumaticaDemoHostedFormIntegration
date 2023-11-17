using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace CookielessHostedForm
{
    public class CLHostedFormProcessor : ICCHostedFormProcessor
    {
        private IEnumerable<SettingsValue> settingValues;

        public CLHostedFormProcessor(IEnumerable<SettingsValue> settingValues)
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
                    hostedFormURL = baseUrl + baseUrl2 + "Frames/CLPaymentConnector.html";
                }
            }

            Dictionary<string, string> parms = new Dictionary<string, string>()
            {
                {"Type", "CreateOnly"},
                {"CPID", customerData.CustomerProfileID},
                {"Width", "400" },
                {"Height", "300"}
            };

            return new HostedFormData() {
                Caption = "CLCreate",
                Url = hostedFormURL,
                UseGetMethod = true,
                 Token = "CLToken",
                Parameters = parms
            };
        }

        public HostedFormData GetDataForManageForm(CustomerData customerData, CreditCardData cardData)
        {
            throw new System.NotImplementedException();
        }
    }
}