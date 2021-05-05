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
            int? expDate = null;
            if (inputData.TranType == CCTranType.AuthorizeOnly) { expDate = 1;}

            ProcessingResult processingResult = new ProcessingResult {
                TransactionNumber = "TRAN" + inputData.CardData.PaymentProfileID.Split('-')[1],   //string.Format("{0}-{1}-{2}", inputData.CardData.PaymentProfileID, inputData.TranType, inputData.Amount.ToString()),
                ResponseCode = "200",
                ResponseText = "Success",
                ResponseReasonCode = "200",
                ResponseReasonText = "Success",
                AuthorizationNbr = inputData.TranType == CCTranType.AuthorizeOnly ? "AUTH" + inputData.CardData.PaymentProfileID.Split('-')[1] : null,
                ExpireAfterDays = expDate,
                CcvVerificatonStatus = CcvVerificationStatus.Match,
            };
            return processingResult;
        }
    }
}