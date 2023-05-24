using PX.CCProcessingBase.Interfaces.V2;
using System.Collections.Generic;

namespace AcumaticaDummyCreditCardPlugin
{
    internal class ADCPProfileCreator : ICCProfileCreator
    {
        private IEnumerable<SettingsValue> settingValues;

        public ADCPProfileCreator(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }

        public TranProfile GetOrCreatePaymentProfileFromTransaction(string transactionId, CreateTranPaymentProfileParams cParams)
        {
            string[] transactionInfo = transactionId.Split('-');
            string token             = string.Format("{0}-{1}-{2}", transactionInfo[0], transactionInfo[1], transactionInfo[2]);

            return new TranProfile
            {
                CustomerProfileId = cParams.PCCustomerId,
                PaymentProfileId  = token                  
            };
        }
    }
}