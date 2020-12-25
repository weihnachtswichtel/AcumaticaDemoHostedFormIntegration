using PX.CCProcessingBase.Interfaces.V2;
using System.Collections.Generic;

namespace CookielessHostedForm
{
    internal class CLProfileCreator : ICCProfileCreator
    {
        private IEnumerable<SettingsValue> settingValues;

        public CLProfileCreator(IEnumerable<SettingsValue> settingValues)
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