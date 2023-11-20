using Acumatica.ADPCGateway;
using Acumatica.ADPCGateway.Model;
using PX.CCProcessingBase.Interfaces.V2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AcumaticaDummyCreditCardPlugin
{
    public class ADCPTransactionFinder : ICCTransactionFinder
    {
        private IEnumerable<SettingsValue> settingValues;

        public ADCPTransactionFinder(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }

        public TransactionData FindTransaction(TransactionSearchParams prms)
        {
            Transaction tran = Requests.FindTransactionByGUID(ADCPHelper.GetPCGredentials(settingValues), prms?.TransactionGuid);

            TransactionData td = new TransactionData
            {
                Amount = (decimal)tran.TransactionAmount.Value,
                AuthCode = "123",
                //CustomerId = "AACUSTOMER",
                CcvVerificationStatus = CcvVerificationStatus.Match,
                DocNum = tran.TransactionDocument,
                ExpireAfterDays = (tran.TransactionExpirationDate.Value - tran.TransactionDate.Value)?.Days,
                PaymentId = tran.PaymentProfileID.ToString(),
                SubmitTime = (DateTime)tran.TransactionDate.Value,
                TranID = tran.TransactionID,
                TranStatus = ADCPHelper.MapTranStatus[tran.TransactionStatus],
                TranType = ADCPHelper.MapTranType[tran.TransactionType],
                ResponseReasonCode = 200,
                ResponseReasonText = "Success",
                CardType = tran.PaymentProfileIDCardType,                                                        //As Card Type comes from the Processing Center
                CardTypeCode = ADCPHelper.MapCardType[tran.PaymentProfileIDCardType],                            //As Acumatica Internal enum                            
                TranUID = tran.Tranuid                                                                           //Setting TranUid returned from Processing Center
            };

            return td;
        }



        //public ProcessingResult DoTransaction(ProcessingInput inputData)
        //{
        //    //Here can be implemented the API call to the processing center and passing the all data including
        //    //inputData.TranUID that should be stored on Processing Center side same way as in scenario with HostedPaymentForm
        //    //(Implementation of GetDataForPaymentForm method of ICCHostedPaymentFormProcessor interface)

        //    Transaction tranToCreate = new Transaction();

        //    if (!string.IsNullOrEmpty(inputData.OrigTranID)){
        //        tranToCreate.TransactionID = inputData.OrigTranID;
        //    }
        //    tranToCreate.PaymentProfileID = Guid.Parse(inputData.CardData.PaymentProfileID);
        //    tranToCreate.TransactionDocument = inputData.DocumentData.DocType + inputData.DocumentData.DocRefNbr;
        //    tranToCreate.TransactionType = ADCPHelper.MapReverseTranType[inputData.TranType];
        //    tranToCreate.TransactionAmount = inputData.Amount;
        //    tranToCreate.Tranuid = inputData.TranUID;
        //    tranToCreate.TransactionStatus = "Approved";
        //    tranToCreate.TransactionCurrency = inputData.CuryID;

        //    Transaction tran = Requests.CreateTransaction(ADCPHelper.GetPCGredentials(settingValues), tranToCreate);

        //    ProcessingResult processingResult = new ProcessingResult {
        //        TransactionNumber = tran.TransactionID,
        //        ResponseCode = "200",
        //        ResponseText = tran.TransactionStatus,
        //        ResponseReasonCode = "200",
        //        ResponseReasonText = "Success",
        //        AuthorizationNbr = tran.AuthorizationNbr,
        //        ExpireAfterDays = (tran.TransactionExpirationDate.Value - tran.TransactionDate.Value)?.Days,
        //        CcvVerificatonStatus = CcvVerificationStatus.Match,
        //    };
        //    return processingResult;
        //}
    }
}