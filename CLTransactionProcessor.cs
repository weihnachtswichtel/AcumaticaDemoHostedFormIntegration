using PX.CCProcessingBase.Interfaces.V2;
using System.Collections.Generic;

namespace CookielessHostedForm
{
    public class CLTransactionProcessor : ICCTransactionProcessor
    {
        private IEnumerable<SettingsValue> settingValues;

        public CLTransactionProcessor(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }

        public ProcessingResult DoTransaction(ProcessingInput inputData)
        {
            throw new System.NotImplementedException();
            //ProcessingResult processingResult = new ProcessingResult {
            //    TransactionNumber = string.Format("{0}-{1}-{2}", inputData.CardData.PaymentProfileID, inputData.TranType, inputData.Amount.ToString()),
            //    ResponseCode = "200",
            //    ResponseText = "Success",
            //    ResponseReasonCode = "200",
            //    ResponseReasonText = "Success",
            //    AuthorizationNbr = string.Format("{0}-{1}-{2}", inputData.CardData.PaymentProfileID, inputData.TranType, inputData.Amount.ToString()),
            //    ExpireAfterDays = 7,
            //    CcvVerificatonStatus = CcvVerificationStatus.Match,

            //};
            //return processingResult;
        }
    }
}