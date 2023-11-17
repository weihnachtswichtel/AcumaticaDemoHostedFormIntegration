using Acumatica.ADPCGateway;
using Acumatica.ADPCGateway.Model;
using PX.CCProcessingBase.Interfaces.V2;
using System.Collections.Generic;
using System.Linq;

namespace AcumaticaDummyCreditCardPlugin
{
    internal class ADCPProfileCreator : ICCProfileCreator
    {
        private IEnumerable<SettingsValue> settingValues;

        public ADCPProfileCreator(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }
        
        //Creates the profile from the transaction. Triggered in the Hosted Payment Form flow.
        public TranProfile GetOrCreatePaymentProfileFromTransaction(string transactionId, CreateTranPaymentProfileParams cParams)
        {
            Transaction tran = Requests.GetTransactionByID(ADCPHelper.GetPCGredentials(settingValues), transactionId);

            return new TranProfile
            {
                CustomerProfileId = tran.CustomerProfileID.ToString(),
                PaymentProfileId  = tran.PaymentProfileID.ToString()                  
            };
        }
    }
}