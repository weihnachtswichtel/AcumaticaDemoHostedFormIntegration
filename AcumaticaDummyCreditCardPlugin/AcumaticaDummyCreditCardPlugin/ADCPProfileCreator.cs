using Acumatica.ADPCGateway.Model;
using AcumaticaDummyProcessingCenterGatewayAPI;
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
        
        //Creates the profile from the transaction. Triggered in the Hosted Payment Horm flow.
        public TranProfile GetOrCreatePaymentProfileFromTransaction(string transactionId, CreateTranPaymentProfileParams cParams)
        {
            string url = settingValues.First(x => x.DetailID == ADCPConstants.ADPCURL).Value;
            string username = settingValues.First(x => x.DetailID == ADCPConstants.ADPCUserName).Value;
            string password = settingValues.First(x => x.DetailID == ADCPConstants.ADPCPassword).Value;
            string tenant = settingValues.First(x => x.DetailID == ADCPConstants.ADPCTenant).Value;

            Requests req = new Requests();
            Transaction tran = req.GetTransactionByID(url, username, password, tenant, transactionId);

            return new TranProfile
            {
                //ToDo implement CCPID properly
                CustomerProfileId = cParams.PCCustomerId,
                PaymentProfileId  = tran.PaymentProfileID.ToString()                  
            };
        }
    }
}