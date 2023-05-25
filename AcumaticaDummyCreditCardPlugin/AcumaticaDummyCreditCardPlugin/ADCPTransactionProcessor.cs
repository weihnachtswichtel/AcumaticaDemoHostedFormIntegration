using PX.CCProcessingBase.Interfaces.V2;
using System.Collections.Generic;

namespace AcumaticaDummyCreditCardPlugin
{
    public class ADCPTransactionProcessor : ICCTransactionProcessor
    {
        private IEnumerable<SettingsValue> settingValues;

        public ADCPTransactionProcessor(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }

        public ProcessingResult DoTransaction(ProcessingInput inputData)
        {

            //Here can be implemented the API call to the processing center and passing the all data including
            //inputData.TranUID that should be stored on Processing Center side same way as in scenario with HostedPaymentForm
            //(Implementation of GetDataForPaymentForm method of ICCHostedPaymentFormProcessor interface)


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