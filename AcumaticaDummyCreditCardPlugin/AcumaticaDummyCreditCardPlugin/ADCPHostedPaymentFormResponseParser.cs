using Newtonsoft.Json.Linq;
using PX.CCProcessingBase.Interfaces.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcumaticaDummyCreditCardPlugin
{
    public class ADCPHostedPaymentFormResponseParser : ICCHostedPaymentFormResponseParser
    {
        private IEnumerable<SettingsValue> settingValues;

        public ADCPHostedPaymentFormResponseParser(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }
        public HostedFormResponse Parse(string input)
        {
            //bool ivAVSDeclined = true;

            //if (ivAVSDeclined) {
            //    throw new CCProcessingException("AVS declined", CCProcessingException.ExceptionReason.TranDeclined);
            //}

            return new HostedFormResponse()
            {
                TranID = input
            };
        }
    }
}
